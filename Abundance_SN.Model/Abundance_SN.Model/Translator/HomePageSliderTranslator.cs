using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class HomePageSliderTranslator : TranslatorBase<HomePageSlider,HOME_PAGE_SLIDER>
    {
        public override HomePageSlider TranslateToModel(HOME_PAGE_SLIDER entity)
        {
            try
            {
                HomePageSlider model = null;
                if (entity != null)
                {
                    model = new HomePageSlider();
                    model.Id = entity.Home_Page_Slider_Id;
                    model.ImageUrl = entity.Image_Url;
                    model.Activated = entity.Activated;
                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override HOME_PAGE_SLIDER TranslateToEntity(HomePageSlider model)
        {
            try
            {
                HOME_PAGE_SLIDER entity = null;
                if (model != null)
                {
                    entity = new HOME_PAGE_SLIDER();
                    entity.Image_Url = model.ImageUrl;
                    entity.Activated = model.Activated;
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
