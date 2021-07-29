let UserID = 0;
let GeneralActionType = "";
const wheel = document.querySelector('.wheel');
const text = document.querySelector('.text')
const container_point = document.querySelector('.container_point')
let child_point = document.querySelectorAll('.container_point p')
const GlobalHostURL = "http://localhost:56750"

let SwippingFin = (id , ActionType) => {
    document.querySelector(".button").style.pointerEvents = 'none';

    deg = Math.floor(5000 + Math.random() * 5000);

    wheel.style.transition = 'all 1s ease-out';
    container_point.style.transition = 'all 1s ease-out';


    wheel.style.transform = `rotate(${deg}deg)`;
    container_point.style.transform = `rotate(${deg}deg)`;
    UserID = id;
    GeneralActionType = ActionType;
    wheel.classList.add('blur');
};
(function () {


    // text of points
    child_point[0].innerHTML = '100'
    child_point[1].innerHTML = '200'
    child_point[2].innerHTML = '300'
    child_point[3].innerHTML = '400'
    child_point[4].innerHTML = '500'
    child_point[5].innerHTML = '600'
    child_point[6].innerHTML = '700'
    child_point[7].innerHTML = '800'
    child_point[8].innerHTML = '900'
    child_point[9].innerHTML = '1000'


    let deg = 0;
    //text.innerHTML='like'


    wheel.addEventListener('transitionend', () => {
        wheel.classList.remove('blur');
        container_point.classList.remove('blur');
        document.querySelector(".button").style.pointerEvents = 'auto';

        wheel.style.transition = 'none';
        container_point.style.transition = 'none';
        const actualDeg = deg % 360;

        if (actualDeg <= 32 && actualDeg >= 0) {
            setTimeout(function() {
                text.innerHTML = 'unlike Add 100 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 100 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 68 && actualDeg >= 33) {
            setTimeout(function() {
                text.innerHTML = 'like Add 200 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 200 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 104 && actualDeg >= 69) {
            setTimeout(function() {
                text.innerHTML = 'watch later Add 300 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 300 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 140 && actualDeg >= 105) {
            setTimeout(function() {
                text.innerHTML = 'download Add 400 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 400 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 176 && actualDeg >= 141) {
            setTimeout(function() {
                text.innerHTML = 'upload Add 500 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 500 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 212 && actualDeg >= 177) {
            setTimeout(function() {
                text.innerHTML = 'save Add 600 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 600 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 248 && actualDeg >= 213) {
            setTimeout(function() {
                text.innerHTML = 'link Add 700 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 700 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 284 && actualDeg >= 249) {
            setTimeout(function() {
                text.innerHTML = 'subscribe button Add 800 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 800 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 320 && actualDeg >= 285) {
            setTimeout(function() {
                text.innerHTML = 'share Add 800 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 900 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        } else if (actualDeg <= 356 && actualDeg >= 321) {
            setTimeout(function() {
                text.innerHTML = 'comment Add 800 Coins :)';
                fetch(GlobalHostURL + '/Home/LuckyBoxPost?id=' + UserID + '&EarnedCoinsNumber=' + 1000 + '&ActionType=' + GeneralActionType)
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            }, 100)
        }
        //console.log(actualDeg)
        wheel.style.transform = `rotate(${actualDeg}deg)`;
        container_point.style.transform = `rotate(${actualDeg}deg)`;
    });
})();