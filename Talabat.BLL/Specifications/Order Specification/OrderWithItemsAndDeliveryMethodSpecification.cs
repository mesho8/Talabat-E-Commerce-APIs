using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BLL.Specifications.Order_Specification
{
    internal class OrderWithItemsAndDeliveryMethodSpecification:BaseSepcification<Order>
    {
        // This Constructor Is Used To Get All Orders For User
        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail)
            :base(O => O.BuyerEmail == buyerEmail)
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }


        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail , int orderId)
            :base(O => (O.BuyerEmail == buyerEmail && O.Id == orderId))
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
        }
    }
}
