let customerId;
        document.addEventListener("DOMContentLoaded", async function () {
            const email = localStorage.getItem("userEmail");
            try {
                const response = await fetch(`https://localhost:1006/get-customer-by-email?email=${email}`);
                if (!response.ok) throw new Error("Lỗi khi lấy thông tin khách hàng");

                const customerData = await response.json();
                customerId = customerData.id;
                const avatarPath = customerData.avatarImagePath ? `https://localhost:1005/${customerData.avatarImagePath}` : "/assets/images/user.png";

                document.getElementById("userAvatar").src = avatarPath;
                document.getElementById("userName").textContent = `${customerData.firstName} ${customerData.lastName}`;
                document.getElementById("email").value = customerData.email;
                document.getElementById("fullName").value = `${customerData.firstName} ${customerData.lastName}`;
                document.getElementById("phone").value = customerData.phoneNumber;

            } catch (error) {
                console.error("Error fetching customer info:", error);
            }
        });

        async function updateProfile(event) {
            event.preventDefault();
            const fullName = document.getElementById("fullName").value.trim();
            const [firstName, ...lastNameParts] = fullName.split(" ");
            const lastName = lastNameParts.join(" ");

            const updatedData = {
                firstName: firstName,
                lastName: lastName,
                phoneNumber: document.getElementById("phone").value,
                email: document.getElementById("email").value
            };

            try {
                const response = await fetch(`https://localhost:1006/update-profile-customer?id=${customerId}`, {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(updatedData)
                });

                if (response.ok) {
                    showPopup("Cập nhật hồ sơ thành công!");
                    window.location.reload();
                } else {
                    showPopup("Cập nhật hồ sơ thất bại. Vui lòng thử lại.");
                }
            } catch (error) {
                console.error("Error updating profile:", error);
                showPopup("Không thể kết nối tới máy chủ.");
            }
        }
        
        // Fetch Tỉnh/Thành phố
        async function fetchProvinces() {
            try {
                const response = await fetch("https://provinces.open-api.vn/api/p/");
                const provinces = await response.json();

                const provinceSelect = document.getElementById("province");
                provinces.forEach(province => {
                    const option = document.createElement("option");
                    option.value = province.code;
                    option.textContent = province.name;
                    provinceSelect.appendChild(option);
                });
            } catch (error) {
                console.error("Error fetching provinces:", error);
            }
        }

        // Fetch Quận/Huyện khi Tỉnh/Thành phố
        async function fetchDistricts() {
            const provinceId = document.getElementById("province").value;
            if (!provinceId) return;

            try {
                const response = await fetch(`https://api.example.com/districts?provinceId=${provinceId}`);
                const districts = await response.json();

                const districtSelect = document.getElementById("district");
                districtSelect.innerHTML = '<option value="">Chọn Quận/Huyện</option>';
                document.getElementById("ward").innerHTML = '<option value="">Chọn Phường/Xã</option>'; // Reset wards

                districts.forEach(district => {
                    const option = document.createElement("option");
                    option.value = district.id;
                    option.textContent = district.name;
                    districtSelect.appendChild(option);
                });
            } catch (error) {
                console.error("Error fetching districts:", error);
            }
        }

        async function fetchDistricts() {
            const provinceCode = document.getElementById("province").value;
            if (!provinceCode) return;

            try {
                const response = await fetch(`https://provinces.open-api.vn/api/p/${provinceCode}?depth=2`);
                const data = await response.json();
                const districts = data.districts;

                const districtSelect = document.getElementById("district");
                districtSelect.innerHTML = '<option value="">Chọn Quận/Huyện</option>';
                document.getElementById("ward").innerHTML = '<option value="">Chọn Phường/Xã</option>';

                districts.forEach(district => {
                    const option = document.createElement("option");
                    option.value = district.code;
                    option.textContent = district.name;
                    districtSelect.appendChild(option);
                });
            } catch (error) {
                console.error("Error fetching districts:", error);
            }
        }

        // Gọi hàm để tải danh sách Tỉnh/Thành phố khi trang được tải
        document.addEventListener("DOMContentLoaded", fetchProvinces);
        async function fetchWards() {
            const districtCode = document.getElementById("district").value;
            if (!districtCode) return;

            try {
                const response = await fetch(`https://provinces.open-api.vn/api/d/${districtCode}?depth=2`);
                const data = await response.json();
                const wards = data.wards;

                const wardSelect = document.getElementById("ward");
                wardSelect.innerHTML = '<option value="">Chọn Phường/Xã</option>';
                wards.forEach(ward => {
                    const option = document.createElement("option");
                    option.value = ward.code;
                    option.textContent = ward.name;
                    wardSelect.appendChild(option);
                });
            } catch (error) {
                console.error("Error fetching wards:", error);
            }
        }
        document.addEventListener("DOMContentLoaded", fetchProvinces);

        // Hàm thêm địa chỉ
        async function addAddress(event) {
            event.preventDefault();

            const addressData = {
                customerId: customerId,
                fullName: document.getElementById("newFullName").value,
                phone: document.getElementById("newPhone").value,
                address: document.getElementById("newAddress").value,
                province: document.getElementById("province").selectedOptions[0].text,
                district: document.getElementById("district").selectedOptions[0].text,
                ward: document.getElementById("ward").selectedOptions[0].text
            };

            try {
                const response = await fetch("https://localhost:1006/create-customeraddress", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(addressData)
                });

                if (response.ok) {
                    showPopup("Địa chỉ mới đã được thêm thành công!");
                    window.location.reload();
                } else {
                    showPopup("Thêm địa chỉ thất bại. Vui lòng thử lại.");
                }
            } catch (error) {
                console.error("Error adding address:", error);
                showPopup("Không thể kết nối tới máy chủ.");
            }
        }

        document.addEventListener("DOMContentLoaded", async function () {
            await getCustomerIdByEmail();
            if (customerId) {
                await loadOrders(customerId); // Chuyển customerId vào loadOrders
            } else {
                showPopup("Vui lòng đăng nhập để xem đơn hàng của bạn.");
            }
        });

        async function getCustomerIdByEmail() {
            const email = localStorage.getItem("userEmail");
            if (!email) return;
        
            try {
                const response = await fetch(`https://localhost:1006/get-customer-by-email?email=${email}`);
                if (!response.ok) throw new Error("Lỗi khi lấy thông tin khách hàng");
        
                const customerData = await response.json();
                customerId = customerData.id;
                return customerId;
            } catch (error) {
                console.error("Error fetching customer info:", error);
            }
        }
        
        let productsData = {};

        
        
        async function getCustomerIdByEmail() {
            const email = localStorage.getItem("userEmail");
            if (!email) return;
        
            try {
                const response = await fetch(`https://localhost:1006/get-customer-by-email?email=${email}`);
                if (!response.ok) throw new Error("Lỗi khi lấy thông tin khách hàng");
        
                const customerData = await response.json();
                customerId = customerData.id;
                return customerId;
            } catch (error) {
                console.error("Error fetching customer info:", error);
            }
        }
        
        // Hàm tải tất cả sản phẩm từ API
        async function fetchProducts() {
            try {
                const response = await fetch("https://localhost:1006/get-products");
                if (!response.ok) throw new Error("Không thể tải danh sách sản phẩm");
        
                const products = await response.json();
                products.forEach(product => {
                    productsData[product.id] = product;
                });
            } catch (error) {
                console.error("Lỗi khi tải sản phẩm:", error);
            }
        }
        
        async function loadOrders(customerId) {
            try {
                const response = await fetch(`https://localhost:1006/get-order-by-customer-id?id=${customerId}`);
                if (!response.ok) throw new Error("Không thể tải danh sách đơn hàng");
        
                const orders = await response.json();
                const ordersContainer = document.getElementById("orders");
        
                if (orders.length === 0) {
                    ordersContainer.innerHTML = "<p>Chưa có đơn hàng nào.</p>";
                    return;
                }
        
                ordersContainer.innerHTML = "";
        
                // Hiển thị từng đơn hàng và thông tin sản phẩm
                for (const order of orders) {
                    const orderElement = document.createElement("div");
                    orderElement.classList.add("order");
        
                    // Tải thông tin chi tiết từng sản phẩm
                    const orderItemsHTML = await Promise.all(order.orderItems.map(async (item) => {
                        try {
                            const productResponse = await fetch(`https://localhost:1006/get-product-detail?id=${item.productId}`);
                            if (!productResponse.ok) throw new Error(`Không thể tải thông tin sản phẩm ID: ${item.productId}`);
                            
                            const product = await productResponse.json();
                            const price = product.discountPrice || product.regularPrice;
        
                            return `
                                <li class="list-group-item d-flex align-items-center">
                                    <img src="https://localhost:1005/${product.imagePath}" alt="${product.productName}" class="img-thumbnail me-3" style="width: 80px; height: 80px; object-fit: cover;">
                                   <div style="padding: 10px; color: black;">
                                        <strong>${product.productName}</strong><br>
                                        <span>Số lượng: ${item.quantity}</span><br>
                                        <span>Đơn giá: ${price.toLocaleString()} VND</span><br>
                                        <span><strong>Tổng: ${(price * item.quantity).toLocaleString()} VND</strong></span>
                                    </div>
                                </li>
                            `;
                        } catch (error) {
                            console.error(`Lỗi khi tải thông tin sản phẩm ID: ${item.productId}`, error);
                            return `<li class="list-group-item text-danger"><strong>Không thể tải thông tin sản phẩm ID: ${item.productId}</strong></li>`;
                        }
                    }));
        
                    // Xử lý trạng thái đơn hàng
                    const statusText = order.status === 0
                    ? "Chờ xác nhận"
                    : order.status === 1
                    ? "Đã xác nhận"
                    : order.status === 2
                    ? "Đang giao hàng"
                    : order.status === 3
                    ? "Thành công"
                    : "Trạng thái không xác định";

                    // Xử lý thông tin mã giảm giá
                    const couponHTML = order.coupon
                        ? `
                            <p><strong>Mã giảm giá:</strong> ${order.coupon.description} (${order.coupon.discount.toLocaleString()} VND)</p>
                          `
                        : `<p><strong>Mã giảm giá:</strong> Không áp dụng</p>`;
        
                    orderElement.innerHTML = `
                        <hr>
                        <div class="card mb-4">
                            <div class="card-header bg-primary text-white">
                                <h5 class="mb-0">Mã đơn hàng: ${order.id}</h5>
                            </div>
                            <div class="card-body">
                                <p><strong>Email:</strong> ${order.email}</p>
                                <p><strong>Địa chỉ:</strong> ${order.address.fullName}, ${order.address.finalAddress} - ${order.address.phone}</p>
                                <p><strong>Trạng thái đơn hàng:</strong> ${statusText}</p>
                                ${couponHTML}
                                <h6 class="mt-4">Sản phẩm:</h6>
                                <ul class="list-group">
                                    ${orderItemsHTML.join("")}
                                </ul>
                                <div class="mt-4 text-end">
                                    <strong>Tổng tiền đơn hàng: ${(order.totalPrice).toLocaleString()} VND</strong>
                                </div>
                            </div>
                        </div>
                    `;
                    ordersContainer.appendChild(orderElement);
                }
            } catch (error) {
                console.error("Lỗi khi tải đơn hàng:", error);
                showPopup("Không thể tải đơn hàng. Vui lòng thử lại.");
            }
        }
        
        