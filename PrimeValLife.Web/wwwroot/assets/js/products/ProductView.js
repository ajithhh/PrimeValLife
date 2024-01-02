﻿//VARIABLES
let qtyUp = document.getElementById("qty-up")
let qtyDown = document.getElementById("qty-down")
let qtyVal = document.getElementById("qty-val")
let btnAddToCart = document.getElementById("addToCart")
let btnBuyNow = document.getElementById("buyNow")
//INITIALIZATION
function _initialize() {
    qtyVal.innerText=1
}
function getBuyerItems() {
    return {    
            ProductId: product.product.productId,
            Quantity: parseInt(qtyVal.innerText)
    }
}

function addToCart() {
    requestAddToCart(getBuyerItems());
    //updateCartUI();
}


//EVENTLISTENERS
qtyUp.addEventListener("click", () => {
    var parsedQty = parseInt(qtyVal.innerText);

    if ((parsedQty + 1) <= product.product.stockQuantity) {
        qtyVal.innerText = parsedQty + 1;
    } else {
        qtyVal.innerText = parsedQty;
        ShowToaster("Maximum quantity limit reached", "error");
    }
})
qtyDown.addEventListener("click", () => {
    var parsedQty = parseInt(qtyVal.innerText);

    if ((parsedQty - 1) >= 1) {
        qtyVal.innerText = parsedQty - 1;
    } else {
        qtyVal.innerText = parsedQty;
    }
})
btnBuyNow.addEventListener("click",buyNow)
btnAddToCart.addEventListener("click",addToCart)