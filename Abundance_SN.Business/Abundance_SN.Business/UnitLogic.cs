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
    public class UnitLogic : BusinessBaseLogic<Unit,UNIT>
    {
        public UnitLogic()
        {
            translator = new UnitMeasurementTranslator();
        }

        public bool Modify(Unit unit)
        {
            Expression<Func<UNIT, bool>> selector = a => a.Id == unit.Id;
            UNIT entity = GetEntityBy(selector);


            entity.Name = unit.Name;
            entity.Active = unit.Active;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
        
    }
}
