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
const btnCreateAccount = document.querySelector("#btnCreateAccount")
const email = document.querySelector("#email")
const password = document.querySelector("#password")
const cart = document.getElementById("cart")
const addressContainer = document.querySelector("#addressAcc")
const netPayment =document.querySelector("#netPayment")
let cartElements = document.querySelectorAll(".cartProduct");
let qtyUps = document.querySelectorAll("#qty-up")
let qtyDowns = document.querySelectorAll("#qty-down")


_init_()
function _init_() {
    loadAddressDetails();
    updateCartTotal();
}
//EVENTLISTENER
cart.addEventListener("click",updateCartTotal)

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
        addressType.value=e.relatedTarget.getAttribute("data-address-type")
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
if (btnCreateAccount) {
    btnCreateAccount.addEventListener("click", (e) => {
        e.preventDefault();
        createAccountRequest = {
            Email: email.value,
            password: password.value
        };
        CreateAccount(createAccountRequest);

    })
}


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
    window.location.reload()

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

//let createNewAccount = document.querySelector("input[id='createaccount']").checked
//let passwordAttached;
//let emailAttached;
//if (createNewAccount) {
//    passwordAttached = document.querySelector("input[id='passwordAttached']").value
//    emailAttached = document.querySelector("input[id='emailAttached']").value
//}

function getCheckOutDetailsFromUser() {
    let shippingAddress = document.querySelector('.shippingAddress:checked').getAttribute("data-address-target")
    let billingAddress = document.querySelector('.billingAddress:checked').getAttribute("data-address-target")
    let paymentMethod = document.querySelector("input[name='payment_option']:checked").value

    return {
        BillingAddress: billingAddress,
        ShippingAddress: shippingAddress,
        CartProducts: currentCartDetails(),
        PaymentMethod: paymentMethod,
    }
}
function  loadAddressDetails() {

    billingAddList.innerHTML = ""
    shippingAddList.innerHTML = ""
    requestAddresses().then((addressList) => {
        if (addressList.length > 0) {
            addressList.forEach((entry) => {
                if (entry.addressType == 'BILLING') {
                    const li = document.createElement('li');
                    li.innerHTML = `
                <div class="d-flex flex-row card p-3 m-1">
                    <div class="align-self-center">
                        <input data-address-target="${entry.addressId}"class="billingAddress form-check-input top-0 start-0 mt-3 ms-3"  type="radio" name="billingAddress" checked>
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
                    document.querySelector("[data-address-type='BILLING']").hidden = true
                }
                if (document.querySelector("input .shippingAddress:checked")) {
                    document.querySelector("input .shippingAddress:checked").checked = false
                }
                
                    
                    const li = document.createElement('li');
                    li.innerHTML = `
                <div class="d-flex flex-row card p-3 m-1">
                    <div class="align-self-center">
                        <input data-address-target="${entry.addressId}" class="shippingAddress form-check-input top-0 start-0 mt-3 ms-3" type="radio" name="address" checked/>
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

               
               
            });
        }
        
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
        addressId.value =0
        firstName.value = ""
        lastName.value = ""
        address1.value = ""
        city.value = ""
        state.value = ""
        postalCode.value = ""
        phone.value=""
    }
}

function updateCartTotal() {
    let eles = cart.querySelectorAll(".totalPrice")
    let total=0;
    eles.forEach((ele) => {
        total= total + parseFloat(ele.innerText)
    })
    if (total>0) {
        netPayment.innerText = total.toFixed(2)
    }
    
}