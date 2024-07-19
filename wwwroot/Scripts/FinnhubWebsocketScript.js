$(document).ready(function () {
    const finnhubKey = $('#finnhub-api-key').val();
    const stockSymbol = $('#stock-symbol').html();
    const openingPrice = $('#stock-price');
    let newPrice;

    console.log(`${finnhubKey}\nStock Symbol: ${stockSymbol}`);

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
        } else if (responseObject.data && responseObject.data[0].p) {
            console.log(`Valid response: ${responseObject.data[0].p}`);
            newPrice = responseObject.data[0].p.toFixed(2);
        }

        openingPrice.html(newPrice.toString());
    });

    socket.addEventListener('error', function (event) {
        console.error(`Finnhub WebSocket error: ${event}`);
    });

    socket.addEventListener('close', function (event) {
        console.log('Finnhubb WebSocket is now closed.');
    });

    window.addEventListener('beforeunload', function () {
        if (socket.readyState === WebSocket.OPEN) {
            socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': `${stockSymbol}` }));
            socket.close();
            return;
        }
    });
});
