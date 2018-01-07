using AutoMapper;
using MusicEventDataAccess;
using MusicEventDataAccess.Data;
using MusicEventLib.DataModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.Helper
{
    public static class GlobalDataHelper
    {
        public static List<MainCategoryDataModel> GetMainCategoryList()
        {
            return Mapper.Map<List<MainCategory>, List<MainCategoryDataModel>>(GlobalData.Instance.MainCategories);
        }

        public static List<CategoryDataModel> GetCategoryList()
        {
            return Mapper.Map<List<Category>, List<CategoryDataModel>>(GlobalData.Instance.SubCategories);
        }
    }
}
