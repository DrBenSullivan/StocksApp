$(document).ready(function () {
    const finnhubKey = $('#finnhub-api-key').val();
    const stockSymbol = $('#stock-symbol').text();
    console.log(`Key: ${finnhubKey}, Stock: ${stockSymbol}`);
    let newPrice;

    if (!finnhubKey || !stockSymbol) {
        console.error('API key or stock symbol is missing.');
        return;
    }

    const socket = new WebSocket(`wss://ws.finnhub.io?token=${finnhubKey}`);

    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': `${stockSymbol}` }));
        console.log(`Websocket opened, awaiting API response.`);
    });

    socket.addEventListener('message', function (event) {
        const responseObject = JSON.parse(event.data);
        console.log(`Response received: ${event.data}`);

        if (responseObject.error) {
            console.error(`Response error: ${responseObject.msg}`);
            newPrice = responseObject.msg;
        } else if (responseObject.data && responseObject.data[0].p && responseObject.data[0].p != newPrice) {
            newPrice = responseObject.data[0].p.toFixed(2);
            console.log(`Valid price received: ${newPrice}`);
            $('#stock-price').text(newPrice.toString());
            $('#order-form-price').prop("value", newPrice);
        }        
    });

    socket.addEventListener('error', function (event) {
        console.error(`Finnhub WebSocket error: ${event}`);
    });

    socket.addEventListener('close', function (event) { 
        console.log(`Websocket closed.`);
    });

    window.addEventListener('beforeunload', function () {
        if (socket.readyState === WebSocket.OPEN) {
            socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': `${stockSymbol}` }));
            socket.close();
            return;
        }
    });
});
