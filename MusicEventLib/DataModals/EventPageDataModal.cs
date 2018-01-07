using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.DataModals
{
    public class EventPageDataModal
    {
        public List<EventDataModal> Events { get; set; }
        public List<TotalRecordsDataModal> TotalRecords { get; set; }
    }
}
