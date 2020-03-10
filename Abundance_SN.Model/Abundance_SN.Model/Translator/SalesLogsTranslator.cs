using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class SalesLogsTranslator : TranslatorBase<SalesLogs, SALES_LOG>
    {
        private ProductTranslator productTranslator;
        private UserTranslator userTranslator;
        private ProductVariantOptionsTranslator prodcuVariantOptionsTranslator;

        public SalesLogsTranslator()
        {
            productTranslator = new ProductTranslator();
            userTranslator = new UserTranslator();
            prodcuVariantOptionsTranslator = new ProductVariantOptionsTranslator();
        }
        public override SalesLogs TranslateToModel(SALES_LOG entity)
        {
            try
            {
                SalesLogs model = null;
                if (entity != null)
                {
                    model = new SalesLogs();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.Amount = entity.Amount;
                    model.CartHash = entity.Cart_Hash;
                    model.Discount = entity.discount;
                    model.Notes = entity.Notes;
                    model.Quantity = entity.Quantity;
                    model.UnitPrice = entity.Unit_Price;
                    model.User = userTranslator.Translate(entity.USER);
                    model.DateOrdered = entity.Date_Ordered;
                    model.ProductVariantOptions = prodcuVariantOptionsTranslator.Translate(entity.PRODUCT_VARIANT_OPTIONS); 

                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override SALES_LOG TranslateToEntity(SalesLogs model)
        {
            try
            {
                SALES_LOG entity = null;
                if (model != null)
                {
                    entity = new SALES_LOG();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Amount = model.Amount;
                    entity.Cart_Hash = model.CartHash;
                    entity.discount = model.Discount;
                    entity.Notes = model.Notes;
                    entity.Quantity = model.Quantity;
                    entity.Unit_Price = model.UnitPrice;
                    entity.User_Id = model.User.Id;
                    entity.Date_Ordered = DateTime.Now;
                    entity.Product_Variant_Option_Id = model.ProductVariantOptions.Id;
                }
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
