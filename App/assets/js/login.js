async function loginUser(event) {
  event.preventDefault();
  const email = document.getElementById("email").value;
  const password = document.getElementById("password").value;

  try {
    const response = await fetch("https://localhost:1006/login-customer", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password })
    });

    if (response.ok) {
      const data = await response.json();
      console.log(data);

      if (data.isSuccess) {
        localStorage.setItem("userEmail", email);
        updateNavbar();
        closePopup('loginPopup');
        showPopup("Đăng nhập thành công");
      } else {
        showPopup("Đăng nhập không thành công. Vui lòng kiểm tra lại tài khoản.");
      }
    } else {
      showPopup("Có lỗi xảy ra khi đăng nhập.");
    }
  } catch (error) {
    console.error("Error during login:", error);
    showPopup("Không thể kết nối tới máy chủ.");
  }
}

function addToCart() {
  const productId = getProductId();
  const quantity = parseInt(document.querySelector('.form-control.text-center').value) || 1;
  

  // Lấy email của người dùng đã đăng nhập
  const email = localStorage.getItem("userEmail");
  if (!email) {
    showPopup("Bạn cần đăng nhập trước khi thêm sản phẩm vào giỏ hàng.");
    return;
  }

  // Lấy giỏ hàng theo email
  const cartKey = `cart_${email}`;
  const cart = JSON.parse(localStorage.getItem(cartKey)) || [];

  // Thêm sản phẩm vào giỏ hàng
  const productIndex = cart.findIndex(item => item.productId === productId);
  if (productIndex > -1) {
    cart[productIndex].quantity += quantity;
  } else {
    cart.push({ productId, quantity });
  }

  // Lưu giỏ hàng vào localStorage theo email của người dùng
  localStorage.setItem(cartKey, JSON.stringify(cart));
  showPopup("Sản phẩm đã được thêm vào giỏ hàng.");
  setTimeout(() => {
    window.location.href = "/pages/cart.html";
  }, 3000);
}



// Hàm đăng xuất
function logout() {
  localStorage.removeItem("userEmail");

  updateNavbar();

  showPopup("Đăng xuất thành công.");
}


function getCartItemCount() {
  const userEmail = localStorage.getItem("userEmail");
  if (!userEmail) return 0;

  // Lấy giỏ hàng dựa trên email người dùng
  const cartKey = `cart_${userEmail}`;
  const cart = JSON.parse(localStorage.getItem(cartKey)) || [];
  return cart.reduce((total, item) => total + item.quantity, 0);
}

function updateNavbar() {
  const cartItemCount = getCartItemCount();
  const userEmail = localStorage.getItem("userEmail");
  const accountSection = document.getElementById("accountSection");

  if (userEmail) {
    
    const emailPrefix = userEmail.split('@')[0];
    const shortName = emailPrefix.length > 10 ? `hi, ${emailPrefix.substring(0, 10)}...` : `hi, ${emailPrefix}`;

    accountSection.innerHTML = `
      <a href="/pages/cart.html" class="icons-btn d-inline-block bag">
        <span class="icon-shopping-bag"></span>
        <span class="number">${cartItemCount}</span>
      </a>
      
      <!-- Dropdown tài khoản -->
      <a href="#" class="icons-btn d-inline-block ml-3 dropdown-toggle" data-toggle="dropdown">
        ${shortName}
      </a>
      <div class="dropdown-menu dropdown-menu-right">
        <a class="dropdown-item" href="/pages/profile.html">Hồ sơ của bạn</a>
        <div class="dropdown-divider"></div>
        <a class="dropdown-item" href="#" onclick="logout()">Đăng xuất</a>
      </div>
    `;
  } else {
    accountSection.innerHTML = `
      <a href="#" class="icons-btn d-inline-block ml-3" onclick="switchPopup('registerPopup', 'loginPopup')">Đăng nhập</a>
      <a href="#" class="icons-btn d-inline-block ml-3" onclick="switchPopup('loginPopup', 'registerPopup')">Đăng ký</a>
    `;
  }
}


// Kiểm tra trạng thái đăng nhập khi tải trang
document.addEventListener("DOMContentLoaded", updateNavbar);
