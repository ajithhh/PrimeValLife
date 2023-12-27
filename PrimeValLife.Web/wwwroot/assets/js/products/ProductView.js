//VARIABLES
let qtyUp = document.getElementById("qty-up")
let qtyDown = document.getElementById("qty-down")
let qtyVal = document.getElementById("qty-val")

//INITIALIZATION
function _initialize() {
    qtyVal.innerText=1
}

function addToCart(event) {
    if (product) {
        const newItem = {
            cartId: cartId,
            SKU: product.product.SKU,
            productId: product.product.productId,
            quantity:parseInt(qtyVal.innerText)
        };

        //Call API 
        //updateCartUI();
    }
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