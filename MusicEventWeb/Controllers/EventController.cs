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
            ViewBag.MainCategoryList = GlobalDataHelper.GetMainCategoryList();

            return View();
        }

        [HttpPost]
        public ActionResult EventList(int MainCategoryId, string Keyword, int PageNumber, int PageSize, string Sort)
        {
            EventPageDataModal EventPage = _EventService.GetEventListBySearch(MainCategoryId, Keyword, PageNumber, PageSize, Sort);
            List<EventViewModal> Events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(EventPage.Events);
            ViewBag.TotalRecords = EventPage.TotalRecords.FirstOrDefault().TotalRecords;

            return PartialView("_EventListPartial", Events);
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}