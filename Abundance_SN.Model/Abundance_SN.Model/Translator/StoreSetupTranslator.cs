using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class StoreSetupTranslator : TranslatorBase<StoreSetup,STORE_SETUP>
    {
        private PersonTranslator _personTranslator;

        public StoreSetupTranslator()
        {
            _personTranslator = new PersonTranslator();
        }
        public override STORE_SETUP TranslateToEntity(StoreSetup model)
        {
            try
            {
                STORE_SETUP entity = null;
                if (model != null)
                {
                    entity = new STORE_SETUP();
                    entity.Store_Setup_Id = model.Id;
                    entity.Shop_Name = model.ShopName;
                    entity.Store_ProductType = model.StoreProductType;
                    entity.Person_Id = model.Person.Id;
                    entity.BusinessEmail = model.BusinessEmail;
                    entity.BusinessName = model.BusinessName;
                    entity.BusinessPhone = model.BusinessPhone;
                    entity.BusinessRCNo = model.BusinessRcNo;
                    entity.StoreLogoUrl = model.StoreLogoUrl;
                    entity.About_Us = model.AboutUs;
                    entity.Store_Address = model.StoreAddress;
                }
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override StoreSetup TranslateToModel(STORE_SETUP entity)
        {
            try
            {
                StoreSetup model = null;
                if (entity != null)
                {
                    model = new StoreSetup();
                    model.Id = entity.Store_Setup_Id;
                    model.ShopName = entity.Shop_Name;
                    model.StoreProductType = entity.Store_ProductType;
                    model.Person = _personTranslator.Translate(entity.PERSON);
                    model.BusinessEmail = entity.BusinessEmail;
                    model.BusinessName = entity.BusinessName;
                    model.BusinessPhone = entity.BusinessPhone;
                    model.BusinessRcNo = entity.BusinessRCNo;
                    model.StoreLogoUrl = entity.StoreLogoUrl;
                    model.AboutUs = entity.About_Us;
                    model.StoreAddress = entity.Store_Address;

                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
