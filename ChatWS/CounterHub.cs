using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using ChatWS.Models;
namespace ChatWS
{
    public class CounterHub : Hub
    {
  

        public override Task OnConnected()
        {
            Clients.All.enterUser();
            return base.OnConnected();
        }

        public void AddGroup(int idRoom)
        {

            Groups.Add(Context.ConnectionId, idRoom.ToString());

        }

        public void Send(int idRoom,string userName, int idUser, string message, string AccessToken)
        {
            if (VerifyToken(AccessToken))
            {

                string fecha ="";
                using (ChatDBEntities db = new ChatDBEntities())
                {
                    //cre el nuevo message en la DB
                    var oMessage = new message();
                    oMessage.idestate = 1;
                    oMessage.idRoom = idRoom;
                    oMessage.idUser = idUser;
                    oMessage.text = message;
                    oMessage.date_created =DateTime.Now;

                    db.message.Add(oMessage);
                    db.SaveChanges();

                    fecha = oMessage.date_created.ToString();
                }
                // aqui se manda a todos los grupos a todo el chat
                // Clients.All.sendChat(userName, message, fecha, idUser);

                //aqui se manda solo a este grupo o sala de chat
                Clients.Group(idRoom.ToString()).sendChat(userName, message, fecha, idUser);

            }
        }


        protected bool VerifyToken(string AccessToken)
        {
            using (ChatDBEntities db = new ChatDBEntities())
            {
                var oUser = db.user.Where(d => d.access_Token ==  AccessToken).FirstOrDefault();

                if (oUser != null)
                {
                    return true;

                }


            }

            return false;

        }
    }
}