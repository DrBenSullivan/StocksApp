$(document).ready(function () {
    const finnhubKey = $('#finnhub-api-key').val();
    const stockSymbol = $('#stock-symbol').text();
    let newPrice;

    if (!finnhubKey || !stockSymbol) {
        console.error('API key or stock symbol is missing.');
        return;
    }

    const socket = new WebSocket(`wss://ws.finnhub.io?token=${finnhubKey}`);

    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': `${stockSymbol}` }));
    });

    socket.addEventListener('message', function (event) {
        const responseObject = JSON.parse(event.data);

        if (responseObject.error) {
            console.error(`Response error: ${responseObject.msg}`);
            newPrice = responseObject.msg;
        } else if (responseObject.data && responseObject.data[0].p && responseObject.data[0].p != newPrice) {
            newPrice = responseObject.data[0].p.toFixed(2);
        }

        $('#stock-price').text(newPrice.toString());
        $('#order-form-price').prop("value", newPrice);
        
    });

    socket.addEventListener('error', function (event) {
        console.error(`Finnhub WebSocket error: ${event}`);
    });

    socket.addEventListener('close', function (event) { });

    window.addEventListener('beforeunload', function () {
        if (socket.readyState === WebSocket.OPEN) {
            socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': `${stockSymbol}` }));
            socket.close();
            return;
        }
    });
});
