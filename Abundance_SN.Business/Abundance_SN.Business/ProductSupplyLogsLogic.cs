using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class ProductSupplyLogsLogic : BusinessBaseLogic<ProductSupplyLogs,PRODUCT_SUPPLY_LOG>
    {
        public ProductSupplyLogsLogic()
        {
            translator = new ProductSupplyLogTranslator();
        }

       
        public bool Modify(ProductSupplyLogs prodcutSupplyLogs)
        {
            Expression<Func<PRODUCT_SUPPLY_LOG, bool>> selector = a => a.Id == prodcutSupplyLogs.Id;
            PRODUCT_SUPPLY_LOG entity = GetEntityBy(selector);


            entity.Product_Id = prodcutSupplyLogs.Id;
            entity.Quantity = prodcutSupplyLogs.Quantity;
            entity.Unit_Price = prodcutSupplyLogs.UnitPrice;


            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
