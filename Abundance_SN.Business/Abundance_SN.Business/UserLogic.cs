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
    public class UserLogic : BusinessBaseLogic<User,USER>
    {
        public UserLogic()
        {
            translator = new UserTranslator();
        }
        public bool ValidateUser(string Username, string Password)
        {
            try
            {
                Expression<Func<USER, bool>> selector = p => p.Email == Username && p.Password == Password;
                User UserDetails = GetModelBy(selector);
                if (UserDetails != null && UserDetails.Password != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool ChangeUserPassword(User user)
        {
            try
            {
                Expression<Func<USER, bool>> selector = p => p.Email == user.Email;
                USER userEntity = GetEntityBy(selector);
                if (userEntity == null || userEntity.Id <= 0)
                {
                    throw new Exception(NoItemFound);
                }

                userEntity.Name = user.Name;
                userEntity.Password = user.Password;
                userEntity.Email = user.Email;
                userEntity.Role_Id = user.Role.Id;
                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    throw new Exception(NoItemModified);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckDuplicateEmail(string email)
        {
            try
            {
                Expression<Func<USER, bool>> selector = r => r.Email == email;
                User userDetails = GetModelBy(selector);
                if (userDetails != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
