using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class StateTranslator : TranslatorBase<State,STATE>
    {
        public override State TranslateToModel(STATE entity)
        {
            try
            {
                State model = null;
                if (entity != null )
                {
                    model = new State();
                    model.StateId = entity.State_Id;
                    model.StateName = entity.State_Name;
                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public override STATE TranslateToEntity(State model)
        {
            try
            {
                STATE entity = null;
                if (model != null)
                {
                    entity =  new STATE();
                    entity.State_Id = model.StateId;
                    entity.State_Name = model.StateName;
                    entity.Nationality_Id = 1;
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
