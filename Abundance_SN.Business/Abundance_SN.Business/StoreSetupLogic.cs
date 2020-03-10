using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System.Linq.Expressions;

namespace Abundance_SN.Business
{
    public class StoreSetupLogic:BusinessBaseLogic<StoreSetup, STORE_SETUP>
    {
        public StoreSetupLogic() 
        { 
            translator = new StoreSetupTranslator();
        }
        public bool Modify(StoreSetup storeSetup)
        {
            try
            {
                Expression<Func<STORE_SETUP, bool>> selector = a => a.Store_Setup_Id == storeSetup.Id;
                STORE_SETUP entity = GetEntityBy(selector);
                entity.Store_Setup_Id = storeSetup.Id;
                entity.Shop_Name = storeSetup.ShopName;
                entity.StoreLogoUrl = storeSetup.StoreLogoUrl;
                entity.Store_ProductType = storeSetup.StoreProductType;
                entity.BusinessEmail = storeSetup.BusinessEmail;
                entity.BusinessName = storeSetup.BusinessName;
                entity.BusinessPhone = storeSetup.BusinessPhone;
                entity.BusinessRCNo = storeSetup.BusinessRcNo;
                entity.About_Us = storeSetup.AboutUs;
                entity.Store_Address = storeSetup.StoreAddress;
                entity.Person_Id = storeSetup.Person.Id;
                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}