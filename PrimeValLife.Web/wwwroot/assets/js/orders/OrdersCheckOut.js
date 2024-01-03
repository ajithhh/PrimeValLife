const address1 = document.getElementById("address1")
const address2 = document.getElementById("address2")
const state = document.getElementById("state")
const city = document.getElementById("city")
const postalCode = document.getElementById("postalCode")
const shippingContact = document.getElementById("phone")
const btnPlaceOrder = document.getElementById("placeOrder")

//EVENTLISTENER
document.addEventListener("load", () => {
    if (address) {
        bindUserAddress();
    }
})
btnPlaceOrder.addEventListener("click",placeOrder)

//BINDING 
function bindUserAddress() {
    address1.value = address.addressLine1
    address2.value = address.addressLine2
    state.value = address.state
    city.value = address.city
    postalCode.value = address.postalCode
    shippingContact = address.Phone
}

function placeOrder() {
    //check current availability
    //Confirm Address
    confirmShippingAddress(getActiveShippingAddress());
    //Confirm Payment Method
    //Post Order 
    primeOrderRequest(getActiveCheckOut());
}

function getActiveCheckOut(){
    return {
        UserId = 1,
        ProductId =Checkout.ProductId,
        SKU = Checkout.SKU,
        Quantity = Checkout.Quantity,
        ShippingAddressId = 1,
        PaymentMethod ="COD",
    }
}

function getActiveShippingAddress() {
    return {
        AddressLine1:address1.value,
        AddressLine2:address2.value,
        City:city.value,
        State:state.value,
        ZipCode:postalCode.value
    }
}
