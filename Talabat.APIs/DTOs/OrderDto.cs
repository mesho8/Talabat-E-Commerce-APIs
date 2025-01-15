﻿namespace Talabat.APIs.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipAddress { get; set; }
    }
}
