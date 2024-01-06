
function primeOrderRequest(data) {
    fetch(orderRequestUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
        .then(function (response) {
            // Handle response you get from the API
            if (response.body.Authorized) {
                alert("Order Placed Successfully")
            } else {
                alert("Order Creation failed")
            }
        });
}

function confirmShippingAdddress(data) {
    let ConfirmOrderRequest = new Request(baseUrl + confirmShippingInfoUrl, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json; charset=UTF-8'
        })
    });

    fetch(request)
        .then(function (response) {
            // Handle response you get from the API
            if (response.body.AddressId > 0) {
                alert(" Successfully Added shipping Address")
            } else {
                alert("Address Could not be added")
            }
        });
    }