﻿namespace NhaThuocOnline.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
