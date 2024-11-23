// Hiển thị form bước tiếp theo
function showForgotStep(step) {
    if (step === 1) {
      document.querySelector('#forgotPasswordForm').style.display = 'block';
      document.querySelector('#resetPasswordForm').style.display = 'none';
    } else if (step === 2) {
      document.querySelector('#forgotPasswordForm').style.display = 'none';
      document.querySelector('#resetPasswordForm').style.display = 'block';
    }
  }
  
  // Gửi OTP quên mật khẩu
  async function sendForgotPasswordOtp() {
    const email = document.getElementById("forgotEmail").value;
    const encodedEmail = encodeURIComponent(email);
  
    try {
      const response = await fetch(`https://localhost:1006/change-password?email=${encodedEmail}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({})
      });
  
      if (response.ok) {
        showPopup("Mã OTP đã được gửi đến email của bạn.");
        showForgotStep(2);
      } else {
        showPopup("Không thể gửi mã OTP. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error sending OTP:", error);
      showPopup("Gửi OTP thất bại. Vui lòng thử lại.");
    }
  }
  
  // Đặt lại mật khẩu bằng OTP
  async function resetPassword() {
    const email = document.getElementById("forgotEmail").value;
    const otp = document.getElementById("resetOtp").value;
    const newPassword = document.getElementById("newPassword").value;
    const confirmPassword = document.getElementById("confirmNewPassword").value;
  
    const resetData = {
      otp: otp,
      newPassword: newPassword,
      confirmPassword: confirmPassword
    };
  
    const encodedEmail = encodeURIComponent(email);
  
    try {
      const response = await fetch(`https://localhost:1006/update-customer-password?email=${encodedEmail}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(resetData)
      });
  
      if (response.ok) {
        showPopup("Mật khẩu của bạn đã được đặt lại thành công.");
        closePopup('forgotPasswordPopup');
      } else {
        showPopup("Đặt lại mật khẩu thất bại. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error resetting password:", error);
      showPopup("Xác thực OTP hoặc đặt lại mật khẩu thất bại. Vui lòng thử lại.");
    }
  }
  