using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;
using Microsoft.Ajax.Utilities;
using Unit = Abundance_SN.Model.Model.Unit;

namespace Abundance_SN.Models
{
    public class Utility
    {
        public const string Select = "-- Select --";
        public const string ID = "Id";
        public const string NAME = "Name";
        public const string VALUE = "Value";
        public const string TEXT = "Text";
        public const string DEFAULT_AVATAR = "/Content/Images/ts_avatar.jpg";
        public const string SelectSubcategory = "Sub-Category";
        public static void BindDropdownItem<T>(DropDownList dropDownList, T items, string dataValueField, string dataTextField)
        {
            dropDownList.Items.Clear();

            dropDownList.DataValueField = dataValueField;
            dropDownList.DataTextField = dataTextField;


            dropDownList.DataSource = items;
            dropDownList.DataBind();
        }
    


       
        public static List<SelectListItem> PopulateBrandsSelectListItem()
        {
            try
            {
                BrandLogic brandsLogic = new BrandLogic();
                List<Brand> brands = brandsLogic.GetAll().Where(x=>x.Active=true).ToList();
                if (brands == null || brands.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> brandsList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                brandsList.Add(list);

                foreach (Brand brand in brands)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = brand.Id.ToString();
                    selectList.Text = brand.Name;
                    brandsList.Add(selectList);

                }
                return brandsList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public static List<SelectListItem> PopulateCategorySelectListItem()
        {
            try
            {
                CategoryLogic categoryLogic = new CategoryLogic();
                List<Category> categories = categoryLogic.GetAll();
                if (categories == null || categories.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> brandsList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "0";
                list.Text = Select;
                brandsList.Add(list);

                foreach (Category category in categories)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = category.Id.ToString();
                    selectList.Text = category.Name;
                    brandsList.Add(selectList);

                }
                return brandsList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateUnitOfMeasurementSelectListItem()
        {
            try
            {
                UnitLogic unitMeasurementLogic = new UnitLogic();
                List<Unit> measurements = unitMeasurementLogic.GetAll();
                if (measurements == null || measurements.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> measurementlist = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                measurementlist.Add(list);

                foreach (Unit measurement in measurements)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = measurement.Id.ToString();
                    selectList.Text = measurement.Name;
                    measurementlist.Add(selectList);

                }
                return measurementlist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<SelectListItem> PopulateStoreTypeSelectListItem()
        {
            try
            {
                StoreTypeLogic storeTypeLogic = new StoreTypeLogic();
                List<StoreType> storeTypes = storeTypeLogic.GetAll();
                if (storeTypes == null || storeTypes.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> storeTypelist = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                storeTypelist.Add(list);

                foreach (StoreType storeType in storeTypes)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = storeType.Id.ToString();
                    selectList.Text = storeType.Name;
                    storeTypelist.Add(selectList);

                }
                return storeTypelist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<SelectListItem> PopulateProductTypeselectListItem()
        {
            try
            {
                ProductTypeLogic productypeLogic = new ProductTypeLogic();
                List<ProductType> productTypes = productypeLogic.GetAll();
                if (productTypes == null || productTypes.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> productTypesList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                productTypesList.Add(list);

                foreach (ProductType productType in productTypes)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = productType.Id.ToString();
                    selectList.Text = productType.Name;
                    productTypesList.Add(selectList);

                }
                return productTypesList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateProductTypeselectListItem(int categoryId)
        {
            try
            {
                ProductTypeLogic productypeLogic = new ProductTypeLogic();
                List<ProductType> productTypes = productypeLogic.GetModelsBy(p => p.Category_Id == categoryId);
                if (productTypes == null || productTypes.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> productTypesList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = SelectSubcategory;
                productTypesList.Add(list);

                foreach (ProductType productType in productTypes)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = productType.Id.ToString();
                    selectList.Text = productType.Name;
                    productTypesList.Add(selectList);

                }
                return productTypesList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateProductselectListItem()
        {
            try
            {
                ProductLogic productLogic = new ProductLogic();
                List<Product> products = productLogic.GetAll();
                if (products == null || products.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> productList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                productList.Add(list);

                foreach (Product product in products)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = product.Id.ToString();
                    selectList.Text = product.Name;
                    productList.Add(selectList);

                }
                return productList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<SelectListItem> PopulateVendorselectListItem()
        {
            try
            {
                VendorLogic vendorsLogic = new VendorLogic();
                List<Vendors> vendorses = vendorsLogic.GetAll();
                if (vendorses == null || vendorses.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> vendorList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                vendorList.Add(list);

                foreach (Vendors vendor in vendorses)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = vendor.Id.ToString();
                    selectList.Text = vendor.Name;
                    vendorList.Add(selectList);

                }
                return vendorList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateCourierServiceselectListItem()
        {
            try
            {
                CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                List<CourierService> courierServices = courierServiceLogic.GetAll();
                if (courierServices == null || courierServices.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> courierServiceList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                courierServiceList.Add(list);

                foreach (CourierService courier in courierServices)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = courier.Courier_Id.ToString();
                    selectList.Text = courier.Courier_Name;
                    courierServiceList.Add(selectList);

                }
                return courierServiceList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public static List<SelectListItem> PopulateCourierAreaselectListItem()
        {
            try
            {
                CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                List<CourierAreaCharges> courierAreaCharges = courierAreaChargesLogic.GetAll();
                if (courierAreaCharges == null || courierAreaCharges.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> courierAreaChargeList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                courierAreaChargeList.Add(list);

                foreach (CourierAreaCharges courierAreaCharge in courierAreaCharges)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = courierAreaCharge.Id.ToString();
                    selectList.Text = courierAreaCharge.Area;
                    courierAreaChargeList.Add(selectList);

                }
                return courierAreaChargeList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public static List<SelectListItem> PopulateCourierAreaselectListItem(int courierRegionId)
        //{
        //    try
        //    {
        //        CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
        //        List<CourierAreaCharges> courierAreaCharges = courierAreaChargesLogic.GetModelsBy(x=>x.Region_Id==courierRegionId);
        //        if (courierAreaCharges == null || courierAreaCharges.Count <= 0)
        //        {
        //            return new List<SelectListItem>();
        //        }

        //        List<SelectListItem> courierAreaChargeList = new List<SelectListItem>();
        //        SelectListItem list = new SelectListItem();
        //        list.Value = "";
        //        list.Text = Select;
        //        courierAreaChargeList.Add(list);

        //        foreach (CourierAreaCharges courierAreaCharge in courierAreaCharges)
        //        {
        //            SelectListItem selectList = new SelectListItem();
        //            selectList.Value = courierAreaCharge.Id.ToString();
        //            selectList.Text = courierAreaCharge.Area;
        //            courierAreaChargeList.Add(selectList);

        //        }
        //        return courierAreaChargeList;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public static List<SelectListItem> PopulateCourierChargesselectListItem()
        {
            try
            {
                CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                List<CourierCharges> courierCharges = courierChargesLogic.GetAll();
                if (courierCharges == null || courierCharges.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> courierChargeList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                courierChargeList.Add(list);

                foreach (CourierCharges courierCharge in courierCharges)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = courierCharge.Id.ToString();
                    selectList.Text = courierCharge.Charges.ToString();
                    courierChargeList.Add(selectList);

                }
                return courierChargeList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string Encrypt(string encrypData)
        {
            string data = "";
            try
            {
                string CharData = "";
                string ConChar = "";
                for (int i = 0; i < encrypData.Length; i++)
                {
                    CharData = Convert.ToString(encrypData.Substring(i, 1));
                    ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) + 128);
                    data = data + ConChar;

                }

            }
            catch (Exception ex)
            {
                data = "1";
                return data;
            }
            return data;


        }

        public static string Decrypt(string encrypData)
        {
            string data = "";
            try
            {
                string CharData = "";
                string ConChar = "";
                for (int i = 0; i < encrypData.Length; i++)
                {
                    CharData = Convert.ToString(encrypData.Substring(i, 1));
                    ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) - 128);
                    data = data + ConChar;

                }

            }
            catch (Exception ex)
            {
                data = "1";
                return data;
            }
            return data;


        }
        public static List<SelectListItem> PopulateSateSelectListItems()
        {
            try
            {
                StateLogic stateLogic = new StateLogic();
                List<State> states = stateLogic.GetAll();
                if (states == null || states.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> vendorList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                vendorList.Add(list);

                foreach (State state in states)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = state.StateId;
                    selectList.Text = state.StateName;
                    vendorList.Add(selectList);

                }
                return vendorList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateCourierStateSelectListItems()
        {
            try
            {
                StateLogic stateLogic = new StateLogic();
                CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                var courierStates = courierAreaChargesLogic.GetAll().Select(x=>x.State.StateId).Distinct().ToList();
                List<State> states = new List<State>();
                //if (courierStates == null || courierStates.Count <= 0)
                //{
                //    return new List<SelectListItem>();
                //}
                if(courierStates !=null && courierStates.Count>0)
                {
                    for(int i=0; i<courierStates.Count; i++)
                    {
                        var stsateCode=courierStates[i];
                        var courierStat=stateLogic.GetModelBy(x => x.State_Id == stsateCode);
                        states.Add(courierStat);
                    }
                }
                List<SelectListItem> courierStateList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                courierStateList.Add(list);

                foreach (State state in states)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = state.StateId;
                    selectList.Text = state.StateName;
                    courierStateList.Add(selectList);

                }
                return courierStateList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SelectListItem> PopulateRoleSelectListItems()
        {
            try
            {
                RoleLogic roleLogic = new RoleLogic();
                List<Role> roles = roleLogic.GetAll();
                if (roles == null || roles.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> roleList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                roleList.Add(list);

                foreach (Role role in roles)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = role.Id.ToString();
                    selectList.Text = role.Name;
                    roleList.Add(selectList);

                }
                return roleList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateProductSample()
        {
            try
            {
                StoreSetupProductSampleLogic storeSetupProductSampleLogic = new StoreSetupProductSampleLogic();
                List<StoreSetupProductSample> storeSetupProductSamples = storeSetupProductSampleLogic.GetAll();
                if (storeSetupProductSamples == null || storeSetupProductSamples.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> productSampleList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                productSampleList.Add(list);

                foreach (StoreSetupProductSample productSample in storeSetupProductSamples)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = productSample.Id.ToString();
                    selectList.Text = productSample.Name;
                    productSampleList.Add(selectList);

                }
                return productSampleList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<SelectListItem> PopulateDeliveryStatuSelectListItems()
        {
            try
            {
                DeliveryStatusLogic deliveryStatusLogic = new DeliveryStatusLogic();
                List<DeliveryStatus> deliveyStatus  = deliveryStatusLogic.GetAll();
                if (deliveyStatus == null || deliveyStatus.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> deliveryStatusSelectList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = Select;
                deliveryStatusSelectList.Add(list);

                foreach (DeliveryStatus deliveryStatus in deliveyStatus)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = deliveryStatus.Id.ToString();
                    selectList.Text = deliveryStatus.Name;
                    deliveryStatusSelectList.Add(selectList);

                }
                return deliveryStatusSelectList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DateTime ConvertToDate(string date)
        {
            DateTime newDate = new DateTime();
            try
            {
                //newDate = DateTime.Parse(date);
                string[] dateSplit = date.Split('/');
                newDate = new DateTime(Convert.ToInt32(dateSplit[2]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
            }
            catch (Exception ex)
            {
                throw;
            }

            return newDate;
        }
    }
}