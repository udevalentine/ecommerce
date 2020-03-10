using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class VendorsTranslator : TranslatorBase<Vendors,VENDOR>
    {
        public override Vendors TranslateToModel(VENDOR entity)
        {
            try
            {
                Vendors model = null;
                if (entity != null)
                {
                    model = new Vendors();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Active = entity.Active;
                    model.Address = entity.Address;
                    model.PhoneNumber = entity.Phone_Number;
                    
                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override VENDOR TranslateToEntity(Vendors model)
        {
            try
            {
                VENDOR entity = null;
                if (model != null)
                {
                    entity = new VENDOR();
                    entity.Id = model.Id;
                    entity.Active = model.Active;
                    entity.Address = model.Address;
                    entity.Name = model.Name;
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
