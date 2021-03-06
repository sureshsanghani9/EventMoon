﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MusicEventLib.DataModals;
using MusicEventDataAccess;

namespace MusicEventLib
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetTenLatestEventList_Result, EventDataModal>();
            CreateMap<GetEventDetailsById_Result, EventDataModal>();
            CreateMap<GetHeaderEvent_Result, EventDataModal>();
            CreateMap<GetAllNewEvents_Result, EventDataModal>();
            CreateMap<MainCategory, MainCategoryDataModel>();
            CreateMap<Category, CategoryDataModel>();
            CreateMap<EmailSubscriber, EmailSubscriberDataModal>();
            CreateMap<NewEvent, EventDataModal>();
        }
    }
}
