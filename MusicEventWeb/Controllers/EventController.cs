using AutoMapper;
using MusicEventApp.ViewModels;
using MusicEventLib.DataModals;
using MusicEventLib.Helper;
using MusicEventLib.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicEventApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _EventService;

        public EventController(IEventService EventService)
        {
            _EventService = EventService;
        }
        // GET: Event
        public ActionResult Index()
        {
            List<MetaDataViewModel> MetaData = new List<MetaDataViewModel> {
                new MetaDataViewModel { name= "description", content="List of MusicEvents. MusicEvent is great place to browse any music event anywhere in world. You can search all recent music events, programs, concert by well know celebrities, musicians, singers using our easy search options. you can also book for events, programs, concert easy steps." } ,
                new MetaDataViewModel { name = "keywords", content = "MusicEvent, Music, Program, Concert, Party, Singer, Celebrities, Musician, Singer, USA" } };
            ViewBag.MetaData = MetaData;

            ViewBag.MainCategoryList = GlobalDataHelper.GetMainCategoryList();

            return View();
        }

        [HttpPost]
        public ActionResult EventList(int MainCategoryId, string Keyword, int PageNumber, int PageSize, string Sort)
        {
            string Latitude = Session["ULatitude"] != null ? Session["ULatitude"].ToString() : "0";
            string Longitude = Session["ULongitude"] != null ? Session["ULongitude"].ToString() : "0";

            EventPageDataModal EventPage = _EventService.GetEventListBySearch(MainCategoryId, Keyword, PageNumber, PageSize, Sort, Latitude, Longitude);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventPage.Events);
            ViewBag.TotalRecords = EventPage.TotalRecords.FirstOrDefault().TotalRecords;

            return PartialView("_EventListPartial", Events);
        }

        public ActionResult Details(int id)
        {
            string Latitude = Session["ULatitude"] != null ? Session["ULatitude"].ToString() : "0";
            string Longitude = Session["ULongitude"] != null ? Session["ULongitude"].ToString() : "0";

            EventDataModal evtdata = _EventService.GetEventDetailsById(id, Latitude, Longitude);
            EventViewModal evt = Mapper.Map<EventDataModal, EventViewModal>(evtdata);

            List<MetaDataViewModel> MetaData = new List<MetaDataViewModel> {
                new MetaDataViewModel { name= "description", content="Music Event "+ evt.EventName +" at " + evt.location + " on " + evt.StartDate.Value.ToString() + ". Book ticket now!" } ,
                new MetaDataViewModel { name = "keywords", content = "MusicEvent, Music, Program, Concert, Party, Singer, Celebrities, Musician, Singer, USA, "+ evt.EventName +", " + evt.location + "" } };
            ViewBag.MetaData = MetaData;

            return View(evt);
        }
    }
}