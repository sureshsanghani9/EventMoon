using AutoMapper;
using MusicEventApp.ViewModels;
using MusicEventLib.DataModals;
using MusicEventLib.Helper;
using MusicEventLib.IServices;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MusicEventApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _EventService;

        public HomeController(IEventService EventService)
        {
            _EventService = EventService;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<MetaDataViewModel> MetaData = new List<MetaDataViewModel> {
                new MetaDataViewModel { name= "description", content="MusicEvent is great place to browse any music event anywhere in world. You can search all recent music events, programs, concert by well know celebrities, musicians, singers using our easy search options. you can also book for events, programs, concert easy steps." } ,
                new MetaDataViewModel { name = "keywords", content = "MusicEvent, Music, Program, Concert, Party, Singer, Celebrities, Musician, Singer, USA" } };
            ViewBag.MetaData = MetaData;

            List<EventDataModal> EventsData = _EventService.GetTenLatestEvents(0, "", null);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventsData);

            EventDataModal HeaderEvent = _EventService.GetHeaderEvent();
            ViewBag.HeaderEvent = Mapper.Map<EventDataModal, EventViewModal>(HeaderEvent);
            ViewBag.MainCategoryList = GlobalDataHelper.GetMainCategoryList();

            return View(Events);
        }

        [HttpPost]
        public ActionResult LatestEventList(int MainCategoryId, string Keyword, DateTime? startdate)
        {
            List<EventDataModal> EventsData = _EventService.GetTenLatestEvents(MainCategoryId, Keyword, startdate);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventsData);
            return PartialView("_LatestEventsListPartial", Events);
        }

        public ActionResult Event(int id)
        {
            return View();
        }
    }
}