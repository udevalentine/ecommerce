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
    public class ProductTypeLogic : BusinessBaseLogic<ProductType,PRODUCT_TYPE>
    {
        public ProductTypeLogic()
        {
            translator = new ProductTypeTranslator();
        }

        public bool Modify(ProductType productType)
        {
            Expression<Func<PRODUCT_TYPE, bool>> selector = a => a.Id == productType.Id;
            PRODUCT_TYPE entity = GetEntityBy(selector);


            entity.Name = productType.Name;
            entity.Active = productType.Active;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
