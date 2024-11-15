async function checkAdminAccess() {
    const email = localStorage.getItem("userEmail");
    
    if (!email) {
        window.location.href = "/pages/404.html";
        return;
    }

    try {
        const response = await fetch(`https://localhost:1006/get-customer-by-email?email=${encodeURIComponent(email)}`);
        
        if (!response.ok) throw new Error("Lỗi khi lấy thông tin khách hàng");

        const userData = await response.json();
        
        if (userData.role === 1) {
            console.log("Access granted to /admin");
        } else {
            window.location.href = "/pages/404.html";
        }
    } catch (error) {
        console.error("Error checking admin access:", error);
        window.location.href = "/pages/404.html";
    }
}

document.addEventListener("DOMContentLoaded", checkAdminAccess);
