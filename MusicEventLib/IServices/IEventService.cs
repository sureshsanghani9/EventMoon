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
        List<EventDataModal> GetTenLatestEvents(int MainCategoryId, string Keyword, DateTime? startdate, string Latitude, string Longitude);
        EventDataModal GetHeaderEvent(string Latitude, string Longitude);
        EventDataModal GetEventDetailsById(int EventID, string Latitude, string Longitude);
        EventPageDataModal GetEventListBySearch(int MainCategoryId, string Keyword, int PageNumber, int PageSize, string Sort, string Latitude, string Longitude);
        List<EventDataModal> GetAllNewEvents();
        void SetEmailSentFlag(List<int> Events);

    }
}
