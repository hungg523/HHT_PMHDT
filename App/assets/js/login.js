async function loginUser(event) {
  event.preventDefault();
  const email = document.getElementById("email").value;
  const password = document.getElementById("password").value;

  try {
    const response = await fetch("https://localhost:1006/customer-login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ email, password })
    });

    if (response.ok) {
      alert("Đăng nhập thành công!");
      closePopup('loginPopup');
      // Lưu trạng thái đăng nhập
      sessionStorage.setItem("isLoggedIn", "true");
    } else {
      alert("Tên đăng nhập hoặc mật khẩu không đúng.");
    }
  } catch (error) {
    console.error("Error:", error);
    alert("Đăng nhập thất bại. Vui lòng thử lại.");
  }
}