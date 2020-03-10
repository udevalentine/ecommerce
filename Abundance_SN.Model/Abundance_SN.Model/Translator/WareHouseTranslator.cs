using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class WareHouseTranslator : TranslatorBase<WareHouse,WARE_HOUSE>
    {
        public override WareHouse TranslateToModel(WARE_HOUSE entity)
        {
            try
            {
                WareHouse model = null;
                if (entity != null)
                {
                    model = new WareHouse();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Active = entity.Active;
                    model.Address = entity.Address;
                    model.CreatedAt = entity.Created_At;
                    model.UpdatedAt = entity.Updated_At;
                    model.PhoneNumber = entity.Phone_Number;

                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override WARE_HOUSE TranslateToEntity(WareHouse model)
        {
            try
            {
                WARE_HOUSE entity = null;
                if (model != null)
                {
                    entity = new WARE_HOUSE();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Active = model.Active;
                    entity.Address = model.Address;
                    entity.Created_At = model.CreatedAt;
                    entity.Updated_At = model.UpdatedAt;
                    entity.Phone_Number = model.PhoneNumber;
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
