using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Business
{
    public class StoreTypeLogic: BusinessBaseLogic<StoreType, STORE_TYPE>
    {
        public StoreTypeLogic()
        {
            translator = new StoreTypeTranslator();
        }
        public bool Modify(StoreType storeType)
        {
            Expression<Func<STORE_TYPE, bool>> selector = a => a.Id == storeType.Id;
            STORE_TYPE entity = GetEntityBy(selector);
            entity.TypeName = storeType.Name;
            entity.Active = storeType.Active;
            int modifiedRecordCount = Save();
                 if (modifiedRecordCount <= 0)
                 {
                     return false;
                 }
            return true;
        }
    }
}
