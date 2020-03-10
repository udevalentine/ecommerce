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
    public class InventoryLogic : BusinessBaseLogic<Inventory, INVENTORY>
    {
        public InventoryLogic()
        {
            translator = new InventoryTranslator();
        }

        public bool Modify(Product product, long productOptionId)
        {
            try
            {
                Expression<Func<INVENTORY, bool>> selector = a => a.Product_Id == product.Id && a.Product_Variant_Option_Id == productOptionId;
                    INVENTORY entity = GetEntityBy(selector);

                    entity.Quantity -= product.Quantity ;
                  

                    int modifiedRecordCount = Save();
                    if (modifiedRecordCount <= 0)
                    {
                        return false;
                    }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }


        public bool Modify(Inventory inventories, ProductVariantOptions productVariantOptions)
        {
            try
            {

                Expression<Func<INVENTORY, bool>> selector = a => a.Id == inventories.Id && a.Product_Variant_Option_Id == inventories.ProductVariantOptions.Id;
                INVENTORY entity = GetEntityBy(selector);

                entity.Quantity = inventories.Quantity;
                entity.Product_Id = inventories.Product.Id;
                entity.Product_Variant_Option_Id = productVariantOptions.Id;


                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }

            }
            catch (Exception)
            {
                    
                throw;
            }
            return true;
        }
        public bool Modify(Inventory inventories)
        {
            try
            {
                Expression<Func<INVENTORY, bool>> selector = a => a.Id == inventories.Id;
                INVENTORY entity = GetEntityBy(selector);
                entity.Quantity = inventories.Quantity;
                entity.Product_Id = inventories.Product.Id;
                entity.Product_Variant_Option_Id = inventories.ProductVariantOptions.Id;

                int modifiedRecordCount = Save();
                if(modifiedRecordCount<=0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
