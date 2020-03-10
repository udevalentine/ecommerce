using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System.Linq.Expressions;

namespace Abundance_SN.Business
{
    public class CategoryLogic:BusinessBaseLogic<Category,CATEGORY>
    {
       public CategoryLogic()
        {
            translator = new CategoryTranslator();
        }
        public bool Modify(Category category)
        {
            Expression<Func<CATEGORY, bool>> selector = a => a.Category_Id == category.Id;
            CATEGORY entity = GetEntityBy(selector);


            entity.Category_Name =category.Name;
            entity.Category_Description = category.Description;
            entity.Image_Url = category.ImageUrl;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
