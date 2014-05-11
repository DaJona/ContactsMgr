using MailChimp;
using MailChimp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class EMail
    {
        public static void sendAccountActivationEmail()
        {
            MandrillApi api = new MandrillApi("3o-nF0RHNIA7k1poegC-_g");
            Mandrill.Messages.Message message = new Mandrill.Messages.Message();
            Mandrill.Messages.Recipient[] listAddresses = new Mandrill.Messages.Recipient[1];

            listAddresses[0] = new Mandrill.Messages.Recipient("jonigu@hotmail.com", "Fulanito");

            message.FromEmail = "jonathan.eljona@gmail.com";
            message.FromName = "Contact Manager";
            message.Html = "<h1>Testing</h1><p>Probando</p>";
            message.Subject = "Testing";
            message.TrackClicks = true;
            message.Important = true;
            message.To = listAddresses;

            MVList<Mandrill.Messages.SendResult> result = api.Send(message);
            string oeoe = "";
        }
    }
}
