function addToCart() {
    const productId = getProductId();
    const quantity = parseInt(document.querySelector('.form-control.text-center').value) || 1;
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    const productIndex = cart.findIndex(item => item.productId === productId);
    if (productIndex > -1) {
        cart[productIndex].quantity += quantity;
    } else {
        cart.push({ productId, quantity });
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    alert('Sản phẩm đã được thêm vào giỏ hàng.');
}