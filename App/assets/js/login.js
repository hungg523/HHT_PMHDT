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
        alert("Đăng nhập thành công");
      } else {
        alert("Đăng nhập không thành công. Vui lòng kiểm tra lại tài khoản.");
      }
    } else {
      alert("Có lỗi xảy ra khi đăng nhập.");
    }
  } catch (error) {
    console.error("Error during login:", error);
    alert("Không thể kết nối tới máy chủ.");
  }
}


// Hàm đăng xuất
function logout() {
  localStorage.removeItem("userEmail");

  updateNavbar();

  alert("Đăng xuất thành công.");
}


function getCartItemCount() {
  const cart = JSON.parse(localStorage.getItem("cart")) || [];
  return cart.reduce((total, item) => total + item.quantity, 0);
}

function updateNavbar() {
  const cartItemCount = getCartItemCount();
  const userEmail = localStorage.getItem("userEmail");
  const accountSection = document.getElementById("accountSection");

  if (userEmail) {
    accountSection.innerHTML = `
    
      <a href="/pages/cart.html" class="icons-btn d-inline-block bag">
        <span class="icon-shopping-bag"></span>
        <span class="number">${cartItemCount}</span>
      </a>
      
      <!-- Dropdown tài khoản -->
      <a href="#" class="icons-btn d-inline-block ml-3 dropdown-toggle" data-toggle="dropdown">
        ${userEmail}
      </a>
      <div class="dropdown-menu dropdown-menu-right">
        <a class="dropdown-item" href="/pages/profile.html">Cập nhật hồ sơ</a>
        <a class="dropdown-item" href="/pages/orders.html">Thông tin đơn hàng</a>
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
