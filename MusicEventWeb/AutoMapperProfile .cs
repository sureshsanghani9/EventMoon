using AutoMapper;
using MusicEventApp.ViewModels;
using MusicEventLib.DataModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicEventApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MusicEventLib.AutoMapperProfile LibProfile = new MusicEventLib.AutoMapperProfile();
            CreateMap<EventDataModal, EventViewModal>();
        }
    }
}