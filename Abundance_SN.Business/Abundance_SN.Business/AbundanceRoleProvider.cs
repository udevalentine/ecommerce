using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Business
{
    public class AbundanceRoleProvider : RoleProvider
    {
        private RoleLogic roleLogic;
        private UserLogic userLogic;
        public AbundanceRoleProvider()
        {

        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string email)
        {
            try
            {

                userLogic = new UserLogic();
                string[] userRoles = new string[] { userLogic.GetModelBy(u => u.Email == email).Role.Name.ToString() };
                userLogic.Dispose();
                return userRoles;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                User user = new User();
                if (username != null && roleName != null)
                {
                    userLogic = new UserLogic();
                    user = userLogic.GetModelBy(u => u.Name == username);
                    if (user != null)
                    {
                        if (user.Role.Name == roleName)
                        {
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
