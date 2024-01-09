//VARIABLES
const floatingCart = document.querySelector("#floatingCart")

//INITIALIZATION
loadCartItems();

//EVENT LISTENERS
floatingCart.addEventListener("click",(e) => {
    if (e.target.className.includes("removeCitem")) {
        removeItem(e)
    }
})

// Fetch data from the API
function loadCartItems() {
    fetch(getActiveCartItemsUrl, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
            
        }
    })
        .then(response => response.json())
        .then(data => {
            data=data.data.$values
            // Process the data and display it
                displayCartItems(data); 
        })
        .catch(error => console.error('Error fetching cart data:', error));
}
function removeItem(e) {
    let itemId = e.target.getAttribute("data-target")
    // Implement logic to remove the item with the given ID from the cart
    // You may need to update the cart on the server and then re-fetch the updated data
    fetch(removeCartItemUrl + `?id=${itemId}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json',
        }
    })
    loadCartItems()
}

function displayCartItems(cartItems) {
    const cartItemList = document.getElementById('cartItemList');
    const cartTotalElement = document.getElementById('cartTotal');

    // Clear previous items
    cartItemList.innerHTML = '';

    // Iterate over the cart items and create the structure dynamically
    cartItems.forEach(item => {
        const listItem = document.createElement('li');

        listItem.innerHTML = `
        <div class="shopping-cart-img">
          <a href="products/productView?id=${item.product.productId}"><img alt="${item.product.name}" src="${item.product.primaryImageUrl}" /></a>
        </div>
        <div class="shopping-cart-title">
          <h4><a href="products/productView?id=${item.product.productId}">${item.product.productName}</a></h4>
          <h4><span>${item.quantity} × </span>${item.product.price *item.quantity}</h4>
        </div>
        <div class="shopping-cart-delete">
          <a class="removeCitem" data-target="${item.cartItemId}"><i data-target="${item.cartItemId}" class="fi-rs-cross-small removeCitem"></i></a>
        </div>
      `;

        cartItemList.appendChild(listItem);
    });

    // Calculate and display the total
    const total = cartItems.reduce((sum, item) => sum + (item.quantity * item.product.price), 0);
    cartTotalElement.textContent = `$${total.toFixed(2)}`;
}

