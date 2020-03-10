using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ActivitiesTranslator : TranslatorBase<Activities,ACTIVITIES>
    {
        private UserTranslator userTranslator;

        public ActivitiesTranslator()
        {
            userTranslator = new UserTranslator();
        }
        public override ACTIVITIES TranslateToEntity(Activities model)
        {
            try
            {
                ACTIVITIES entity = null;
                if (model != null)
                {
                    entity = new ACTIVITIES();
                    entity.Subject_Id = model.SubjectId;
                    entity.Subject_Type = model.SubjectType;
                    entity.User_Id = model.User.Id;
                    entity.Createed_At = model.CreatedAt;
                    entity.Updated_At = model.UpdatedAt;
                    
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public override Activities TranslateToModel(ACTIVITIES entity)
        {
            try
            {
                Activities model = null;
                if (entity != null)
                {
                    model = new Activities();
                    model.SubjectId = entity.Subject_Id;
                    model.SubjectType = entity.Subject_Type;
                    model.User = userTranslator.Translate(entity.USER);
                    model.CreatedAt = entity.Createed_At;
                    model.UpdatedAt = entity.Updated_At;

                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
