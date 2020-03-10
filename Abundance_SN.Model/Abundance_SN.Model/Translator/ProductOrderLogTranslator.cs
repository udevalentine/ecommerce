using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductOrderLogTranslator : TranslatorBase<ProductOrderLogs,PRODUCT_ORDER_LOG>
    {
        private ProductTranslator productTranslator;
        private VendorsTranslator vendorsTranslator;

        public ProductOrderLogTranslator()
        {
            productTranslator = new ProductTranslator();
            vendorsTranslator = new VendorsTranslator();
        }
        public override ProductOrderLogs TranslateToModel(PRODUCT_ORDER_LOG entity)
        {
            try
            {
                ProductOrderLogs model = null;
                if (entity != null)
                {
                    model = new ProductOrderLogs();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.DateRequested = entity.Date_Requested;
                    model.Quantity = entity.Quantity;
                    model.UnitPrice = entity.Unit_Price;
                    model.Vendors = vendorsTranslator.Translate(entity.VENDOR);
                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override PRODUCT_ORDER_LOG TranslateToEntity(ProductOrderLogs model)
        {
            try
            {
                PRODUCT_ORDER_LOG entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_ORDER_LOG();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Date_Requested = model.DateRequested;
                    entity.Quantity = model.Quantity;
                    entity.Unit_Price = model.UnitPrice;
                    entity.Vendor_Id = model.Vendors.Id;
                    entity.User_Id = model.User.Id;
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
