using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class CourierAreaChargesTranslator : TranslatorBase<CourierAreaCharges, COURIER_AREA_CHARGES>
    {
        private StateTranslator stateTranslator;
        //private CourierRegionTranslator courierRegionTranslator;
        private CourierServiceTranslator courierServiceTranslator;

        public CourierAreaChargesTranslator()
        {
            //courierRegionTranslator = new CourierRegionTranslator();
            courierServiceTranslator = new CourierServiceTranslator();
            stateTranslator = new StateTranslator();
        }
        public override CourierAreaCharges TranslateToModel(COURIER_AREA_CHARGES entity)
        {
            try
            {
                CourierAreaCharges model = null;
                if (entity != null)
                {
                    model = new CourierAreaCharges();
                    model.Id = entity.Id;
                    model.Area = entity.Area;
                    model.Charger = entity.Charger;
                    model.State = stateTranslator.Translate(entity.STATE);
                    //model.CourierRegion = courierRegionTranslator.Translate(entity.COURIER_REGION);
                    model.CourierId = entity.Courier_Id;
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override COURIER_AREA_CHARGES TranslateToEntity(CourierAreaCharges model)
        {
            try
            {
                COURIER_AREA_CHARGES entity = null;
                if (model != null)
                {
                    entity = new COURIER_AREA_CHARGES();
                    entity.Id = model.Id;
                    entity.Area = model.Area;
                    entity.Charger = model.Charger;
                    entity.State_Id = model.State.StateId;
                    //entity.Region_Id = model.CourierRegion.Region_Id;
                    entity.Courier_Id = model.CourierId;

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