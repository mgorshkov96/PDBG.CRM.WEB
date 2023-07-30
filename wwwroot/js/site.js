async function getAgents() {
    let response = await fetch('https://localhost:32768/api/location');
    let result = await response.json();
    return result;
}
async function init() {
    
    let agents = await getAgents();
    
    var myMap = new ymaps.Map("map", {       
        center: [59.945206, 30.214301],
        zoom: 10
    });

    var myAgents = [];

    for (i = 0; i < agents.length; i++) {
        var date = new Date(agents[i].lDate);

        myAgents[i] = new ymaps.GeoObject({
            geometry: {
                type: 'Point',
                coordinates: [agents[i].lat, agents[i].lng]
            },
            properties: {
                clusterCaption: `${agents[i].agentName}`,
                balloonContent: `Агент: ${agents[i].agentName}<br />Статус: ${agents[i].statusName}<br />Дата: ${date.toLocaleString()}`
            }
        });

        switch (agents[i].statusName) {
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

    var myClusterer = new ymaps.Clusterer({
        clusterDisableClickZoom: true,
        clusterOpenBalloonOnClick: true,
        clusterBalloonContentLayout: 'cluster#balloonAccordion'
    });

    myClusterer.add(myAgents);
    myMap.geoObjects.add(myClusterer);
}

ymaps.ready(init);