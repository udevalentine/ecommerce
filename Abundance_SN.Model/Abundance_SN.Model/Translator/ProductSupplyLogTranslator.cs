using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductSupplyLogTranslator : TranslatorBase<ProductSupplyLogs,PRODUCT_SUPPLY_LOG>
    {
        private ProductTranslator productTranslator;
        private ProductOrderLogTranslator productOrderLogsTranslator;
        private UserTranslator userTranslator;
        private VendorsTranslator vendorsTranslator;
        public ProductSupplyLogTranslator()
        {
           productTranslator = new ProductTranslator(); 
            productOrderLogsTranslator = new ProductOrderLogTranslator();
            userTranslator = new UserTranslator();
            vendorsTranslator = new VendorsTranslator();
        }
        public override ProductSupplyLogs TranslateToModel(PRODUCT_SUPPLY_LOG entity)
        {
            try
            {
                ProductSupplyLogs model = null;
                if (entity != null)
                {
                    model = new ProductSupplyLogs();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.ProductOrderLogs = productOrderLogsTranslator.Translate(entity.PRODUCT_ORDER_LOG);
                    model.Quantity = entity.Quantity;
                    model.UnitPrice = entity.Unit_Price;
                    if (entity.USER != null)
                    {
                        model.User = userTranslator.Translate(entity.USER);  
                    }
                   
                    model.SupplyDate = entity.Supply_Date;
                    model.Vendors = vendorsTranslator.Translate(entity.VENDOR);
                    

                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public override PRODUCT_SUPPLY_LOG TranslateToEntity(ProductSupplyLogs model)
        {
            try
            {
                PRODUCT_SUPPLY_LOG entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_SUPPLY_LOG();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Product_Order_Log_Id = model.ProductOrderLogs.Id;
                    entity.Quantity = model.Quantity;
                    entity.Unit_Price = model.UnitPrice;
                    entity.User_Id = model.User.Id;
                    entity.Supply_Date = model.SupplyDate;
                    entity.Vendor_Id = model.Vendors.Id;
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
