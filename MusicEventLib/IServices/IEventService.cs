using MusicEventDataAccess;
using MusicEventLib.DataModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.IServices
{
    public interface IEventService
    {
        List<EventDataModal> GetTenLatestEvents(int MainCategoryId, string Keyword, DateTime? startdate);
        EventDataModal GetHeaderEvent();
        EventDataModal GetEventDetailsById(int EventID);
        EventPageDataModal GetEventListBySearch(int MainCategoryId, string Keyword, int PageNumber, int PageSize, string Sort);

    }
}
