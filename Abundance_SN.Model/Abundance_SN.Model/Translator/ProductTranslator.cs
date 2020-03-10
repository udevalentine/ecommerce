using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductTranslator : TranslatorBase<Product, PRODUCT>
    {
        private BrandTranslator brandsTranslator;
        private ProductTypeTranslator productTypeTranslator;
        private UnitMeasurementTranslator unitMeasurementTranslator;

        public ProductTranslator()
        {
            brandsTranslator = new BrandTranslator();
            productTypeTranslator = new ProductTypeTranslator();
            unitMeasurementTranslator = new UnitMeasurementTranslator();

        }
        public override PRODUCT TranslateToEntity(Product model)
        {
            try
            {
                PRODUCT entity = null;
                if (model != null)
                {
                    entity = new PRODUCT();
                    entity.Id = model.Id;
                    entity.Active = model.Active;
                    entity.Brand_Id = model.Brand.Id;
                    entity.Can_Be_Delivered = model.CanBedelivered;
                    entity.Additional_Information = model.AdditionalInformation;
                    entity.Description = model.Description;
                    entity.Name = model.Name;
                    entity.Image_Url = model.ImageUrl;
                    entity.Product_Type_Id = model.ProductType.Id;
                    entity.Reorder_Level = model.ReorderLevel;
                    entity.Shipping = model.Shipping;
                    entity.Unit_Id = model.UnitMeasurement.Id;
                    if (model.Visits != null)
                    {
                        entity.Visits = model.Visits;
                    }
                    if (model.Weight != null)
                    {
                        entity.Weight = model.Weight;
                    }
                    if (model.Slug != null)
                    {
                        entity.Slug = model.Slug;
                    }
                    entity.sku = model.Sku;
                    entity.Price = model.Price;
                    entity.MaxPrice = model.MaxPrice;
                    entity.Discount = model.Discount;
                    entity.Size = model.Size;
                    entity.Color = model.Color;

                }
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override Product TranslateToModel(PRODUCT entity)
        {
            Product model = null;
            try
            {
                if (entity != null)
                {
                    model = new Product();
                    model.Active = entity.Active;
                    model.Id = entity.Id;
                    model.Brand = brandsTranslator.TranslateToModel(entity.BRAND);
                    model.CanBedelivered = entity.Can_Be_Delivered;
                    model.AdditionalInformation = entity.Additional_Information;
                    model.Description = entity.Description;
                    model.Name = entity.Name;
                    model.ImageUrl = entity.Image_Url;
                    model.ProductType = productTypeTranslator.Translate(entity.PRODUCT_TYPE);
                    model.ReorderLevel = entity.Reorder_Level;
                    model.Shipping = entity.Shipping;
                    model.Slug = entity.Slug;
                    model.UnitMeasurement = unitMeasurementTranslator.Translate(entity.UNIT);
                    model.Visits = entity.Visits;
                    model.Weight = entity.Weight;
                    model.Sku = entity.sku;
                    model.Price = entity.Price;
                    model.MaxPrice = entity.MaxPrice;
                    model.Discount = (int)entity.Discount;
                    if (model.Discount > 0)
                    {
                        int productDiscount = (int)(model.Price * model.Discount / 100);
                        model.DiscountAmount = (int)(model.Price - productDiscount);
                        if(model.MaxPrice !=null)
                        {
                            int productMaxDiscount = (int)(model.MaxPrice * model.Discount / 100);
                            model.DiscountAmountForMaxPrice = (int)(model.MaxPrice - productMaxDiscount);
                        }
                        
                        

                    }
                    else
                    {
                        model.DiscountAmount = (int)model.Price;
                        if(model.MaxPrice!=null)
                        {
                            model.DiscountAmountForMaxPrice = (int)model.MaxPrice;
                        }
                        
                    }
                    model.Size = entity.Size;
                    model.Color = entity.Color;

                }
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
