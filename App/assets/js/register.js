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
    const email = document.getElementById("registerEmail").value;
    const password = document.getElementById("regPassword").value;
    const confirmPassword = document.getElementById("confirmPassword").value;




    const userData = {
      lastName: "Hiện chưa cập nhật...",
      email: email,
      password: password,
      confirmPassword: confirmPassword
    };

    try {
      const response = await fetch("https://localhost:1006/register-customer", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(userData)
      });

      if (response.ok) {
        const data = await response.json();
        userId = data.id;
        showPopup("Đã gửi mã xác thực, kiểm tra email của bạn!");
        showStep(2);
        startCountdown(); 
      } else {
        showPopup("Đăng ký thất bại. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error registering user:", error);
      showPopup("Đăng ký thất bại. Vui lòng thử lại.");
    }
}

  // Hàm xác thực OTP
  async function verifyOtp() {
    const email = document.getElementById("registerEmail").value;
    const otp = document.getElementById("otp").value;
    try {
      const encodedEmail = encodeURIComponent(email);
      const response = await fetch(`https://localhost:1006/authen-customer?email=${encodedEmail}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ otp })
      });
      if (response.ok) {
        alert("OTP đã được xác thực thành công.");
        closePopup('registerPopup');
        window.location.reload();
      } else {
        alert("Mã OTP không đúng. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error verifying OTP:", error);
      alert("Xác thực OTP thất bại. Vui lòng thử lại.");
    }
  }
  function showPopup(message) {
    document.getElementById("notificationMessage").textContent = message;
    $('#notificationModal').modal('show'); 
}

let countdownInterval;

async function resendOtp() {
    const email = document.getElementById("registerEmail").value;
    const encodedEmail = encodeURIComponent(email);

    try {
        const response = await fetch(`https://localhost:1006/resend-otp?email=${encodedEmail}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({})
        });

        if (response.ok) {
            alert("Mã OTP mới đã được gửi!");
            startCountdown();
        } else {
            alert("Gửi lại OTP thất bại. Vui lòng thử lại.");
        }
    } catch (error) {
        console.error("Error resending OTP:", error);
        alert("Gửi lại OTP thất bại. Vui lòng thử lại.");
    }
}

function startCountdown() {
    let timeLeft = 75;
    const countdownDisplay = document.getElementById('countdown');
    const resendButton = document.getElementById('resendOtpButton');

    resendButton.disabled = true;

    countdownInterval = setInterval(() => {
        timeLeft -= 1;
        countdownDisplay.textContent = timeLeft;

        if (timeLeft <= 0) {
            clearInterval(countdownInterval);
            resendButton.disabled = false;
            countdownDisplay.textContent = 'Gửi lại mã OTP';
        }
    }, 1000);
}



