var hubUrl = 'https://localhost:44316/chat';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();


hubConnection.on('SendComment', function (data) {
    let HTMLstring = `<div>${data.date} -  ${data.name}</div>\
    ${data.comment}
    <input type="button" id="like" onclick="ProcessLike('${data.Id}')" value="like!" />
    <div>${data.likes}</div>`
    $("#" + data.articleId).prepend("<li > " + HTMLstring + " </li>");

});

hubConnection.start();
