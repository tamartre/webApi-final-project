let sum = 0;
let shoppingBag = JSON.parse(sessionStorage.getItem("basket")) || [];

window.addEventListener("load", () => {

   
    checkAndClearBasketOnProductsPage();
    calculateBasketTotal();
    updateTotalColumnText();
    showProductsInBasket();
});

function checkAndClearBasketOnProductsPage() {
    if (window.location.href === "https://localhost:44356/Products.html") {
        sessionStorage.setItem("basket", "[]");
        shoppingBag = [];
    }

    else {
        shoppingBag = JSON.parse(sessionStorage.getItem("basket")) || [];
    }
}

function showProductsInBasket() {
    const template = document.querySelector("#temp-row");
    shoppingBag.forEach(product => createBasketShop(product, template));
}

function createBasketShop(product, template) {
    const clone = template.content.cloneNode(true);
    let item = clone.querySelector(".item-row");
    updateBasketItemDetails(item, product);
    document.querySelector(".items tbody").appendChild(clone);
    item.querySelector(".DeleteButton").addEventListener('click', () => handleDeleteClick(product));
}

function updateBasketItemDetails(item, product) {
    item.querySelector(".nameColumn").textContent = product.prod.productName;
    item.querySelector(".priceColumn").textContent = product.prod.price;
    item.querySelector(".quantityColumn").textContent = product.quantity;
    updateProductImage(item, product.prod.imgUrl);
    item.dataset.productId = product.prod.productId;
}

function updateProductImage(item, imgUrl) {
    let imageColumn = item.querySelector(".imageColumn");
    let image = imageColumn.querySelector(".image");
    if (!image.querySelector("img")) {
        let img = document.createElement("img");
        img.src = imgUrl;
        img.style.setProperty("width", "100px");
        image.appendChild(img);
    } else {
        image.querySelector("img").src = imgUrl;
    }
}

function handleDeleteClick(product) {
    if (product.quantity === 1) {
        removeProductFromBasket(product);
    } else {
        decreaseProductQuantity(product);
    }
    location.reload();
}

function removeProductFromBasket(product) {
    let updatedBasket = shoppingBag.filter(item => item.prod.description !== product.prod.description);
    sessionStorage.setItem("basket", JSON.stringify(updatedBasket));
    calculateBasketTotal();
    updateTotalColumnText();
}

function decreaseProductQuantity(product) {
    shoppingBag = shoppingBag.map(item => {
        if (item.prod.description === product.prod.description) {
            item.quantity -= 1;
        }
        return item;
    });
    sessionStorage.setItem("basket", JSON.stringify(shoppingBag));
    calculateBasketTotal();
    updateTotalColumnText();
}

function calculateBasketTotal() {
    sum = shoppingBag.reduce((total, item) => total + (item.quantity*item.prod.price), 0);
}

function updateTotalColumnText() {
    document.querySelector("#totalColumnText").textContent = sum;
}

function placeOrder() {
    let totalSum = calculateOrderSum();
    let userId = JSON.parse(localStorage.getItem("userID") || null);
    if (userId && totalSum) {
        addOrder(userId, totalSum);
    } else {
        alert("userId and sum are required!");
    }
}

function calculateOrderSum() {
    return shoppingBag.reduce((total, item) => total + (item.quantity * item.prod.price), 0);
}

async function addOrder(userId, sum) {
    let orderDTO = createOrderDTO(userId, sum);
    let response = await fetch("api/Order", {
        method: "POST",
        headers: { 'Content-Type': "application/json" },
        body: JSON.stringify(orderDTO)
    });
    handleOrderResponse(response);
}

function createOrderDTO(userId, sum) {
    let orderItems = shoppingBag.map(item => ({
        ProductId: item.prod.productId,
        Quantity: item.quantity
    }));
    return {
        UserId: userId,
        OrderSum: sum,
        OrderItemsDto: orderItems
    };
}

async function handleOrderResponse(response) {
    if (!response.ok) {
        alert("Order failed");
    } else {
        const data = await response.json();
        alert(`${data.orderId} order placed`);
        sessionStorage.setItem("basket", "[]");
        location.reload();
    }
}