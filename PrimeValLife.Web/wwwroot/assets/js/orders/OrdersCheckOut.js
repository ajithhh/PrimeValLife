const billingAddress = document.getElementById("billingAddress")
const billingAddress2 = document.getElementById("billingAddress2")
const billingState = document.getElementById("billingState")
const billingCity = document.getElementById("billingCity")
const billingPostCode = document.getElementById("billingPostCode")
const billingPhone = document.getElementById("billingPhone")
const shippingAddress = document.getElementById("shippingAddress")
const shippingAddress2 = document.getElementById("shippingAddress2")
const shippingState = document.getElementById("shippingState")
const shippingCity = document.getElementById("shippingCity")
const shippingPostCode = document.getElementById("shippingPostCode")
const shippingPhone = document.getElementById("shippingPhone")
const paymentMethod = document.querySelector('input[name="payment_option"]:checked')
const btnPlaceOrder = document.getElementById("placeOrder")

//EVENTLISTENER
document.addEventListener("load", () => {
    if (address) {
        bindCheckOutDetails();
    }
})
btnPlaceOrder.addEventListener("click", placeOrder)

//BINDING 
function bindCheckOutDetails() {
    if (checkOutModel.billingAddress) {
        billingAddress = checkOutModel.billingAddress.addressLine1
        billingAddress2 = checkOutModel.billingAddress.addressLine2
        billingState = checkOutModel.billingAddress.state
        billingCity = checkOutModel.billingAddress.city
        billingPostCode = checkOutModel.billingAddress.zipCode
        billingPhone = checkOutModel.billingAddress.Phone

    }
    if (checkOutModel.shippingAddress) {
        shippingAddress = checkOutModel.shippingAddress.addressLine1
        shippingAddress2 = checkOutModel.shippingAddress.addressLine2
        shippingState = checkOutModel.shippingAddress.state
        shippingCity = checkOutModel.shippingAddress.city
        shippingPostCode = checkOutModel.shippingAddress.zipCode
        shippingPhone = checkOutModel.shippingAddress.Phone
    }

}

function placeOrder() {
    //check current availability
    //Confirm Address
    confirmShippingAddress(getActiveShippingAddress());
    //Confirm Payment Method
    //Post Order 
    primeOrderRequest(getActiveCheckOut());
}

function currentCartDetails(){


    
}

function getCheckOutDetailsFromUser() {
    var billingAddressDetails = {
        AddressLine1: billingAddress.value,
        AddressLine2: billingAddress2.value,
        City: billingCity.value,
        State: billingState.value,
        ZipCode: billingPostCode.value,
    };

    if (differentShippingAddress) {
        return {
            BillingAddress: billingAddressDetails,
            ShippingAddress: {
                AddressLine1: shippingAddress.value,
                AddressLine2: shippingAddress2.value,
                City: shippingCity.value,
                State: shippingState.value,
                ZipCode: shippingPostCode.value,
            }
        };
    } else {
        return {
            BillingAddress: billingAddressDetails,
            ShippingAddress: billingAddressDetails  // Use billing address details for shipping if same as billing
        };
    }
}
