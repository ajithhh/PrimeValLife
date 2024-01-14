
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
                addressList = response.data.$values
            } else {
            }
        });
    return new Promise((resolve, reject) => {
        // Simulating an asynchronous operation, e.g., a network request
        setTimeout(() => {
            const success = true; // Set to false to simulate an error
            if (success) {
                const addresses = addressList;
                resolve(addresses);
            } else {
                reject(new Error('Failed to fetch addresses'));
            }
        }, 1000); // Simulating a delay of 1 second
    });
}
function getAddress(id) {
    let result;
    fetch(getAddressUrl+`?id=${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    }).then((response) => { return response.json() })
        .then(function (response) {
            // Handle response you get from the API
            if (response.success) {
                result = response.data
            } else {
            }
        });
    return new Promise((resolve, reject) => {
        // Simulating an asynchronous operation, e.g., a network request
        setTimeout(() => {
            const success = true; // Set to false to simulate an error
            if (success) {
                const address = result;
                resolve(address);
            } else {
                reject(new Error('Failed to fetch addresses'));
            }
        }, 1000); // Simulating a delay of 1 second
    }); 
}



function saveAddress(data) {
    fetch(saveAddressUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    }).then((response) => { return response.json() })
        .then(function (response) {
            // Handle response you get from the API
            if (response.success) {
                alert("Address saved Successfully")
                window.location.reload()
            } else {
                alert("Address Creation failed")
            }
        });
}