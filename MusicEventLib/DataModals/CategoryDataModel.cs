using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.DataModals
{
    public class CategoryDataModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Discription { get; set; }
        public Nullable<bool> ISActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDatetime { get; set; }
        public Nullable<System.DateTime> UpdatedDatetime { get; set; }
        public Nullable<int> MainCatID { get; set; }
    }
}
