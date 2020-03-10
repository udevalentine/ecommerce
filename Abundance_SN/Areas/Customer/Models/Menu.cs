using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Areas.Customer.Models
{
    public static class Menu
    {

        public static List<Brands> GetMenuList()
        {
            var brandList = new List<Brands>();
            try
            {
                BrandsLogic brandLogic = new BrandsLogic();
                CategoryLogic categoryLogic = new CategoryLogic();
                brandList = brandLogic.GetAll();

                foreach (Brands brand in brandList)
                {
                    long brandId = brand.Id;
                    List<Category> categories = categoryLogic.GetModelsBy(c => c.Product_Type_Id == brandId);

                    if (categories.Count > 0)
                    {
                        foreach (Category categoryItems in categories)
                        {
                            brand.Category.Add(categoryItems);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return brandList;
        }
    }
}