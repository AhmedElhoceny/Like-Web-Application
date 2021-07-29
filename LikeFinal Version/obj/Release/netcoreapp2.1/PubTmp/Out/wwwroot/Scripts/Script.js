window.scrollTo(500, 0); 
let GlobalHostURL = "http://localhost:53308";
let GeneralData = {
    videoURL: "Null",
    videoTime: 0,
    videoOwnerID: 0,
    clientID: 0,
    CoinsNumber: 0,
    ItemID: 0,
    ItemPreviousViews: 0
};
let flag = 0;
let Start = ()=>{
    window.scrollTo(500, 600); 
}
let Starts = ()=>{
    window.scrollTo(500, 950); 
}
let Targets = ()=>{
    window.scrollTo(500, 1300); 
}

let ShowLogIn = ()=>{
    document.querySelector(".LogIn").style.top="60px";
    document.querySelector(".PlaceHolder").style.display="block";
}
let HideAllForms = ()=>{
    document.querySelector(".LogIn").style.top="-200%";
    document.querySelector(".Sign").classList.remove("ShowDataForm");
    document.querySelector(".PlaceHolder").style.display="none";
    document.querySelector(".MeniItems").style.right = "-100%";
}
let SignUp = ()=>{
    document.querySelector(".LogIn").style.top="-200%";
    document.querySelector(".Sign").classList.add("ShowDataForm");
    document.querySelector(".PlaceHolder").style.display="block";
}
let ShowMenu = ()=>{
    document.querySelector(".MeniItems").style.right = "0px";
    document.querySelector(".PlaceHolder").style.display="block";
}

let HideAllForms2 = () => {
    document.querySelector(".PutNumberCoins").style.top = "-200%";
    document.querySelector(".AddNewItem").style.top = "-200%";
    document.querySelector(".ListIsEmpty").style.top = "-200%";
    document.querySelector(".PlaceHolder").style.display = "none";
}
let ShowAddNewItem = () => {
    document.querySelector(".AddNewItem").style.top = "20px";
    document.querySelector(".PlaceHolder").style.display = "block";
}
let YoutubeViewsFetchApi = (id) => {
    let Data = {
        videoURL: "Null",
        videoTime: 0,
        videoOwnerID: 0,
        clientID: 0,
        CoinsNumber: 0,
        ItemID: 0,
        ItemPreviousViews : 0
    };
    fetch(GlobalHostURL+'/Home/GetAllYouTubeViews?id=' + id)
        .then(response => response.json())
        .then(data => { JSON.stringify(data); Data["videoURL"] = data["videoURL"]; Data["videoTime"] = data["videoTime"]; Data["videoOwnerID"] = data["videoOwnerID"]; Data["clientID"] = data["clientID"]; Data["CoinsNumber"] = data["coinsNumber"]; Data["ItemID"] = data["itemID"]; Data["ItemPreviousViews"] = data["itemPreviousViews"]; })
        .then(data => {
            console.log(Data);
            if (Data["videoOwnerID"] != 0) {
                window.open(Data["videoURL"], '_blank');
                document.querySelector(".TakeAction>p").innerHTML = "Wait " + Data["videoTime"] + " Seconds Please ";
               if (Data["videoTime"] > 0) {
                   myVar = setTimeout(function () {
                       fetch(GlobalHostURL +'/Home/ConfirmationYoutubeViews?URL=' + Data["videoURL"] + '&PreviousViews=' + Data["ItemPreviousViews"] )
                           .then(response => response.json())
                           .then(data => {
                               JSON.stringify(data);
                               console.log(data["returnMessage"]);
                               if (data["returnMessage"] == "Done") {
                                   document.querySelector(".TakeAction>p").innerHTML = "Done";
                                   document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(Data["CoinsNumber"])).toString();
                                   fetch(GlobalHostURL +'/Home/ViewsDone?ClientID=' + Data["clientID"] + '&VideoOwnerID=' + Data["videoOwnerID"] + '&CoinsNumber=' + Data["CoinsNumber"] + '&ItemID2=' + Data["ItemID"], + '&Redirection=' + 'Views')
                                       .then(response => response.json())
                                       .then(data => { JSON.stringify(data); Result = data })
                               } else {
                                   document.querySelector(".TakeAction>p").innerHTML = "Wait One Minute Please";
                                   myVar2 = setTimeout(function () {
                                       fetch(GlobalHostURL +'/Home/ConfirmationYoutubeViews?URL=' + Data["videoURL"] + '&PreviousViews=' + Data["ItemPreviousViews"])
                                           .then(response => response.json())
                                           .then(data => {
                                               JSON.stringify(data);
                                               document.querySelector(".TakeAction>p").innerHTML = "Done";
                                               document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(Data["CoinsNumber"])).toString();
                                               fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + Data["clientID"] + '&TheVideoOwnerID=' + Data["videoOwnerID"] + '&TheCoinsNumber=' + Data["CoinsNumber"] )
                                                   .then(response => response.json())
                                                   .then(data => {
                                                       JSON.stringify(data);
                                                       fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + Data["clientID"] + '&ItemmID=' + Data["ItemID"] + '&Direction=' + "Views")
                                                           .then(response => response.json())
                                                           .then(data => { JSON.stringify(data); console.log("Done") })
                                                   })
                                           })
                                   }, 20 * 1000)
                               }
                           })
                   }, Data["videoTime"] * 1000);
                }
            } else {
                document.querySelector(".ListIsEmpty").style.top = "20px";
                document.querySelector(".PlaceHolder").style.display = "block";
            }
        });
}

let YoutubeLikesFetchApi = (id) => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Confirm Like Action ";
    let Data = {
        videoURL: "Null",
        videoTime: 0,
        videoOwnerID: 0,
        clientID: 0,
        CoinsNumber: 0,
        ItemID: 0,
        ItemPreviousViews: 0
    };

    fetch(GlobalHostURL +'/Home/GetAllYouTubeLikes?id=' + id)
        .then(response => response.json())
        .then(data => { JSON.stringify(data); Data["videoURL"] = data["videoURL"]; Data["videoTime"] = data["videoTime"]; Data["videoOwnerID"] = data["videoOwnerID"]; Data["clientID"] = data["clientID"]; Data["CoinsNumber"] = data["coinsNumber"]; Data["ItemID"] = data["itemID"]; Data["ItemPreviousViews"] = data["itemPreviousViews"]; })
        .then(data => {
            if (Data["videoOwnerID"] != 0) {
                GeneralData["videoURL"] = Data["videoURL"];
                GeneralData["videoTime"] = Data["videoTime"];
                GeneralData["videoOwnerID"] = Data["videoOwnerID"];
                GeneralData["clientID"] = Data["clientID"];
                GeneralData["CoinsNumber"] = Data["CoinsNumber"];
                GeneralData["ItemID"] = Data["ItemID"];
                GeneralData["ItemPreviousViews"] = Data["ItemPreviousViews"];
                window.open(Data["videoURL"]);
                document.querySelector(".ConfirmIcon").style.top = "20px";
                document.querySelector(".PlaceHolder2").style.display = "block";
            } else {
                document.querySelector(".ListIsEmpty").style.top = "20px";
                document.querySelector(".PlaceHolder").style.display = "block";
            }
        });
}
let ConfirmLikeYoutube = () => {
    if (GeneralData["ItemPreviousViews"] > 1000) {
        document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
        myVar = setTimeout(function () {
            document.querySelector(".ConfirmIcon").style.top = "-200%";
            document.querySelector(".PlaceHolder2").style.display = "none";
        }, 2 * 1000);
        document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
        fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Likes")
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
     })
     
    }
    else {
        fetch(GlobalHostURL +'/Home/ConfirmYoutubeLikes?URL=' + GeneralData["videoURL"] + '&PreviousLikes=' + GeneralData["ItemPreviousViews"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                if (data["returnMessage"] == "Done") {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
                    myVar = setTimeout(function () {
                        document.querySelector(".ConfirmIcon").style.top = "-200%";
                        document.querySelector(".PlaceHolder2").style.display = "none";
                    }, 2 * 1000);
                    document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
                    fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
                        .then(response => response.json())
                        .then(data => {
                            JSON.stringify(data);
                            fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Likes")
                                .then(response => response.json())
                                .then(data => { JSON.stringify(data); console.log("Done") }) })
                    
                }
                else {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Please Press Like :) ";
                }
            });
    }
    
}
let SkipLikeVideo = () => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Skiped :( ";
    myVar = setTimeout(function () {
        document.querySelector(".ConfirmIcon").style.top = "-200%";
        document.querySelector(".PlaceHolder2").style.display = "none";
    }, 2 * 1000);   
    fetch(GlobalHostURL +'/Home/FailedViewAction?ItemID=' + GeneralData["ItemID"] + '&ClientID=' + GeneralData["clientID"] + '&Redirection=' + 'Likes')
        .then(response => response.json())
        .then(data => { JSON.stringify(data); console.log("Fauld") })
}
let HideAllForms3 = () => {
    document.querySelector(".ConfirmIcon").style.top = "-200%";
    document.querySelector(".PlaceHolder2").style.display = "none";
}
let YoutubeCommentsFetchApi = (id) => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Confirm Comment Action ";
    let Data = {
        videoURL: "Null",
        videoTime: 0,
        videoOwnerID: 0,
        clientID: 0,
        CoinsNumber: 0,
        ItemID: 0,
        ItemPreviousViews: 0
    };

    fetch(GlobalHostURL +'/Home/GetAllYouTubeComments?id=' + id)
        .then(response => response.json())
        .then(data => { JSON.stringify(data); Data["videoURL"] = data["videoURL"]; Data["videoTime"] = data["videoTime"]; Data["videoOwnerID"] = data["videoOwnerID"]; Data["clientID"] = data["clientID"]; Data["CoinsNumber"] = data["coinsNumber"]; Data["ItemID"] = data["itemID"]; Data["ItemPreviousViews"] = data["itemPreviousViews"]; })
        .then(data => {
            if (Data["videoOwnerID"] != 0) {
                GeneralData["videoURL"] = Data["videoURL"];
                GeneralData["videoTime"] = Data["videoTime"];
                GeneralData["videoOwnerID"] = Data["videoOwnerID"];
                GeneralData["clientID"] = Data["clientID"];
                GeneralData["CoinsNumber"] = Data["CoinsNumber"];
                GeneralData["ItemID"] = Data["ItemID"];
                GeneralData["ItemPreviousViews"] = Data["ItemPreviousViews"];
                window.open(Data["videoURL"]);
                document.querySelector(".ConfirmIcon").style.top = "20px";
                document.querySelector(".PlaceHolder2").style.display = "block";
            } else {
                document.querySelector(".ListIsEmpty").style.top = "20px";
                document.querySelector(".PlaceHolder").style.display = "block";
            }
        });
}
let ConfirmCommentsYoutube = () => {
    if (GeneralData["ItemPreviousViews"] > 1000) {
        document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
        myVar = setTimeout(function () {
            document.querySelector(".ConfirmIcon").style.top = "-200%";
            document.querySelector(".PlaceHolder2").style.display = "none";
        }, 3 * 1000);
        document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
        fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Comments")
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            })
    } else {
        fetch(GlobalHostURL +'/Home/ConfirmYoutubeComments?URL=' + GeneralData["videoURL"] + '&PreviousLikes=' + GeneralData["ItemPreviousViews"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                if (data["returnMessage"] == "Done") {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
                    myVar = setTimeout(function () {
                        document.querySelector(".ConfirmIcon").style.top = "-200%";
                        document.querySelector(".PlaceHolder2").style.display = "none";
                    }, 3 * 1000);
                    document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
                    fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
                        .then(response => response.json())
                        .then(data => {
                            JSON.stringify(data);
                            fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Comments")
                                .then(response => response.json())
                                .then(data => { JSON.stringify(data); console.log("Done") })
                        })
                }
                else {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Please Write Comment :) ";
                }
            });
    }
  
}
let SkipCooemtsVideo = () => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Skiped :( ";
    myVar = setTimeout(function () {
        document.querySelector(".ConfirmIcon").style.top = "-200%";
        document.querySelector(".PlaceHolder2").style.display = "none";
    }, 3 * 1000);   
    fetch(GlobalHostURL +'/Home/FailedViewAction?ItemID=' + GeneralData["ItemID"] + '&ClientID=' + GeneralData["clientID"] + '&Redirection=' + 'Comments')
        .then(response => response.json())
        .then(data => { JSON.stringify(data); console.log("Fauld") })
}
let SkipSubscribesVideo = () => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Skiped :( ";
    myVar = setTimeout(function () {
        document.querySelector(".ConfirmIcon").style.top = "-200%";
        document.querySelector(".PlaceHolder2").style.display = "none";
    }, 5 * 1000);
    fetch(GlobalHostURL +'/Home/FailedViewAction?ItemID=' + GeneralData["ItemID"] + '&ClientID=' + GeneralData["clientID"] + '&Redirection=' + 'Subscribes')
        .then(response => response.json())
        .then(data => { JSON.stringify(data); console.log("Fauld") })
}
let YoutubeChannelSubscribeApi = (id) => {
    document.querySelector(".ConfirmIcon p").innerHTML = "Confirm Comment Action ";
    let Data = {
        videoURL: "Null",
        videoTime: 0,
        videoOwnerID: 0,
        clientID: 0,
        CoinsNumber: 0,
        ItemID: 0,
        ItemPreviousViews: 0
    };
    fetch(GlobalHostURL +'/Home/GetAllYouTubeChannelSubscribes?id=' + id)
        .then(response => response.json())
        .then(data => { JSON.stringify(data); Data["videoURL"] = data["videoURL"]; Data["videoTime"] = data["videoTime"]; Data["videoOwnerID"] = data["videoOwnerID"]; Data["clientID"] = data["clientID"]; Data["CoinsNumber"] = data["coinsNumber"]; Data["ItemID"] = data["itemID"]; Data["ItemPreviousViews"] = data["itemPreviousViews"]; })
        .then(data => {
            if (Data["videoOwnerID"] != 0) {
                GeneralData["videoURL"] = Data["videoURL"];
                GeneralData["videoTime"] = Data["videoTime"];
                GeneralData["videoOwnerID"] = Data["videoOwnerID"];
                GeneralData["clientID"] = Data["clientID"];
                GeneralData["CoinsNumber"] = Data["CoinsNumber"];
                GeneralData["ItemID"] = Data["ItemID"];
                GeneralData["ItemPreviousViews"] = Data["ItemPreviousViews"];
                window.open(Data["videoURL"]);
                document.querySelector(".ConfirmIcon").style.top = "20px";
                document.querySelector(".PlaceHolder2").style.display = "block";
            } else {
                document.querySelector(".ListIsEmpty").style.top = "20px";
                document.querySelector(".PlaceHolder").style.display = "block";
            }
        });
}
let ConfirmSubscribesYoutube = () => {
    if (GeneralData["ItemPreviousViews"] > 1000) {
        document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
        myVar = setTimeout(function () {
            document.querySelector(".ConfirmIcon").style.top = "-200%";
            document.querySelector(".PlaceHolder2").style.display = "none";
        }, 3 * 1000);
        document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
        fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Subscribes")
                    .then(response => response.json())
                    .then(data => { JSON.stringify(data); console.log("Done") })
            })
    }
    else {
        fetch(GlobalHostURL +'/Home/ConfirmYoutubeSubscribes?URL=' + GeneralData["videoURL"] + '&PreviousLikes=' + GeneralData["ItemPreviousViews"])
            .then(response => response.json())
            .then(data => {
                JSON.stringify(data);
                if (data["returnMessage"] == "Done") {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Done :) ";
                    myVar = setTimeout(function () {
                        document.querySelector(".ConfirmIcon").style.top = "-200%";
                        document.querySelector(".PlaceHolder2").style.display = "none";
                    }, 5 * 1000);
                    document.querySelector(".Coins>p").innerHTML = (parseInt(document.querySelector(".Coins>p").innerHTML) + parseInt(GeneralData["CoinsNumber"])).toString();
                    fetch(GlobalHostURL +'/Home/ViewsDone?TheClientID=' + GeneralData["clientID"] + '&TheVideoOwnerID=' + GeneralData["videoOwnerID"] + '&TheCoinsNumber=' + GeneralData["CoinsNumber"])
                        .then(response => response.json())
                        .then(data => {
                            JSON.stringify(data);
                            fetch(GlobalHostURL +'/Home/VerifyOperation?TheClientID=' + GeneralData["clientID"] + '&ItemmID=' + GeneralData["ItemID"] + '&Direction=' + "Subscribes")
                                .then(response => response.json())
                                .then(data => { JSON.stringify(data); console.log("Done") })
                        })
                }
                else {
                    document.querySelector(".ConfirmIcon p").innerHTML = "Please Make Subscribe :) ";
                }
            });
    }
  
}
let DeleteThisItem = (ItemID , YoutubeActionType , clientID) => {
    fetch(GlobalHostURL + '/Home/DeleteYoutubeItem?UserId=' + clientID + '&ItemID=' + ItemID + '&ActionType=' + YoutubeActionType)
        .then(response => response.json())
        .then(data => {
            JSON.stringify(data);
})
}