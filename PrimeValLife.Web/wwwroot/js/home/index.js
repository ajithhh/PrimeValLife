function requestAddToCart(data, loadCartItems) {
    fetch(addToCartUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    }).then(function (response) {
        // Handle response you get from the API
        if (response.ok) {
            return response.json()
        }
    }).then(data => {
        if (data.success) {
        } else {
            alert("Error Occurred")
        }
    });
}

function getBuyerItems(e) {
    e=e.target.closest(".product-cart-wrap")

    return {
        ProductId: e.querySelector("#productId").value,
        ProductVariationId: e.querySelector("#productVariationId").value,
        Quantity: 1
    }
}

function addToCart(e) {
    requestAddToCart(getBuyerItems(e),loadCartItems);
    
}
document.addEventListener("click",  (e) => {
    if (e.target.className.includes("addToCart")) {
        buttonLoader(e.target, "Adding to Cart...").then(() => {
            addToCart(e)
            
        }).then(() => {
            loadCartItems();
            buttonLoader(e.target,undefined,"Add to Cart",1)
        })
        
    }
})
