async function getAgents() {
    let response = await fetch('https://pdbg-crm.ru/api/location');
    /* https://localhost:32768/api/location */
    let result = await response.json();
    return result;
}
async function init() {
    function cleanFilter() {
        myMap.geoObjects.removeAll();
        myClusterer.removeAll();
        myAgents.splice(0, myAgents.length);
        filtredAgents.splice(0, filtredAgents.length);
    }

    function updatePlacemarks() {
        cleanFilter();
        agentsSelect.value = 0;
        boxFilter();
        showAgents();
    }

    function updateSelect() {
        cleanFilter();
        boxOnline.checked = true;
        boxOffline.checked = true;
        boxNotAvailable.checked = true;
        selectFilter();
        showAgents();
    }

    function boxFilter() {
        for (i = 0; i < agents.length; i++) {
            if (boxOnline.checked) {
                if (agents[i].statusName == 'Онлайн') {
                    filtredAgents.push(agents[i]);
                }
            }
            if (boxOffline.checked) {
                if (agents[i].statusName == 'Офлайн') {
                    filtredAgents.push(agents[i]);
                }
            }
            if (boxNotAvailable.checked) {
                if (agents[i].statusName == 'Недоступен') {
                    filtredAgents.push(agents[i]);
                }
            }
        }
    }

    function selectFilter() {
        for (i = 0; i < agents.length; i++) {
            if (agentsSelect.value == 0) {
                filtredAgents.push(agents[i]);
            }
            if (agents[i].agentId == agentsSelect.value) {
                filtredAgents.push(agents[i]);
            }
        }
    }

    function showAgents() {       

        for (i = 0; i < filtredAgents.length; i++) {
            var date = new Date(filtredAgents[i].lDate);

            myAgents[i] = new ymaps.GeoObject({
                geometry: {
                    type: 'Point',
                    coordinates: [filtredAgents[i].lat, filtredAgents[i].lng]
                },
                properties: {
                    clusterCaption: `${filtredAgents[i].agentName}`,
                    balloonContent: `Агент: ${filtredAgents[i].agentName}<br />Статус: ${filtredAgents[i].statusName}<br />Дата: ${date.toLocaleString()}`
                }
            });

            switch (filtredAgents[i].statusName) {
                case 'Онлайн':
                    myAgents[i].options.set({
                        preset: 'islands#blueDotIcon'
                    });
                    break;
                case 'Офлайн':
                    myAgents[i].options.set({
                        preset: 'islands#grayDotIcon'
                    });
                    break;
                case 'Недоступен':
                    myAgents[i].options.set({
                        preset: 'islands#violetDotIcon'
                    });
                    break;
            }
        }

        myClusterer.add(myAgents);
        myMap.geoObjects.add(myClusterer);
    }

    var agents = await getAgents();

    let boxOnline = document.getElementById('box-online');
    let boxOffline = document.getElementById('box-offline');
    let boxNotAvailable = document.getElementById('box-not-available');
    let agentsSelect = document.getElementById('agents');
    boxOnline.addEventListener("change", updatePlacemarks);
    boxOffline.addEventListener("change", updatePlacemarks);
    boxNotAvailable.addEventListener("change", updatePlacemarks);
    agentsSelect.addEventListener("change", updateSelect);

    for (i = 0; i < agents.length; i++) {
        let option = document.createElement('option');
        option.text = agents[i].agentName;
        option.value = agents[i].agentId;
        //document.querySelector('#agents').add(option);
        agentsSelect.add(option);
    }

    agentsSelect.size = agents.length + 1;


    var filtredAgents = [];
    
    var myMap = new ymaps.Map("map", {       
        center: [59.945206, 30.214301],
        zoom: 10
    });

    var myAgents = [];

    var myClusterer = new ymaps.Clusterer({
        clusterDisableClickZoom: true,
        clusterOpenBalloonOnClick: true,
        clusterBalloonContentLayout: 'cluster#balloonAccordion'
    });

    boxFilter();
    showAgents();  
}

ymaps.ready(init);