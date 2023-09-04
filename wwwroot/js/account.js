function accClicked() {
    subAcc.classList.toggle("active");
    console.log("Clicked");
}

var btnAcc = document.getElementById("btn-account");
var subAcc = document.getElementById("sub-account");
btnAcc.addEventListener("click", accClicked);