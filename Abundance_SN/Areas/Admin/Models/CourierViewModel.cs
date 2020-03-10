using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using System.Web.Mvc;

namespace Abundance_SN.Areas.Admin.Models
{
    public class CourierViewModel
    {
        public CourierViewModel()
        {
            CourierServiceSelectListItem = Utility.PopulateCourierServiceselectListItem();
            CourierAreaSelectListItem = Utility.PopulateCourierAreaselectListItem();
            //CourierRegionSelectListItem = Utility.PopulateCourierRegionselectListItem();
            StateSelectListItem = Utility.PopulateSateSelectListItems();
            CourierChargesSelectListItem = Utility.PopulateCourierChargesselectListItem();
            //if (CourierRegion != null && CourierRegion.Region_Id > 0)
            //{
            //    CourierAreaSelectListItem = Utility.PopulateCourierAreaselectListItem(CourierRegion.Region_Id);
            //}
        }
        public CourierService CourierService { get; set; }
        //public CourierRegion CourierRegion { get; set; }
        public CourierAreaCharges CourierAreaCharges { get; set; }
        public CourierCharges CourierCharges { get; set; }
        public State State { get; set; }
        public List<SelectListItem> CourierServiceSelectListItem { get; set; }
        public List<SelectListItem> CourierAreaSelectListItem { get; set; }
        public List<SelectListItem> StateSelectListItem { get; set; }
        public List<SelectListItem> CourierChargesSelectListItem { get; set; }
        public List<CourierDetail> CourierDetails { get; set; }
    }
    public class CourierArrayModel
    {
        public string Id { get; set; }
        public string CourierServiceId { get; set; }
        public string CourierName { get; set; }
        public string CourierEmail { get; set; }
        public string CourierAddress { get; set; }
        public string CourierPhone { get; set; }
        //public string CourierRegion { get; set; }
        public string state { get; set; }
        public string CourierArea { get; set; }
        public string CourierCharges { get; set; }
        public bool IsError { get; set; }
        //public string CourierRegionId { get; set; }
        public string StateId { get; set; }
        public string CourierAreaId { get; set; }
        public string CourierChargeId { get; set; }
        public string Message { get; set; }
        

    }
}