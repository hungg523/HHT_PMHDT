let customerId;
        document.addEventListener("DOMContentLoaded", async function () {
            const email = localStorage.getItem("userEmail");
            try {
                const response = await fetch(`https://localhost:1006/get-customer-by-email?email=${email}`);
                if (!response.ok) throw new Error("Lỗi khi lấy thông tin khách hàng");

                const customerData = await response.json();
                customerId = customerData.id;
                const avatarPath = customerData.avatarImagePath ? `https://localhost:1006/${customerData.avatarImagePath}` : "/assets/images/user.png";

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
                    alert("Cập nhật hồ sơ thành công!");
                } else {
                    alert("Cập nhật hồ sơ thất bại. Vui lòng thử lại.");
                }
            } catch (error) {
                console.error("Error updating profile:", error);
                alert("Không thể kết nối tới máy chủ.");
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
                    alert("Địa chỉ mới đã được thêm thành công!");
                } else {
                    alert("Thêm địa chỉ thất bại. Vui lòng thử lại.");
                }
            } catch (error) {
                console.error("Error adding address:", error);
                alert("Không thể kết nối tới máy chủ.");
            }
        }

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