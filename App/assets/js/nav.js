document.addEventListener("DOMContentLoaded", function () {
      // Tham chiếu đến phần tử HTML nơi danh mục sẽ được hiển thị
      const categoryList = document.getElementById("categoryList");

      fetch('https://localhost:7099/get-categories')
        .then(response => response.json())
        .then(data => {
          // Xóa nội dung hiện tại của categoryList
          categoryList.innerHTML = '';

          data.forEach(category => {
            const listItem = document.createElement("li");
            listItem.innerHTML = `<a href="/pages/category/index-category.html?id=${category.id}">${category.name}</a>`;
            categoryList.appendChild(listItem);
          });
        })
        .catch(error => {
          console.error('Error fetching categories:', error);
          categoryList.innerHTML = '<li><a href="#">Không thể tải danh mục</a></li>';
        });
});