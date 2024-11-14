document.addEventListener("DOMContentLoaded", function () {
  // Tham chiếu đến phần tử HTML nơi danh mục sẽ được hiển thị
  const categoryList = document.getElementById("categoryList");

  fetch('https://localhost:1006/get-categories')
      .then(response => response.json())
      .then(data => {
          // Xóa nội dung hiện tại của categoryList
          categoryList.innerHTML = '';

          data.forEach(category => {
              const listItem = document.createElement("li");
              listItem.innerHTML = `<a href="/pages/index-category.html?id=${category.id}">${category.name}</a>`;
              categoryList.appendChild(listItem);
          });
      })
      .catch(error => {
          console.error('Error fetching categories:', error);
          categoryList.innerHTML = '<li><a href="#">Không thể tải danh mục</a></li>';
      });
});

function toggleSearch(show) {
    document.getElementById("searchSection").style.display = show ? "block" : "none";
    if (show) document.getElementById("searchInput").focus();
}

function searchProduct(event) {
    event.preventDefault();

    const productName = document.getElementById("searchInput").value.trim();
    if (!productName) return;

    // Chuyển hướng tới trang kết quả tìm kiếm với từ khóa tìm kiếm trong URL
    window.location.href = `/pages/search-results.html?productname=${encodeURIComponent(productName)}`;
}



  function displaySearchResults(products) {
    let resultsContainer = document.createElement("div");
    resultsContainer.innerHTML = products.map(product => `
    <div>
      <a href="/pages/shop-single.html?id=${product.id}">
        <h5>${product.name}</h5>
      </a>
    </div>
  `).join("");

    document.getElementById("searchSection").appendChild(resultsContainer);
  }



