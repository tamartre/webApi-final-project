let search = "";
let min = 0;
let max = 1000000;
let categoryChecked = [];
let catString = "";
let countBasket = 0;

// Initialize event listeners and load initial data on window load
window.addEventListener("load", function () {
    initializeProducts();
    initializeCategories();
    initializeBasketCount();
    document.querySelector("#ItemsCountText").innerHTML = countBasket;
});

// Filter products based on search criteria and category selection
const filterProducts = () => {
    search = document.getElementById('nameSearch').value;
    min = document.getElementById('minPrice').value;
    max = document.getElementById('maxPrice').value;
    catString = categoryChecked.map(catId => `&categoryIds=${catId}`).join('');
    applyFilter();
}

// Apply filter by fetching products from the API based on the filter criteria
async function applyFilter() {
    const resp = await fetch(`api/Product?descreption=${search}&min=${min}&max=${max}${catString}`);
    if (resp.ok) {
        const data = await resp.json();
        clearElementContent('PoductList');
        displayProducts(data);
    }
}

// Add event listeners to category checkboxes
const addCheckBoxEventListeners = () => {
    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function (event) {
            if (event.target.checked) {
                categoryChecked.push(event.target.value);
            } else {
                categoryChecked = categoryChecked.filter(catId => catId != event.target.value);
                catString = "";
            }
            filterProducts();
        });
    });
};

// Fetch and display all products initially
async function initializeProducts() {
    const resp = await fetch("api/Product");
    if (resp.ok) {
        const data = await resp.json();
        displayProducts(data);
    }
}

// Display products on the UI based on the provided product data
const displayProducts = (products) => {
    const template = document.querySelector("#temp-card");
    products.forEach(product => {
        createProductCard(product, template);
    });
}

// Create and append a product card to the product list
const createProductCard = (product, template) => {
    document.querySelector("#ItemsCountText").innerHTML = countBasket;
    const clone = template.content.cloneNode(true);
    let item = clone.querySelector("div");
    item.querySelector("h1").textContent = product.productName;
    item.querySelector(".description").textContent = product.description;
    item.querySelector(".price").textContent = product.price;
    item.querySelector("img").src = product.imgUrl;
    item.setAttribute("id", product.productId);
    
    let addToBasketButton = item.querySelector('button');
    addToBasketButton.addEventListener('click', function () {
        addToBasket(product);
    });
    
    document.getElementById("PoductList").appendChild(clone);
};

// Add product to the basket and update the basket count
const addToBasket = (product) => {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    let found = false;
    basket.forEach(item => {
        if (item.prod.productName === product.productName && item.prod.description === product.description) {
            item.quantity += 1;
            found = true;
        }
    });
    if (!found) {
        basket.push({ prod: product, quantity: 1 });
    }
    countBasket += 1;
    document.querySelector("#ItemsCountText").innerHTML = countBasket;
    sessionStorage.setItem("basket", JSON.stringify(basket));
};

// Fetch and display all categories initially
async function initializeCategories() {
    const resp = await fetch("api/Category");
    if (resp.ok) {
        const categories = await resp.json();
        displayCategories(categories);
        addCheckBoxEventListeners();
    }
}

// Display categories on the UI based on the provided category data
const displayCategories = (categories) => {
    const template = document.querySelector("#temp-category");
    const categoryList = document.getElementById("categoryList");

    categories.forEach(category => {
        const clone = template.content.cloneNode(true);
        const item = clone.querySelector("div");
        item.querySelector(".opt").value = category.categoryId;
        item.querySelector(".OptionName").textContent = category.categoryName;
        categoryList.appendChild(clone);
    });
};

// Track the basket link and redirect to user details page
function trackLinkID(a) {
    window.location.replace("userDeatails.html");
}

// Initialize and count the items in the basket on page load
function initializeBasketCount() {
    const basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    countBasket = basket.reduce((count, item) => count + item.quantity, 0);
}

// Utility function to clear HTML content of an element by its ID
function clearElementContent(elementId) {
    const element = document.getElementById(elementId);
    while (element.firstChild) {
        element.removeChild(element.firstChild);
    }
}
