//VARIABLES
let qtyUp = document.getElementById("qty-up")
let qtyDown = document.getElementById("qty-down")
let qtyVal = document.getElementById("qty-val")
let btnAddToCart = document.getElementById("addToCart")
let btnBuyNow = document.getElementById("buyNow")
//INITIALIZATION
function _initialize() {
    qtyVal.innerText=1
}


//EVENTLISTENERS
qtyUp.addEventListener("click", (e) => {
    e.preventDefault()
    var parsedQty = parseInt(qtyVal.innerText);
    if ((parsedQty + 1) <= product.product.stockQuantity) {
        qtyVal.innerText = parsedQty + 1;
    } else {
        qtyVal.innerText = parsedQty;
        ShowToaster("Maximum quantity limit reached", "error");
    }
})
qtyDown.addEventListener("click", (e) => {
    e.preventDefault()
    var parsedQty = parseInt(qtyVal.innerText);
    if ((parsedQty - 1) >= 1) {
        qtyVal.innerText = parsedQty - 1;
    } else {
        qtyVal.innerText = parsedQty;
    }
})
btnBuyNow.addEventListener("click", addToCart)
btnBuyNow.addEventListener("click", (e) => {
    window.location.href="orders/orderscheckout"
})
btnAddToCart.addEventListener("click",addToCart)