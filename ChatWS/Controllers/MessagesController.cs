using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;
namespace ChatWS.Controllers
{
    public class MessagesController : BaseController
    {
        [HttpPost]
        public Reply Get(MessagesRequest model )
        {

            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                if (!VerifyToken(model))
                {
                    oR.message = "Metodo no permitido";
                    return oR;
                }

                using (ChatDBEntities db = new ChatDBEntities())
                {


                    List<MessagesResponse> lst = (from d in db.message.ToList()
                                                  where d.idestate == 1 && d.idRoom == model.idRoom
                                                  orderby d.date_created descending
                                                  select new MessagesResponse
                                                  {
                                                      DateCreated = d.date_created,
                                                      Id = d.id,
                                                      idUser = d.idUser,
                                                      Message = d.text,
                                                      UserName = d.user.name,
                                                      TypeMessage =(
                                                                    new Func<int>(
                                                                           () =>
                                                                           {
                                                                               try
                                                                               {
                                                                                   if (d.idUser == oUserSession.Id)
                                                                                       return 1;
                                                                                   else
                                                                                       return 2;

                                                                               }
                                                                               catch
                                                                               {
                                                                                   return 2;
                                                                               }
                                                                           }
                                                                           )()
                                                                ) 
                                                  }).Take(20).ToList();

                    oR.data = lst.ToList();
                    oR.result = 1;

                }

            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un error";
            }

            return oR;
        }

    }
}
