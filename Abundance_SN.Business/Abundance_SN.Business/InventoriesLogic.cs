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
    public class InventoriesLogic : BusinessBaseLogic<Inventories, INVENTORIES>
    {
        public InventoriesLogic()
        {
            translator = new InventoriesTranslator();
        }

        public bool Modify(List<Product> products)
        {
            try
            {
               
                for (int i = 0; i < products.Count; i++)
                {
                    long proId = products[i].Id;
                    Expression<Func<INVENTORIES, bool>> selector = a => a.Product_Id == proId;
                    INVENTORIES entity = GetEntityBy(selector);

                    entity.Quantity =  entity.Quantity - products[i].Quantity ;
                  

                    int modifiedRecordCount = Save();
                    if (modifiedRecordCount <= 0)
                    {
                        return false;
                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }


        public bool Modify(Inventories inventories)
        {
            try
            {

                Expression<Func<INVENTORIES, bool>> selector = a => a.Product_Id == inventories.Id;
                INVENTORIES entity = GetEntityBy(selector);

                entity.Quantity = inventories.Quantity;
                entity.Product_Id = inventories.Product.Id;


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
    }
}
