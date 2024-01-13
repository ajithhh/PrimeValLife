//VARIABLES
const floatingCart = document.querySelector("#floatingCart")
const searchBox =document.querySelector("#globalSearch")

//INITIALIZATION
loadCartItems();
initGlobalSearch();

//EVENT LISTENERS
floatingCart.addEventListener("click",(e) => {
    if (e.target.className.includes("removeCitem")) {
        removeItem(e)
    }
})

// Fetch data from the API
function loadCartItems() {
    initTerminateLoader(floatingCart).then(() => {
        fetch(getActiveCartItemsUrl, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',

            }
        })
            .then(response => response.json())
            .then(data => {
                data = data.data.$values
                // Process the data and display it
                displayCartItems(data);
            })
            .catch(error => console.error('Error fetching cart data:', error));
})
    .then(() => {
        initTerminateLoader(floatingCart)
    });
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
    const cartItemCount = document.getElementById('cartItemCount');
   
    // Clear previous items
    cartItemList.innerHTML = '';
    cartItemCount.innerText = cartItems.length
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

function initTerminateLoader(floatingCart) {
    let childNodes = getLeafNodes(floatingCart);
    floatingCart.classList.toggle("placeholder-wave")
    childNodes.forEach((ele) => {
        ele.classList.toggle("placeholder")
        
    })  
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve(); 
        }, 1000); 
    });

}

function getLeafNodes(rootElement) {
    const leafNodes = rootElement.querySelectorAll(':scope *:not(:has(*))');
    return Array.from(leafNodes);
}

function initGlobalSearch() {

    sxQuery("#globalSearch").unibox({
        // these are the required:
        suggestUrl: searchUrl +"?query=", // the URL where to get the search suggests
        // these are optional:
        ivfImagePath: '', // the root path to the instant visual feedback images
        ivfImageOffset: -80, // the vertical offset of the ivf images
        missingErrorImage: undefined, // if an image is missing, hide it (undefined) or show a placeholder image
        queryVisualizationHeadline: '', // the headline of the query visualization
        highlight: true, // whether the search words should be highlighted in the results
        throttleTime: 50, // the number of ms before the update of the search box is triggered
        animationSpeed: 300, // general animation speed
        instantVisualFeedback: 'all', // where to show the ivf
        showOnMobile: true, // whether to show search suggestions on mobile devices
        callbacks: {
            enter: enterResultCallback, // the callback to trigger after 'enter' press or search button click (when no suggestion is selected) - args(query, searchButton [optional], hideSpecialCallback [optional])
            enterResult: enterResultCallback, // the callback to trigger after 'enter' press or click on selected suggestion - args(query, href, hasCtrlModifier [optional])
            type: undefined, // the callback to trigger after search box value changes - args(event, query)
            focus: undefined, // the callback to trigger after search box is focused - args(event, query)
            blur: undefined, // the callback to trigger after search box is abandoned - args(event, query)
            line: undefined, // the callback to trigger after suggest line is built - args(lineString, key, index, suggest)
            suggestsBuilt: undefined, // the callback to trigger after all suggestions are built - args(suggestBox, data)
            preSuggest: undefined, // the callback to trigger before suggestions are fetched, return 'false' to interrupt the process  - args(query, searchBox)
            suggestChange: undefined // the callback to trigger after the suggestion set is changed - args(isSuggestBoxVisible)
        },
        trackingCallbacks: {
            abandon: undefined, // a tracking callback, called after the search box is abandoned - args(query, visible suggestionCount, searchBox)
            select: undefined, // a tracking callback, called after a suggestion is selected - args(searchBox, suggestBox, target, query, suggestions, position, link)
            show: undefined, // a tracking callback, called after a suggestion set is shown - args(searchBox, suggestBox, aSuggestion, query, suggestions)
            change: undefined // a tracking callback, called after a search box value is changed - args(searchBox)
        },
        placeholder: undefined, // the placeholder for the input field
        extraHtml: undefined, // extra HTML code that is shown in each search suggest
        dataPoints: undefined, // extra HTML code by key, overrides extraHtml, e.g. {price: {html: '<span>#price# $</span>', position: 1}, category: {html: '<b>#category#</b>', position: 2}}
        noSuggests: undefined, // the content to show when no suggests are available, if undefined, no suggests will be shown
        emptyQuerySuggests: undefined, // empty query suggests, if someone clicks in the search field, we can show suggests
        minChars: 3, // the minimum input before the suggest pops up
        maxWidth: 'auto', // the maximum width of the suggest box, default: as wide as the input box
        showDeleteAllButton: false, // show 'delete all' (x) button when focus hits back to input field
        showImagesSuggestions: true, // whether to show images
        disableEventPropagationHtml: true, // disable click event propagation to HTML element   
        suggestOrder: [], // sort suggests by this array, if empty, use given array order
        suggestSelectionOrder: [], // move through selectables by this cluster order. if empty, use naturally given order by selectables
        viewAllLabel: undefined, // label of the 'View All' button, if undefined no 'View All' button will be shown at the bottom of suggestion list
        loaderSelector: undefined, // the loader element selector
        viewKeyMappings: undefined, // mapping of suggestion group key to view key
        themeColor: '#1C5D7D', // theme color, used for magnifier icon when using 'View All' button
        enabled: true, // whether suggestions should be shown
        specialMobileSuggest: {
            enabled: false,  // whether to show fullscreen search box + suggest box on search field focus when the viewport width is below specified breakpoint, default: false
            breakpoint: 768, // the maximum width of device, where special mobile suggestion should be shown, default: 768 px
            placeholder: undefined, // html/string to be shown when there are no suggest results in special mobile suggest box
            customTopHtml: undefined,  // html/string to be shown at the top of the page when special mobile suggests are visible
            searchBoxPlaceholder: undefined, // the placeholder to show in mobile suggestion search box
            animateTransitions: true, // whether to animate transition into special mobile suggestions
            resizeSearchBoxOnScroll: true, // whether to resize mobile special input block (search field + icons) on special mobile suggest box scroll
            trigger: undefined, // selector for special mobile suggest trigger, if the trigger is clicked, the fullscreen suggestion layer will be shown
            autoHide: true, // whether to hide the mobile layer automatically on search submission, if set to false, the enterCallback becomes a hideLayer callback as 3rd parameter, which has to be called in order to hide the mobile suggestions
            hiddenCallback: undefined // a callback that is called after the special mobile suggestions have been hidden
        },
        accessibility: {
            headingLevel: 2, // the level of search suggestion heading
            searchFieldLabel: "Search", // the invisible label of the input fields
            srSuggestionsHiddenText: "Search suggestions are hidden", // text to announce @screen reader when search suggestions were hidden
            srNoSuggestionsText: 'No search suggestions', // text to announce @screen reader if no suggestions are available
            srSuggestionsCountText: '#COUNT# search suggestions shown', // text to announce @screen reader after search suggestions have been shown, #COUNT# will be replaced with the suggestion count
            srOneSuggestionText: 'One search suggestion shown', // text to announce @screen reader after search suggestions have been shown
            srSuggestBoxControlDescription: 'Use up and down arrows to select available result. Press enter to go to selected search result. Touch devices users can use touch and swipe gestures.', // text to announce @screen reader after search input is focused - describes keyboard controls
        },
        searchBoxContainer: searchBox.parentElement.parentElement // the search box's parent
    });
} function enterResultCallback(query, href, hasCtrlModifier) {

    if (href) {
        window.location.href = href;
    }
    else {
        window.location.href=shopUrl+`?query=${document.querySelector("#globalSearch").value}`
    }

}