using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class CourierServiceTranslator:TranslatorBase<CourierService,COURIER_SERVICE>
    {
        public override CourierService TranslateToModel(COURIER_SERVICE entity)
        {
            try
            {
                CourierService model = null;
                if (entity != null)
                {
                    model = new CourierService();
                    model.Courier_Id = entity.Courier_Id;
                    model.Courier_Name = entity.Courier_Name;
                    model.Email = entity.Email;
                    model.Phone_Number = entity.Phone_Number;
                    model.Address = entity.Address;
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override COURIER_SERVICE TranslateToEntity(CourierService model)
        {
            try
            {
                COURIER_SERVICE entity = null;
                if (model != null)
                {
                    entity = new COURIER_SERVICE();
                    entity.Courier_Id = model.Courier_Id;
                    entity.Courier_Name = model.Courier_Name;
                    entity.Email = model.Email;
                    entity.Phone_Number = model.Phone_Number;
                    entity.Address = model.Address;
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
