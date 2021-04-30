using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChatWS.Models;
using ChatWS.Models.Request;
using ChatWS.Models.ViewModels;
using UtilitiesChat.Models.WS;

namespace ChatWS.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public Reply Get()
        {
            Reply oReply = new Reply();

            using (ChatDBEntities db = new ChatDBEntities())
            {

                List<UserViewModel> lst = (from d in db.user
                                           select new UserViewModel
                                           {
                                               Name = d.name,
                                               City = d.city
                                           }).ToList();

                oReply.result = 1;
                oReply.message = "";
                oReply.data = lst;

            }

            return oReply;
        }


       



        [HttpPost]
        public Reply Register([FromBody]Models.Request.User model)
        {

            Reply oReply = new Reply();
            oReply.result = 0;
            try
            {

                Models.user oUser = new Models.user();
                oUser.city = model.City;
                oUser.idState = 1;
                oUser.date_created = DateTime.Now;
                oUser.email = model.Email;
                oUser.password = model.Password;
                oUser.name = model.Name;
                using (ChatDBEntities db = new ChatDBEntities())
                {
                    db.user.Add(oUser);
                    db.SaveChanges();

                    oReply.result = 1;
                    oReply.message = "Se registro el usuario";


                    List<UserViewModel> lst = (from d in db.user
                                               select new UserViewModel
                                               {
                                                   Name = d.name,
                                                   City = d.city
                                               }).ToList();

                     oReply.data = lst;
                }

            }
            catch (Exception ex)
            {

                oReply.message = ("Error al registar un usuario, intenta de nuevo mas tarde");
                // crear un log en base de datos
            }
            return oReply;
        }


        ////public Object Lista(ChatDBEntities db)
        ////{
        ////    List<UserViewModel> lst = (from d in db.user
        ////                               select new UserViewModel
        ////                               {
        ////                                   Name = d.name,
        ////                                   City = d.city
        ////                               }).ToList();

        ////    return lst;
        ////}
    }
}
