using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using PagedList;

namespace Abundance_SN.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

            PopulateProductTypeSelectListItems = Utility.PopulateProductTypeselectListItem();
            PopulateBrandsSelectListItems = Utility.PopulateBrandsSelectListItem();
            PopulateUnitMeasurementSelectListItems = Utility.PopulateUnitOfMeasurementSelectListItem();
            PopulateProductSelectListItems = Utility.PopulateProductselectListItem();
            PopulateVendorSelectListItems = Utility.PopulateVendorselectListItem();
            PopulateSateSelectListItems = Utility.PopulateSateSelectListItems();
            PopulateCategorySelectListItem = Utility.PopulateCategorySelectListItem();
            populateCourierServiceSelectListItem = Utility.PopulateCourierServiceselectListItem();
            PopulateCourierStateSelectListItem = Utility.PopulateCourierStateSelectListItems();

        }

        public List<IGrouping<int, Product>> GroupProducts { get; set; }
        public List<IGrouping<long, Product>> GroupBrands { get; set; }
        public List<IGrouping<string, CustomerOrder>> GroupOrder { get; set; }
        public List<string> GroupCountString { get; set; }
        public List<long> GroupCount { get; set; }
        public List<int> GroupCountInt { get; set; } 
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
       
        public Person Person { get; set; }
        public List<SelectListItem> PopulateSateSelectListItems { get; set; }
        public List<SelectListItem> PopulateProductTypeSelectListItems { get; set; }
        public List<SelectListItem> PopulateBrandsSelectListItems { get; set; }
        public List<SelectListItem> PopulateUnitMeasurementSelectListItems { get; set; }
        public List<SelectListItem> PopulateDeliverySeriveSelectListItem { get; set; }
        public List<SelectListItem> PopulateVendorSelectListItems { get; set; }
        public List<SelectListItem> PopulateProductSelectListItems { get; set; }
        public List<SelectListItem> PopulateCategorySelectListItem { get; set; }
        public List<SelectListItem> populateCourierServiceSelectListItem { get; set; }
        public List<SelectListItem> PopulateCourierStateSelectListItem { get; set; }
        public List<SalesLogs> SalesLogs { get; set; }
        public List<Vendors> VendorList { get; set; }
        public Vendors Vendors { get; set; }
        public WareHouse WareHouse { get; set; }
        public List<WareHouse> WareHouses { get; set; }
        public ProductOrderLogs ProductOrderLogs { get; set; }
        public List<ProductOrderLogs> ProductOrderLogses { get; set; }
        public ProductSupplyLogs ProductSupplyLogs { get; set; }
        public List<ProductSupplyLogs> ProductSupplyLogses { get; set; }
        public ProductType ProductType { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }
        public List<Inventory> Inventorieses { get; set; }
        public Inventory Inventory { get; set; }
        public List<Unit> Units { get; set; }
        public Unit Unit { get; set; }
        public Brand Brand { get; set; }
        public List<Brand> Brands { get; set; }
        public Payment Payment { get; set; }
        public List<Payment> Payments { get; set; } 
        public Paystack PayStack { get; set; }
        public List<Paystack> PayStackPayments { get; set; }
        public string ScannedFilePath { get; set; }
        public List<ProductUploadFormat> UploadFormat { get; set; }
        public List<DeliveryAddress> DeliveryAddresses { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public ProductImageGallary ProductImageGallary { get; set; }
        public HomePageSlider HomePageSlider { get; set; }
        public List<HomePageSlider> HomePageSliders { get; set; }
        public List<CustomerReview> CustomerReviews { get; set; }
        public CustomerReview CustomerReview { get; set; }
        public ProductVariant ProductVariant { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }
        public HttpPostedFileBase MyFile { get; set; }
        public HttpPostedFileBase MyFile2 { get; set; }
        public List<ProductImageGallary> ProductImageGallaries { get; set; }
        public string MiniFilterValue { get; set; }
        public string MaxFilterValue { get; set; }
        public ProductVariantOptions ProductVariantOptions { get; set; }
        public List<ProductVariantOptions> ProductVariantOptionses { get; set; }
        public IPagedList<Product> PagedProducts { get; set; }
        public IPagedList<Inventory> PagedInventorys { get; set; }
        public List<Category> Categorys { get; set; }
        public CourierService CourierService { get; set; }
    }

    public partial class ArrayJsonView
    {


        public string id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string item_name { get; set; }
        public string price { get; set; }
        public string currency_code { get; set; }
        public int quantity { get; set; }
        public string amount { get; set; }
        public string item_Id { get; set; }
        public string ProductImageUrl { get; set; }
        public string Price { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string CustomerIdEncrypted { get; set; }
        public string ErrorMessage { get; set; }
        public string paymentMode { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string mobile { get; set; }
        public string landMark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public string delivery { get; set; }
        public string size { get; set; }
        public string PayStackUrl { get; set; }

    }

    public partial class CheckOutJson
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string image { get; set; }
        public string productOptionId { get; set; }
    }
    public class ProductModel
    {
        public string productName { get; set; }
        public string categoryId { get; set; }
        public string productTypeId { get; set; }
        public string productBrand { get; set; }
        [StringLength(150)]
        public string productDescription { get; set; }
        public string productAdditionalInfomation { get; set; }
        public string productPrice { get; set; }
        public string MaxPrice { get; set; }
        public string productDiscount { get; set; }
        public string productWeight { get; set; }
        public string unitOfMeasurement { get;set; }
        public string reOrderLevel { get; set; }
        public string canBeDelivered { get; set; }
        public string shipping { get; set; }
        public string productSku { get;set; }
        public string activated { get; set; }
        public string productVariantKey1 { get; set; }
        public string productVariantKey2 { get; set; }
        public string productVariantKey3 { get; set; }
        public string productVariantValue1 { get; set; }
        public string productVariantValue2 { get; set; }
        public string productVariantValue3 { get; set; }
        public string Type { get; set; }
        public string select { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Barcode { get; set; }
        public string filePath { get; set; }
        public string Quantity { get; set; }

    }

}