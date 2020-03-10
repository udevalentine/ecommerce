using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace Abundance_SN.Business
{
    public class ProductVariantLogic : BusinessBaseLogic<ProductVariant,PRODUCT_VARIANT>
    {
        public ProductVariantLogic()
        {
            translator = new ProductVariantTranslator();
        }


        public bool Modify(ProductVariant productVariant)
        {
            try
            {
                Expression<Func<PRODUCT_VARIANT, bool>> selector = a => a.Product_Variant_Id == productVariant.Id;
                PRODUCT_VARIANT entity = GetEntityBy(selector);

                if (productVariant.Value1 != null)
                {
                    entity.Value_1 = productVariant.Value1;
                }
                if (productVariant.Value2 != null)
                {
                    entity.Value_2 = productVariant.Value2;
                }
                if (productVariant.Value3 != null)
                {
                    entity.Value_3 = productVariant.Value3;
                }

                int modifiedCount = Save();
                if (modifiedCount > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return false;
        }
    }
}
