document.addEventListener("DOMContentLoaded", function () {
    const categoryListFooter = document.getElementById("categoryListFooter");
  
    fetch('https://localhost:7099/get-categories')
      .then(response => response.json())
      .then(data => {
        // Lấy ngẫu nhiên 4 mục từ dữ liệu trả về
        const shuffledData = data.sort(() => 0.5 - Math.random());
        const randomCategories = shuffledData.slice(0, 4);
  
        // Xóa nội dung hiện tại của categoryListFooter
        categoryListFooter.innerHTML = '';
  
        // Duyệt qua 4 danh mục ngẫu nhiên và thêm vào danh sách
        randomCategories.forEach(category => {
          const listItem = document.createElement("li");
          listItem.innerHTML = `<a href="/pages/category/index-category.html?id=${category.id}">${category.name}</a>`;
          categoryListFooter.appendChild(listItem);
        });
      })
      .catch(error => {
        console.error('Error fetching categories:', error);
        categoryListFooter.innerHTML = '<li><a href="#">Không thể tải danh mục</a></li>';
      });
  });
  