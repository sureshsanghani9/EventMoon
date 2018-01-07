﻿using MusicEventLib.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEventLib.DataModals;
using MusicEventDataAccess;
using AutoMapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace MusicEventLib.Services
{
    public class EventService : IEventService
    {
        public EventDataModal GetEventDetailsById(int EventID)
        {
            using (var db = new MusicEventEntities())
            {
                var evt = db.GetEventDetailsById(EventID).ToList().FirstOrDefault();
                return Mapper.Map<GetEventDetailsById_Result, EventDataModal>(evt);
            }
        }

        public EventPageDataModal GetEventListBySearch(int MainCategoryId, string Keyword, int PageNumber, int PageSize, string Sort)
        {
            using (var db = new MusicEventEntities())
            {
                db.Database.Connection.Open();
                EventPageDataModal EventPage = new EventPageDataModal();
                EventPage.Events = new List<EventDataModal>();
                EventPage.TotalRecords = new List<TotalRecordsDataModal>();

                var command = db.Database.Connection.CreateCommand();
                command.CommandText = "[dbo].[GetEventListBySearch]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@MainCategoryId", MainCategoryId));
                command.Parameters.Add(new SqlParameter("@Keyword", Keyword));
                command.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                command.Parameters.Add(new SqlParameter("@Sort", Sort));

                var reader = command.ExecuteReader();
                List<EventDataModal> _events = ((IObjectContextAdapter)db).ObjectContext.Translate<EventDataModal>(reader).ToList();
                reader.NextResult();
                List<TotalRecordsDataModal> _totalrecords = ((IObjectContextAdapter)db).ObjectContext.Translate<TotalRecordsDataModal>(reader).ToList();

                EventPage.Events.AddRange(_events);
                EventPage.TotalRecords.AddRange(_totalrecords);

                db.Database.Connection.Close();
                return EventPage;
            }
        }

        public EventDataModal GetHeaderEvent()
        {
            using (var db = new MusicEventEntities())
            {
                var evt = db.GetHeaderEvent().ToList().FirstOrDefault();
                return Mapper.Map<GetHeaderEvent_Result, EventDataModal>(evt);
            }
        }

        public List<EventDataModal> GetTenLatestEvents(int MainCategoryId, string Keyword, DateTime? startdate)
        {

            using (var db = new MusicEventEntities())
            {
                var events = db.GetTenLatestEventList(MainCategoryId, Keyword, startdate).ToList();
                return Mapper.Map<List<GetTenLatestEventList_Result>, List<EventDataModal>>(events);
            }
        }
    }
}