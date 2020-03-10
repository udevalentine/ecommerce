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
    public class ProductOrderLogLogic : BusinessBaseLogic<ProductOrderLogs,PRODUCT_ORDER_LOG>
    {
        public ProductOrderLogLogic()
        {
            translator = new ProductOrderLogTranslator();
        }

        public bool Modify(ProductOrderLogs productOrderLogs)
        {
            Expression<Func<PRODUCT_ORDER_LOG, bool>> selector = a => a.Id == productOrderLogs.Id;
            PRODUCT_ORDER_LOG entity = GetEntityBy(selector);

            entity.Product_Id = productOrderLogs.Id;
            entity.Quantity = productOrderLogs.Quantity;
            entity.Unit_Price = productOrderLogs.UnitPrice;
            

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
