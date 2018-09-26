var hubUrl = 'https://localhost:44316/chat';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();


hubConnection.on('Send', function (data) {
    alert(data.comment + "  " + data.articleId);
});

hubConnection.start();
