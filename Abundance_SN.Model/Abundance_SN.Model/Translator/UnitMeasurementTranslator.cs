using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class UnitMeasurementTranslator : TranslatorBase<Unit,UNIT>
    {
        public override UNIT TranslateToEntity(Unit model)
        {
            try
            {
                UNIT entity = null;
                if (model!= null)
                {
                    entity = new UNIT();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Active = model.Active;
                  
                }
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            } 
        }

        public override Unit TranslateToModel(UNIT entity)
        {
            try
            {
                Unit model = null;
                if (entity != null)
                {
                    model = new Unit();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Active = entity.Active;

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
