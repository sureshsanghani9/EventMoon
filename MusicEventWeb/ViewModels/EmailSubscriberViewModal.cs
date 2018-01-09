using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicEventApp.ViewModels
{
    public class EmailSubscriberViewModal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}