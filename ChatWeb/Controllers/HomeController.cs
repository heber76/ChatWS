
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;
using ChatWeb.Business;
using ChatWeb.Models.ViewModels;
using System.Web.Script.Serialization;
//using ChatWeb.Models.ViewModels;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
       

        [HttpGet]
        public ActionResult Login()
        {
            UserAccessViewModel model = new UserAccessViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserAccessViewModel model)
        {

            if (!ModelState.IsValid)
            {
                //REGRESO as LA  VISTA 
                return View(model);
            }

            Reply oReply = new Reply();
            AccessRequest oAR = new AccessRequest();
            oAR.Email = model.Email;
            oAR.Password = UtilitiesChat.Tools.Encrypt.GetSHA256(model.Password);



            RequestUtil oRequestUtil = new RequestUtil();

            oReply = oRequestUtil.Execute<AccessRequest>(Constants.Url.ACESS, "post", oAR);
            JavaScriptSerializer js = new JavaScriptSerializer();
            UtilitiesChat.Models.WS.UserResponse oUserResponse = js.Deserialize<UtilitiesChat.Models.WS.UserResponse>(js.Serialize(oReply.data));
            if (oReply.result == 1)
            {
                Session["User"] = oUserResponse;
                return RedirectToAction("Index", "Lobby");
            }
            
             //mandar error
                ViewBag.error = "Datos Incorrectos";
                return View(model);
            

        }

        [HttpGet]
        public ActionResult Register()
        {
            ChatWeb.Models.ViewModels.RegisterViewModel model = new Models.ViewModels.RegisterViewModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Register(Models.ViewModels.RegisterViewModel model)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    //REGRESO as LA  VISTA 
                    return View(model);
                }

                UtilitiesChat.Models.WS.Reply oReply = new Reply();
                Models.Request.User oUser = new Models.Request.User();
                oUser.Name = model.Name;
                oUser.City = model.City;
                oUser.Email = model.Email;
                oUser.Password = model.Password;

                RequestUtil oRequestUtil = new RequestUtil();
                oReply =   oRequestUtil.Execute<Models.Request.User>(Constants.Url.REGISTER, "post", oUser);

                return RedirectToAction("Index","Home");

            }
            catch (Exception ex )
            {

                throw;
            }
            
        }

    }
}