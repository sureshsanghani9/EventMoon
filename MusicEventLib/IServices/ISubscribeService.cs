using MusicEventLib.DataModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEventLib.IServices
{
    public interface ISubscribeService
    {
        bool IsSubscribedEmail(string Email);
        bool SubscribeEmail(string Email);
        List<EmailSubscriberDataModal> GetAllEmailSubscriber();
    }
}
