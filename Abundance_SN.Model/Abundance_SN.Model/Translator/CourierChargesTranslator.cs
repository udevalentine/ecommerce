using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class CourierChargesTranslator:TranslatorBase<CourierCharges,COURIER_CHARGES>
    {
        public override CourierCharges TranslateToModel(COURIER_CHARGES entity)
        {
            try
            {
                CourierCharges model = null;
                if (entity != null)
                {
                    model = new CourierCharges();
                    model.Id = entity.Id;
                    model.Charges = entity.Charges;
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override COURIER_CHARGES TranslateToEntity(CourierCharges model)
        {
            try
            {
                COURIER_CHARGES entity = null;
                if (model != null)
                {
                    entity = new COURIER_CHARGES();
                    entity.Id = model.Id;
                    entity.Charges = model.Charges;
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
