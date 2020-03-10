using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Translator
{
    public class ReorderNotificationTranslator : TranslatorBase<ReorderNotification, REORDER_NOTIFICATION>
    {
        public ReorderNotificationTranslator()
        {

        }
        public override ReorderNotification TranslateToModel(REORDER_NOTIFICATION entity)
        {
            try
            {
                ReorderNotification model = null;
                if (entity != null)
                {
                    model = new ReorderNotification();
                    model.Id = entity.Id;
                    model.Message = entity.Message;
                    model.Status = entity.Status;
                    model.NotificationDate = entity.DateTime;
                    model.NotificationType = entity.NotificationType;
                    model.OrderNumber = entity.Order_Number;
                }
                return model;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public override REORDER_NOTIFICATION TranslateToEntity(ReorderNotification model)
        {
            try
            {
                REORDER_NOTIFICATION entity = null;
                if (model != null)
                {
                    entity = new REORDER_NOTIFICATION();
                    entity.Id = model.Id;
                    entity.Message = model.Message;
                    entity.Status = model.Status;
                    entity.DateTime = DateTime.Now;
                    entity.NotificationType = model.NotificationType;
                    entity.Order_Number = model.OrderNumber;
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
