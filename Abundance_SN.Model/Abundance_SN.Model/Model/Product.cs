using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abundance_SN.Model.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string AdditionalInformation { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool Shipping { get; set; }
        public int? Weight { get; set; }
        public bool CanBedelivered { get; set; }
        public int ReorderLevel { get; set; }
        public int? Visits { get; set; }
        public string ImageUrl { get; set; }
        public string Sku { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public  Brand Brand { get; set; }
        public  DeliveryService DeliveryService { get; set; }
        public Unit UnitMeasurement { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int DiscountAmount { get; set; }
        public int DiscountAmountForMaxPrice { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Rating { get; set; }

    }
}
