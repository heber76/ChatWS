using ChatWS.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UtilitiesChat.Models.WS;


namespace ChatWS.Controllers
{
    public class AccessController : ApiController
    {
        [HttpPost]
        public Reply Login([FromBody]AccessRequest model)
        {

            Reply oReply = new Reply();
            oReply.result = 0;

            using (ChatDBEntities db = new ChatDBEntities())
            {
                var oUSer = (from d in db.user
                            where d.email == model.Email && d.password == model.Password
                            select d).FirstOrDefault();

                if (oUSer != null)
                {
                    string AccessToken = Guid.NewGuid().ToString();

                    oUSer.access_Token = AccessToken;

                    db.Entry(oUSer).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    UserResponse oUserResponse = new UserResponse();
                    oUserResponse.AccessToken = AccessToken;
                    oUserResponse.City = oUSer.city;
                    oUserResponse.Name = oUSer.name;
                    oUserResponse.Id = oUSer.id;

                    oReply.result = 1;
                    oReply.data =oUserResponse;


                }
                else
                {
                    oReply.result = 0;
                    oReply.message = "Datos incorrectos";
                }
            }

            return oReply;

        }

    }
}
