$(document).ready(function () {
    const finnhubKey = $('#finnhub-api-key').val();
    const stockSymbol = $('#stock-symbol').html();

    const socket = new WebSocket(`wss://ws.finnhub.io?token=${finnhubKey}`);
    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': `${stockSymbol}` }))
    });
    socket.addEventListener('message', function (event) {
        const jsonTrades = JSON.parse(event.data);
        console.log(jsonTrades);
    });

    window.addEventListener('beforeunload', function () {
        socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': `${stockSymbol}` }));
        socket.close();
    });
});