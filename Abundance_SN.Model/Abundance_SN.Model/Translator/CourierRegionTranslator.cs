using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Entity;

namespace Abundance_SN.Model.Translator
{
    public class CourierRegionTranslator:TranslatorBase<CourierRegion, COURIER_REGION>
    {
        private CourierServiceTranslator courierServiceTranslator;

        public CourierRegionTranslator()
        {
            courierServiceTranslator = new CourierServiceTranslator();
        }
        public override CourierRegion TranslateToModel(COURIER_REGION entity)
        {
            try
            {
                CourierRegion model = null;
                if (entity != null)
                {
                    model = new CourierRegion();
                    model.Region_Id = entity.Region_Id;
                    model.Region_Name = entity.Region_Name;
                    model.CourierService = courierServiceTranslator.Translate(entity.COURIER_SERVICE);
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override COURIER_REGION TranslateToEntity(CourierRegion model)
        {
            try
            {
                COURIER_REGION entity = null;
                if (model != null)
                {
                    entity = new COURIER_REGION();
                    entity.Region_Id = model.Region_Id;
                    entity.Region_Name = model.Region_Name;
                    entity.Courier_Id = model.CourierService.Courier_Id;
               
                    
                 
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
