using AutoMapper;
using MusicEventDataAccess;
using MusicEventLib.DataModals;
using MusicEventLib.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.Services
{
    public class SubscribeService : ISubscribeService
    {
        public bool IsSubscribedEmail(string Email)
        {
            using (var db = new MusicEventEntities())
            {
                return db.EmailSubscribers.Any(e => e.Email == Email);
            }
        }

        public bool SubscribeEmail(string Email)
        {
            using (var db = new MusicEventEntities())
            {
                db.EmailSubscribers.Add(new EmailSubscriber { Email = Email, CreatedOn = DateTime.Now });
                db.SaveChanges();
                return true;
            }
        }

        public List<EmailSubscriberDataModal> GetAllEmailSubscriber()
        {
            using (var db = new MusicEventEntities())
            {
                var subscribers = db.EmailSubscribers.ToList();
                return Mapper.Map<List<EmailSubscriber>, List<EmailSubscriberDataModal>>(subscribers);
            }
        }
    }
}
