using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class UserTranslator : TranslatorBase<User,USER>
    {
        private RolesTranslator rolesTranslator;

        public UserTranslator()
        {
            rolesTranslator = new RolesTranslator();
        }
        public override User TranslateToModel(USER entity)
        {
            try
            {
                User model = null;
                if (entity != null)
                {
                    model = new User();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Email = entity.Email;
                    model.Password = entity.Password;
                    model.Role = rolesTranslator.Translate(entity.ROLE);
                    
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override USER TranslateToEntity(User model)
        {
            try
            {
                USER entity = null;
                if (model != null)
                {
                    entity = new USER();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Email = model.Email;
                    entity.Password = model.Password;
                    entity.Role_Id = model.Role.Id;
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
