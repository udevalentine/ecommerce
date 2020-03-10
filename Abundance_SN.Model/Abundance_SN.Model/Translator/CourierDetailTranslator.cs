using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Translator
{
    public class CourierDetailTranslator : TranslatorBase<CourierDetail, COURIER_DETAIL>
    {
        public readonly CourierServiceTranslator courierServiceTranslator;
        public readonly CourierChargesTranslator courierChargesTranslator;
        public readonly CourierAreaChargesTranslator courierAreaChargesTranslator;
        public readonly StateTranslator stateTranslator;
        public CourierDetailTranslator()
        {
            courierAreaChargesTranslator = new CourierAreaChargesTranslator();
            courierChargesTranslator = new CourierChargesTranslator();
            courierServiceTranslator = new CourierServiceTranslator();
            stateTranslator = new StateTranslator();
        }
        public override CourierDetail TranslateToModel(COURIER_DETAIL entity)
        {
            try
            {
                CourierDetail model = null;
                if (entity != null)
                {
                    model = new CourierDetail();
                    model.Id = entity.Id;
                    model.CourierAreaCharges = courierAreaChargesTranslator.Translate(entity.COURIER_AREA_CHARGES);
                    model.CourierCharges = courierChargesTranslator.Translate(entity.COURIER_CHARGES);
                    model.CourierService = courierServiceTranslator.Translate(entity.COURIER_SERVICE);
                    model.State = stateTranslator.Translate(entity.STATE);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public override COURIER_DETAIL TranslateToEntity(CourierDetail model)
        {
            try
            {
                COURIER_DETAIL entity = null;
                if (model != null)
                {
                    entity = new COURIER_DETAIL();
                    entity.Id = model.Id;
                    entity.Area_Id = model.CourierAreaCharges.Id;
                    entity.Courier_Id = model.CourierService.Courier_Id;
                    entity.Charge_Id = model.CourierCharges.Id;
                    entity.State_Id = model.State.StateId;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}