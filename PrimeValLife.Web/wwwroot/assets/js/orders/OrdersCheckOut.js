const billingfname = document.getElementById("billingFName")
const billinglname = document.getElementById("billingLName")
const shippingfname = document.getElementById("shippingFName")
const shippinglname = document.getElementById("shippingLName")
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
const btnPlaceOrder = document.getElementById("btnPlaceOrder")
const cart = document.getElementById("cart")
let cartElements = document.querySelectorAll(".cartProduct");
let qtyUps = document.querySelectorAll("#qty-up")
let qtyDowns = document.querySelectorAll("#qty-down")
//EVENTLISTENER
document.addEventListener("load", () => {
    if (address) {
        bindCheckOutDetails();
    }
})
btnPlaceOrder.addEventListener("click", placeOrder)
cartElements.forEach((ele) => {
    ele.querySelector("#qty-up").addEventListener("click", (e) => {
        e.preventDefault()
        
            ele.querySelector("#quantity").value =Math.min(parseInt(ele.querySelector("#quantity").value)+1,10)
        ele.querySelector("#totalPrice").innerText = (parseFloat(ele.querySelector("#unitPrice").innerText) * parseFloat(ele.querySelector("#quantity").value)).toFixed(2)
    })
    ele.querySelector("#qty-down").addEventListener("click", (e) => {
        e.preventDefault()
        
        ele.querySelector("#quantity").value = Math.max(parseInt(ele.querySelector("#quantity").value) - 1, 1)
        ele.querySelector("#totalPrice").innerText = (parseFloat(ele.querySelector("#unitPrice").innerText) * parseFloat(ele.querySelector("#quantity").value)).toFixed(2)
    })

})
cart.addEventListener("click", (e) => {
    if (e.target.classList.contains("removeCitem")) {
        removeItem(e);
        window.location.reload();
    }
   }
);
//BINDING 
function bindCheckOutDetails() {
    //Cart Details binded through razor
    if (checkOutModel.billingAddress) {
        billingfname.value = checkOutModel.billingAddress.user.firstName
        billinglname.value = checkOutModel.billingAddress.user.lastName
        billingAddress.value = checkOutModel.billingAddress.addressLine1
        billingAddress2.value = checkOutModel.billingAddress.addressLine2
        /*billingState.value = checkOutModel.billingAddress.state*/
        billingCity.value = checkOutModel.billingAddress.city
        billingPostCode.value = checkOutModel.billingAddress.zipCode
        billingPhone.value = checkOutModel.billingAddress.Phone

    }
    //if (checkOutModel.shippingAddress) {
    //    shippingAddress.value = checkOutModel.shippingAddress.addressLine1
    //    shippingAddress2.value = checkOutModel.shippingAddress.addressLine2
    //    shippingState.value = checkOutModel.shippingAddress.state
    //    shippingCity.value = checkOutModel.shippingAddress.city
    //    shippingPostCode.value = checkOutModel.shippingAddress.zipCode
    //    shippingPhone.value = checkOutModel.shippingAddress.Phone
    //}

}

function placeOrder(e) {
    //check current availability
    //Confirm Address
    //confirmShippingAddress(getActiveShippingAddress());
    //Confirm Payment Method
    //Post Order 
    e.preventDefault()
    primeOrderRequest(getCheckOutDetailsFromUser());
}

function currentCartDetails() {
    let cartProducts = [];
    cartElements.forEach((element) => {
        if (element.querySelector("#productCheckbox:checked")) {
            let cartProduct = {
                ProductId: element.querySelector("#productId").value,
                Quantity: element.querySelector("#quantity").value
            }
            cartProducts.push(cartProduct)
        }
       
    })
    return cartProducts
}

function getCheckOutDetailsFromUser() {
    let paymentMethod = document.querySelector("input[name='payment_option']:checked").value
    let createNewAccount = document.querySelector("input[id='createaccount']").checked
    let differentShippingAddress = document.querySelector("input[id='differentaddress']").checked
    let passwordAttached;
    let emailAttached;
    if (createNewAccount) {
        passwordAttached = document.querySelector("input[id='passwordAttached']").value
        emailAttached = document.querySelector("input[id='emailAttached']").value
    }   
    var billingAddressDetails = {
        AddressLine1: billingAddress.value,
        AddressLine2: billingAddress2.value,
        FName: billingfname.value,
        LName: billinglname.value,
        City: billingCity.value,
        Phone:billingPhone.value,
        /*State: billingState.value,*/
        ZipCode: billingPostCode.value,
    };

    if (differentShippingAddress) {
        return {
            BillingAddress: billingAddressDetails,
            ShippingAddress: {
                AddressLine1: shippingAddress.value,
                AddressLine2: shippingAddress2.value,
                FName: shippingfname.value,
                LName: shippinglname.value,
                City: shippingCity.value,
                Phone: shippingPhone.value,
                /*State: shippingState.value,*/
                ZipCode: shippingPostCode.value
            },
            CartProducts: currentCartDetails(),
            PaymentMethod: paymentMethod,
            CreateNewAccount: createNewAccount,
            PasswordAttached: passwordAttached,
            EmailAttached: emailAttached
            
        };
    } else {
        return {
            BillingAddress: billingAddressDetails,
            ShippingAddress: billingAddressDetails,
            CartProducts: currentCartDetails(),
            PaymentMethod: paymentMethod,
            CreateNewAccount: createNewAccount,
            PasswordAttached: passwordAttached,
            EmailAttached: emailAttached
        };
    }
}
