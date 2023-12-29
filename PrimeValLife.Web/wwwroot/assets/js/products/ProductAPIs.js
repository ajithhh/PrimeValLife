function requestBuyNow(items) {
    
}
function requestBuyNow(data) {

    let directCheckoutRequest = new Request(baseUrl + directCheckOutUrl, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json; charset=UTF-8'
        })
    });

    fetch(directCheckoutRequest)
        .then(function (response) {
            // Handle response you get from the API
            if (response.body.Authorized) {
                alert("Order Placed Successfully")
            } else {
                alert("Order Creation failed")
            }
        });
}