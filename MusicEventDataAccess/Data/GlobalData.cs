using MusicEventDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventDataAccess.Data
{
    public sealed class GlobalData
    {
        private static readonly Lazy<GlobalData> lazy =
            new Lazy<GlobalData>(() => new GlobalData());

        private MusicEventEntities db { get; set; }

        public static GlobalData Instance { get { return lazy.Value; } }

        public List<MainCategory> MainCategories { get; set; }
        public List<Category> SubCategories { get; set; }
                

        private GlobalData()
        {
            db = new MusicEventEntities();
            MainCategories = db.MainCategories.ToList();
            SubCategories = db.Categories.ToList();
        }
    }
}
