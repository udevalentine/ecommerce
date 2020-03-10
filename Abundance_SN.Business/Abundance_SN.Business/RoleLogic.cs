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
    public class RoleLogic : BusinessBaseLogic<Role,ROLE>
    {
        public RoleLogic()
        {
            translator = new RolesTranslator();
        }

        public bool Modify(Role role)
        {
            Expression<Func<ROLE, bool>> selector = a => a.Id == role.Id;
            ROLE entity = GetEntityBy(selector);


            entity.Name = role.Name;
            entity.Active = role.Active;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
