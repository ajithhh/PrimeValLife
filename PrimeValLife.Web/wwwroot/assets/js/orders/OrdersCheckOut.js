﻿const billingfname = document.getElementById("billingFName")
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
const addressModal = document.querySelector("#addressModal")
const firstName = addressModal.querySelector("#firstName")
const addressId = addressModal.querySelector("#addressId")
const addressType = addressModal.querySelector("#addressType")
const lastName = addressModal.querySelector("#lastName")
const address1 = addressModal.querySelector("#address1")
const city = addressModal.querySelector("#city")
const state = addressModal.querySelector("#state")
const postalCode = addressModal.querySelector("#postalCode")
const billingAddList = document.querySelector("#billingAddressList")
const shippingAddList = document.querySelector("#shippingAddressList")
const btnSaveAddress = document.querySelector("#btnSaveAddress")
const cart = document.getElementById("cart")
const addressContainer = document.querySelector("#addressAcc")
let cartElements = document.querySelectorAll(".cartProduct");
let qtyUps = document.querySelectorAll("#qty-up")
let qtyDowns = document.querySelectorAll("#qty-down")


_init_()
function _init_() {
    loadAddressDetails();
   
}
//EVENTLISTENER

btnPlaceOrder.addEventListener("click", placeOrder)
cartElements.forEach((ele) => {
    ele.querySelector("#qty-up").addEventListener("click", (e) => {
        e.preventDefault()

        ele.querySelector("#quantity").value = Math.min(parseInt(ele.querySelector("#quantity").value) + 1, 10)
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
addressModal.addEventListener("show.bs.modal", (e) => {
        let aid = e.relatedTarget.getAttribute("data-address-target")
        bindAddressWithModal(aid)
})
btnSaveAddress.addEventListener("click", () => {
    let address = {
        FName: firstName.value,
        LName:lastName.value,
        AddressId: addressId.value,
        AddressLine1: address1.value,
        AddressType:addressType.value,
        City: city.value,
        State: state.value,
        ZipCode: postalCode.value,
        Phone:phone.value
    }
    saveAddress(address);

})


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
        Phone: billingPhone.value,
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
function  loadAddressDetails() {

    billingAddList.innerHTML = ""
    shippingAddList.innerHTML = ""
    requestAddresses().then((addressList) => {
        addressList.forEach((entry) => {
            if (entry.addressType == 'BILLING') {
                const li = document.createElement('li');
                li.innerHTML = `
                <div class="d-flex flex-row card p-3 m-1">
                    <div class="align-self-center">
                        <input class="form-check-input top-0 start-0 mt-3 ms-3" type="radio" name="billingAddress" >
                    </div>
                    <div class="ms-4 ml-4">
                        <h5 class="mb-3"><span>${entry.fName}</span> <span>${entry.lName}</span></h5>
                        <address class="mb-2">
                            ${entry.addressLine1}<br>
                            ${entry.addressLine2}<br>
                            ${entry.city}, ${entry.state}, ${entry.zipCode}<br>
                            ${entry.phone}
                        </address>
                        <a class="modify-address" data-address-target="${entry.addressId}" data-bs-toggle="modal" data-bs-target="#addressModal">Modify</a>
                    </div>
                </div>
            `;

                // Append the <li> element to the addressList
                billingAddList.appendChild(li);
            }
            else {
                const li = document.createElement('li');
                li.innerHTML = `
                <div class="d-flex flex-row card p-3 m-1">
                    <div class="align-self-center">
                        <input class="form-check-input top-0 start-0 mt-3 ms-3" type="radio" name="address" >
                    </div>
                    <div class="ms-4 ml-4">
                        <h5 class="mb-3"><span>${entry.fName}</span> <span>${entry.lName}</span></h5>   
                        <address class="mb-2">
                            ${entry.addressLine1}<br>
                            ${entry.addressLine2}<br>
                            ${entry.city}, ${entry.state}, ${entry.zipCode}<br>
                            ${entry.phone}
                        </address>
                        <a class="modify-address" data-address-target="${entry.addressId}" data-bs-toggle="modal" data-bs-target="#addressModal">Modify</a>
                    </div>
                </div>
            `;

                // Append the <li> element to the addressList
                shippingAddList.appendChild(li);

            }

        });
    })

}
function bindAddressWithModal(id) {
    if (id > 0) {
        getAddress(id).then((address) => {
            addressType.value = address.addressType
            addressId.value = address.addressId
            firstName.value = address.fName
            lastName.value = address.lName
            address1.value = address.addressLine1
            city.value = address.city
            state.value = address.state
            phone.value = address.phone
            postalCode.value = address.zipCode
        });
        
    }
    else {
        addressType.value="SHIPPING"
        addressId.value =0
        firstName.value = ""
        lastName.value = ""
        address1.value = ""
        city.value = ""
        state.value = ""
        postalCode.value = ""
    }
}