using System.Collections.Generic;
using System;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.APIs.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }
        public int PaymentIntentId { get; set; }
        public decimal Total { get; set; }
    }
}
