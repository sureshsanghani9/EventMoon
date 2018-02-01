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
            string Latitude = Session["ULatitude"] != null ? Session["ULatitude"].ToString() : "0";
            string Longitude = Session["ULongitude"] != null ? Session["ULongitude"].ToString() : "0";

            List<MetaDataViewModel> MetaData = new List<MetaDataViewModel> {
                new MetaDataViewModel { name= "description", content="MusicEvent is great place to browse any music event anywhere in world. You can search all recent music events, programs, concert by well know celebrities, musicians, singers using our easy search options. you can also book for events, programs, concert easy steps." } ,
                new MetaDataViewModel { name = "keywords", content = "MusicEvent, Music, Program, Concert, Party, Singer, Celebrities, Musician, Singer, USA" } };
            ViewBag.MetaData = MetaData;

            List<EventDataModal> EventsData = _EventService.GetTenLatestEvents(0, "", null, Latitude, Longitude);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventsData);

            EventDataModal HeaderEvent = _EventService.GetHeaderEvent(Latitude, Longitude);
            ViewBag.HeaderEvent = Mapper.Map<EventDataModal, EventViewModal>(HeaderEvent);
            ViewBag.MainCategoryList = GlobalDataHelper.GetMainCategoryList();

            return View(Events);
        }

        [HttpPost]
        public ActionResult LatestEventList(int MainCategoryId, string Keyword, DateTime? startdate)
        {
            string Latitude = Session["ULatitude"] != null ? Session["ULatitude"].ToString() : "0";
            string Longitude = Session["ULongitude"] != null ? Session["ULongitude"].ToString() : "0";

            List<EventDataModal> EventsData = _EventService.GetTenLatestEvents(MainCategoryId, Keyword, startdate, Latitude, Longitude);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventsData);
            return PartialView("_LatestEventsListPartial", Events);
        }

        [HttpPost]
        public string SetUserLocation(string Latitude, string Longitude)
        {
            Session["ULatitude"] = Latitude;
            Session["ULongitude"] = Longitude;

            return "success";
        }

        public ActionResult Event(int id)
        {
            return View();
        }
    }
}