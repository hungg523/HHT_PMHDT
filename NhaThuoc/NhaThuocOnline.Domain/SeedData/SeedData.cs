using Microsoft.EntityFrameworkCore;
using NhaThuocOnline.Domain.Entities;

namespace NhaThuocOnline.Domain.SeedDomain
{
    public static class SeedDomain
    {
        public static void SeedDomainGenerate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(

                 new Customer()
                 {
                     Id = 1,
                     FirstName = "Tran",
                     LastName = "The",
                     PhoneNumber = "0987932132",
                     AvatarImagePath = "/img/default-avt",
                     Email = "abc@example.com",
                     PasswordHash = "Abcd@123",
                     IsActive = true,
                     CreatedAt = new DateTime()
                 },
                  new Customer()
                  {
                      Id = 2,

                      FirstName = "Hoang",
                      LastName = "Huynh",
                      PhoneNumber = "03456789",
                      AvatarImagePath = "/img/default-avt",
                      Email = "def@example.com",

                      PasswordHash = "Abcd@123",
                      IsActive = true,
                      CreatedAt = new DateTime()
                  }

                );

            // product
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    ProductName = "Paracetamol 500mg",
                    SKU = "SKU001",
                    RegularPrice = 48000,
                    DiscountPrice = 38000,

                    Description = "Paracetamol 500mg là một loại thuốc giảm đau và hạ sốt thông thường được sử dụng. Nó phù hợp để giảm đau nhẹ đến vừa và hạ sốt.",
                    ImagePath = "img/products/paracetamol500mg.jpg",
                    Brand = "Stada",
                    SeoTitle = "Paracetamol 500mg",
                    SeoAlias = "paracetamol-500mg",

                    Packaging = "Hộp 10 Vỉ x 10 Viên",
                    Origin = "Việt Nam",
                    Manufacturer = "STADA",
                    ProductionLocation = "Việt Nam",
                    Ingredients = "Paracetamol",


                    isPrescriptionRequired = false,
                    IsPublished = true,

                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                 new Product()
                 {
                     Id = 2,
                     ProductName = "Ibuprofen 200mg",
                     SKU = "SKU002",
                     RegularPrice = 55000,
                     DiscountPrice = 45000,
                     Description = "Ibuprofen 200mg là một loại thuốc giảm đau, hạ sốt và chống viêm thông thường được sử dụng. Nó phù hợp để giảm đau nhẹ đến vừa, hạ sốt và giảm viêm.",
                     ImagePath = "img/products/ibuprofen200mg.jpg",
                     Brand = "Nadyphar",
                     SeoTitle = "Ibuprofen 200mg",
                     SeoAlias = "ibuprofen-200mg",
                     Packaging = "Hộp 20 Vỉ x 10 Viên",
                     Origin = "Việt Nam",
                     Manufacturer = "ABC Company",
                     ProductionLocation = "Việt Nam",
                     Ingredients = "Ibuprofen",
                     isPrescriptionRequired = false,
                     IsPublished = true,
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                 },
                 new Product()
                 {
                     Id = 3,
                     ProductName = "Vitamin C 1000mg",
                     SKU = "SKU003",
                     RegularPrice = 25000,
                     DiscountPrice = 20000,
                     Description = "Vitamin C 1000mg là một loại vitamin tổng hợp thường được sử dụng để bổ sung nhu cầu vitamin C hàng ngày. Nó giúp tăng cường hệ miễn dịch và chống oxy hóa.",
                     ImagePath = "img/products/vitaminc1000mg.jpg",
                     Brand = "KUDOS",
                     SeoTitle = "Vitamin C 1000mg",
                     SeoAlias = "vitamin-c-1000mg",
                     Packaging = "Hộp 30 Viên",
                     Origin = "Mỹ",
                     Manufacturer = "ABC Company",
                     ProductionLocation = "Mỹ",
                     Ingredients = "Vitamin C",
                     isPrescriptionRequired = false,
                     IsPublished = true,
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                 },
    new Product()
    {
        Id = 4,
        ProductName = "Omega-3 Fish Oil",
        SKU = "SKU004",
        RegularPrice = 120000,
        DiscountPrice = 100000,
        Description = "Omega-3 Fish Oil là một loại bổ sung dầu cá chứa các axit béo omega-3. Nó có lợi cho sức khỏe tim mạch, não bộ và khớp.",
        ImagePath = "img/products/omega3fishoil.jpg",
        Brand = "Sirio Pharma",
        SeoTitle = "Omega-3 Fish Oil",
        SeoAlias = "omega-3-fish-oil",
        Packaging = "Chai 60 Viên",
        Origin = "Na Uy",
        Manufacturer = "XYZ Company",
        ProductionLocation = "Na Uy",
        Ingredients = "Dầu cá omega-3",
        isPrescriptionRequired = false,
        IsPublished = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    }

                );

            modelBuilder.Entity<Category>().HasData(
                     new Category
                     {
                         Id = 1,
                         ParentId = 0,
                         CategoryName = "Dược phẩm",
                         CategoryDescription = "Dược phẩm",
                         ImagePath = "path/to/image",
                         IsActive = true,
                         CreatedAt = DateTime.Now,
                         UpdatedAt = DateTime.Now
                     },
    new Category
    {
        Id = 2,
        ParentId = 0,
        CategoryName = "Chăm sóc sức khỏe",
        CategoryDescription = "Chăm sóc sức khỏe",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 3,
        ParentId = 0,
        CategoryName = "Sản phẩm tiện lợi",
        CategoryDescription = "Sản phẩm tiện lợi",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 4,
        ParentId = 0,
        CategoryName = "Thực phẩm chức năng",
        CategoryDescription = "Thực phẩm chức năng",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 5,
        ParentId = 1,
        CategoryName = "Thuốc kê đơn",
        CategoryDescription = "Thuốc kê đơn",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 6,
        ParentId = 1,
        CategoryName = "Thuốc không kê đơn",
        CategoryDescription = "Thuốc không kê đơn",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 7,
        ParentId = 2,
        CategoryName = "Thực phẩm dinh dưỡng",
        CategoryDescription = "Thực phẩm dinh dưỡng",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 8,
        ParentId = 2,
        CategoryName = "Tai mũi họng",
        CategoryDescription = "Tai mũi họng",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    },
    new Category
    {
        Id = 9,
        ParentId = 8,
        CategoryName = "Hàng tổng hợp",
        CategoryDescription = "Hàng tổng hợp",
        ImagePath = "path/to/image",
        IsActive = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    }

                );

            modelBuilder.Entity<ProductInCategory>().HasData(

                 new ProductInCategory
                 {
                     Id = 1,
                     CategoryId = 6,
                     ProductId = 1
                 },
    new ProductInCategory
    {
        Id = 2,
        CategoryId = 6,
        ProductId = 2
    },
    new ProductInCategory
    { Id = 3,
        CategoryId = 7,
        ProductId = 3
    }
                );

            modelBuilder.Entity<Coupon>().HasData(

                new Coupon()
                {
                    Id = 1,
                    Code = "BANMOI",
                    CouponDescription = "KHUYEN MAI LAN DAU",
                    TimesUsed = 0,
                    MaxUsage = 1,
                    DiscountType = "co-dinh",
                    DiscountValue = 50,
                    CouponStartDate = DateTime.Now,
                    CouponEndDate = DateTime.Now.AddDays(14),
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                }
                //,new Coupon()
                //  {
                //      Id = 2,
                //      Code = "NOEL",
                //      CouponDescription = "KHUYEN MAI THANG 12",
                //      TimesUsed = 0,
                //      MaxUsage = 1,
                //      DiscountType = "phan-tram",
                //      DiscountValue = 10,
                //      CouponStartDate = DateTime.Now,
                //      CouponEndDate = DateTime.Now.AddDays(14),
                //      IsActive = true,
                //      CreatedAt = DateTime.Now,
                //      UpdatedAt = DateTime.Now

                //  }
                );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem()
                {
                    Id = 1,
                    CartId= "3c2efd8f-d0f9-4719-ae61-755b5b2226fc",
                    ProductId=1,
                    Quantity=2,
                },
                 new CartItem()
                 {
                     Id = 2,
                     CartId = "3c2efd8f-d0f9-4719-ae61-755b5b2226fc",
                     ProductId = 2,
                     Quantity = 3,
                 }


                );

            modelBuilder.Entity<Batch>().HasData(
                new Batch()
                {
                    Id=1,
                    ProductId= 1,
                    OriginPrice= 50000,
                    Quantity= 100,
                    Stock = 100,
                    ExpireDate = DateTime.Now,
                    ManufacturingDate = DateTime.Now.AddMonths(12),
                    CreatedAt= DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    

                }
                );





        }
    }
}
