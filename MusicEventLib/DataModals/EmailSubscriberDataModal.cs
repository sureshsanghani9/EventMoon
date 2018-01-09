using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.DataModals
{
    public class EmailSubscriberDataModal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
