using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicEventApp.ViewModels
{
    public class EventViewModal
    {
        public int EventID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int CategoryID { get; set; }
        public string EventName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.TimeSpan> Starttime { get; set; }
        public Nullable<System.TimeSpan> Endtime { get; set; }
        public string Dealtext { get; set; }
        public string Percentage { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string location { get; set; }
        public string Image { get; set; }
        public bool ISActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDatetime { get; set; }
        public Nullable<System.DateTime> UpdatedDatetime { get; set; }
        public string TicketUrl { get; set; }
        public bool IsTmaster { get; set; }
        public Nullable<int> MasterEventID { get; set; }
        public Nullable<long> ViewCount { get; set; }
        public string CategoryName { get; set; }
        public string MainCategoryName { get; set; }
        public int MainCatID { get; set; }
        public Nullable<int> IsNew { get; set; }
        //public string Discription { get; set; }
    }
}