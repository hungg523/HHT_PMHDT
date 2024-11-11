function convertToBase64(fileInputId, callback) {
    const file = document.getElementById(fileInputId).files[0];
    const reader = new FileReader();

    const validImageTypes = ["image/jpeg", "image/jpg", "image/png", "image/webp", "image/bmp"];
    if (file && validImageTypes.includes(file.type)) {
        // Khi đọc file hoàn tất
        reader.onloadend = function () {
            const base64Image = reader.result;
            callback(base64Image); // Gọi hàm callback với dữ liệu base64
        };
        reader.readAsDataURL(file); // Chuyển file ảnh thành chuỗi base64
    } else {
        console.error("File không phải là ảnh hoặc định dạng ảnh không được hỗ trợ.");
        callback(null); // Trả về null nếu không phải ảnh
    }
}
