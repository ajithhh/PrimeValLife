//VARIABLES
let qtyUp = document.getElementById("qty-up")
let qtyDown = document.getElementById("qty-down")
let qtyVal = document.getElementById("qty-val")
let btnAddToCart = document.getElementById("addToCart")
let btnBuyNow = document.getElementById("buyNow")
let currentBuyerItems;
//INITIALIZATION
function _initialize() {
    qtyVal.innerText=1
}

function getBuyerItems() {
    return {
            cartId:1,
            SKU: product.product.sku,
            productId: product.product.productId,
            quantity: parseInt(qtyVal.innerText)
    }
}

function addToCart() {
    getBuyerItems()
        //Call API 
        //updateCartUI();
    
}

function buyNow() {
    requestBuyNow(getBuyerItems());
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
btnBuyNow.addEventListener("click",getBuyerItems)