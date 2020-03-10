using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abundance_SN.Areas.Admin.Models
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public int NotificationType { get; set; }

    }
}