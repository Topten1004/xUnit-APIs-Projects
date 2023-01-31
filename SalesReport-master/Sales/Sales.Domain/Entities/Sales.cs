using System;
using System.ComponentModel.DataAnnotations;

namespace Sales.Domain.Entities
{
    public class Sale : BaseEntity
    {
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Sales Channel")]
        public string SalesChannel { get; set; }

        [Display(Name = "Order Priority")]
        public string OrderPriority { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order ID")]
        public string OrderId { get; set; }

        [Display(Name = "Ship Date")]
        public DateTime ShipDate { get; set; }

        [Display(Name = "Units Sold")]
        public int UnitsSold { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }

        [Display(Name = "Total Revenue")]
        public decimal TotalRevenue { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Total Profit")]
        public decimal TotalProfit { get; set; }
    }
}