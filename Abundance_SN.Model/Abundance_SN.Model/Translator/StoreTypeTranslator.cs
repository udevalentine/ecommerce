using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Translator
{
    public class StoreTypeTranslator:TranslatorBase<StoreType,STORE_TYPE>
    {
        public override StoreType TranslateToModel(STORE_TYPE entity)
        {
            try
            {
                StoreType model = null;
                if (entity != null)
                {
                    model = new StoreType();
                    model.Id = entity.Id;
                    model.Name = entity.TypeName;
                    model.Active = entity.Active;
                }
                return model;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public override STORE_TYPE TranslateToEntity(StoreType model)
        {
            try
            {
                STORE_TYPE entity = null;
                if (model != null)
                {
                    entity = new STORE_TYPE();
                    entity.Id = model.Id;
                    entity.TypeName = model.Name;
                    entity.Active = model.Active;
                }
                return entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}