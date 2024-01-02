function requestBuyNow(data) {
    fetch(directCheckOutUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    }).then(function (response) {
            // Handle response you get from the API
            if (response.body.Authorized) {
                alert("Order Placed Successfully")
            } else {
                alert("Order Creation failed")
            }
        });

}
function requestAddToCart(data) {
    fetch(addToCartUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    }).then(function (response) {
        // Handle response you get from the API
        if (response.body.success) {
            alert("Item added to cart")
        } else {
            alert("Error Occurred")
        }
    });

}
