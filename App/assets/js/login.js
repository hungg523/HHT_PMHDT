// Hiển thị form bước tiếp theo
function showStep(step) {
    if (step === 1) {
      document.querySelector('.form-step-1').style.display = 'block';
      document.querySelector('.form-step-2').style.display = 'none';
    } else if (step === 2) {
      document.querySelector('.form-step-1').style.display = 'none';
      document.querySelector('.form-step-2').style.display = 'block';
    }
  }

  // Hàm đăng ký người dùng
  async function registerUser() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("regPassword").value;
    const confirmPassword = document.getElementById("confirmPassword").value;

    const userData = {
      firstName: "usernew",
      lastName: "usernew",
      email: email,
      password: password,
      confirmPassword: confirmPassword
    };

    try {
      const response = await fetch("https://localhost:1006/customer-register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(userData)
      });

      if (response.ok) {
        const data = await response.json();
        userId = data.id;
        alert("Đã gửi mã xác thực, kiểm tra email của bạn!");
        showStep(2); // Chuyển sang bước 2: Nhập OTP
      } else {
        alert("Đăng ký thất bại. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error registering user:", error);
      alert("Đăng ký thất bại. Vui lòng thử lại.");
    }
  }

  // Hàm xác thực OTP
  async function verifyOtp() {
    const otp = document.getElementById("otp").value;
    try {
      const response = await fetch(`https://localhost:1006/authen-customer?id=${userId}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ otp })
      });
      if (response.ok) {
        alert("OTP đã được xác thực thành công.");
        closePopup('registerPopup'); // Đóng popup sau khi xác thực thành công
      } else {
        alert("Mã OTP không đúng. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error verifying OTP:", error);
      alert("Xác thực OTP thất bại. Vui lòng thử lại.");
    }
  }