using AutoMapper;
using MusicEventApp.ViewModels;
using MusicEventLib.DataModals;
using MusicEventLib.Helper;
using MusicEventLib.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicEventApp.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly IEventService _EventService;
        private readonly ISubscribeService _SubscribeService;

        public SubscribeController(IEventService EventService, ISubscribeService SubscribeService)
        {
            _EventService = EventService;
            _SubscribeService = SubscribeService;
        }

        [HttpGet]
        public JsonResult SubscribeEmail(string Email)
        {
            bool IsExist = _SubscribeService.IsSubscribedEmail(Email);
            if (!IsExist)
            {
                bool IsSuccess = _SubscribeService.SubscribeEmail(Email);
                if (IsSuccess)
                {
                    return Json(new { Code = 1, Message = "Your email has been subscribed successfully!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Code = 0, Message = "Something wrong! Try after some time!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Code = 0, Message = "You have already subscribed this email!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendNewEventEmailToSubscribers()
        {
            List<EventDataModal> eventdata = _EventService.GetAllNewEvents();
            List<EventViewModal> events = Mapper.Map<List<EventDataModal>, List<EventViewModal>>(eventdata);
            List<EmailSubscriberDataModal> subscribers = _SubscribeService.GetAllEmailSubscriber();

            string MailUserName = ConfigurationManager.AppSettings["MailUserName"] != null ? ConfigurationManager.AppSettings["MailUserName"].ToString() : "";
            string NewEventMailSubject = ConfigurationManager.AppSettings["NewEventMailSubject"] != null ? ConfigurationManager.AppSettings["NewEventMailSubject"].ToString() : "Checkout our new Event!";
            List<int> eventids = new List<int>();
            foreach (EventViewModal evt in events)
            {
                bool isError = false;
                String emailhtml = MvcHelpers.RenderViewToString(this.ControllerContext, "~/Views/Subscribe/NewEventEmail.cshtml", evt);
                foreach (EmailSubscriberDataModal sb in subscribers)
                {
                    isError = EmailHelper.SendEmail(MailUserName, sb.Email, NewEventMailSubject, emailhtml, null, "", true);
                }
                if (isError)
                {
                    eventids.Add(evt.EventID);
                }
            }
            _EventService.SetEmailSentFlag(eventids);

            return Json(new { Code = 1, Message = "Success!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewEventEmail()
        {
            ViewBag.MySiteURL = ConfigurationManager.AppSettings["MySiteURL"] != null ? ConfigurationManager.AppSettings["MySiteURL"].ToString() : "";
            EventDataModal HeaderEvent = _EventService.GetHeaderEvent();
            return View(Mapper.Map<EventDataModal, EventViewModal>(HeaderEvent));
        }

    }
}