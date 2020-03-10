using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class UnitMeasurementLogic : BusinessBaseLogic<UnitMeasurement,UNIT_MEASUREMENT>
    {
        public UnitMeasurementLogic()
        {
            translator = new UnitMeasurementTranslator();
        }
    }
}
