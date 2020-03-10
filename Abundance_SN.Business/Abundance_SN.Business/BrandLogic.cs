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
    public class BrandLogic : BusinessBaseLogic<Brand,BRAND>
    {
        public BrandLogic()
        {
            translator = new BrandTranslator();
        }

        public bool Modify(Brand brand)
        {
            Expression<Func<BRAND, bool>> selector = a => a.Id == brand.Id;
            BRAND entity = GetEntityBy(selector);


            entity.Name = brand.Name;
            entity.Active = brand.Active;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
