
function primeOrderRequest(data) {
    fetch(orderRequestUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    }).then((response) => {return response.json() })
        .then(function (response) {
            // Handle response you get from the API
            if (response.success) {
                alert("Order Placed Successfully")
                window.location.reload()
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

function requestAddresses() {
    let addressList;
    fetch(addressRequestUrl, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }        
    }).then((response) => { return response.json() })
        .then(function (response) {
            // Handle response you get from the API
            if (response.success) {
               addressList=response.data 
            } else {
            }
        });
    return addressList;
}
function getAddress(id) {
    let address;
    fetch(addressRequestUrl+`?id=${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    }).then((response) => { return response.json() })
        .then(function (response) {
            // Handle response you get from the API
            if (response.success) {
                address = response.data
            } else {
            }
        });
    return address;
}