using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class HomePageSliderLogic: BusinessBaseLogic<HomePageSlider,HOME_PAGE_SLIDER>
    {
        public HomePageSliderLogic()
        {
            translator = new HomePageSliderTranslator();
        }

        public bool Modify(HomePageSlider homePageSlider)
        {
            try
            {
                Expression<Func<HOME_PAGE_SLIDER, bool>> selector = a => a.Home_Page_Slider_Id == homePageSlider.Id;
                HOME_PAGE_SLIDER entity = GetEntityBy(selector);
                entity.Home_Page_Slider_Id = homePageSlider.Id;
                entity.Activated = homePageSlider.Activated;

                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
