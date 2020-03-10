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
    public class ProductSupplyLogsTranslator : TranslatorBase<ProductSupplyLogs,PRODUCT_SUPPLY_LOGS>
    {
        private ProductTranslator productTranslator;
        private ProductOrderLogsTranslator productOrderLogsTranslator;
        private UserTranslator userTranslator;
        private VendorsTranslator vendorsTranslator;
        public ProductSupplyLogsTranslator()
        {
           productTranslator = new ProductTranslator(); 
            productOrderLogsTranslator = new ProductOrderLogsTranslator();
            userTranslator = new UserTranslator();
            vendorsTranslator = new VendorsTranslator();
        }
        public override ProductSupplyLogs TranslateToModel(PRODUCT_SUPPLY_LOGS entity)
        {
            try
            {
                ProductSupplyLogs model = null;
                if (entity != null)
                {
                    model = new ProductSupplyLogs();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.ProductOrderLogs = productOrderLogsTranslator.Translate(entity.PRODUCT_ORDER_LOGS);
                    model.Quantity = entity.Quantity;
                    model.UnitPrice = entity.Unit_Price;
                    if (entity.USER != null)
                    {
                        model.User = userTranslator.Translate(entity.USER);  
                    }
                    
                    model.CreatedAt = entity.Created_At;
                    model.SupplyDate = entity.Supply_Date;
                    model.UpdateAt = entity.Update_At;
                    model.Vendors = vendorsTranslator.Translate(entity.VENDORS);
                    

                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }

        public override PRODUCT_SUPPLY_LOGS TranslateToEntity(ProductSupplyLogs model)
        {
            try
            {
                PRODUCT_SUPPLY_LOGS entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_SUPPLY_LOGS();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Product_Order_Log_Id = model.ProductOrderLogs.Id;
                    entity.Quantity = model.Quantity;
                    entity.Unit_Price = model.UnitPrice;
                    entity.User_Id = model.User.Id;
                    entity.Created_At = model.CreatedAt;
                    entity.Supply_Date = model.SupplyDate;
                    entity.Update_At = model.UpdateAt;
                    entity.Vendor_Id = model.Vendors.Id;
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
