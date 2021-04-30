using ChatWeb.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    public class LobbyController : BaseController
    {
        // GET: Lobby
        public ActionResult Index()
        {
            GetSession();
            List<ListRoomResponse> lst = new List<ListRoomResponse>();

            //oUserSession

            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken = oUserSession.AccessToken;
            Reply oReply = new Reply();
            RequestUtil oRequestUtil = new RequestUtil();

            oReply = oRequestUtil.Execute<SecurityRequest>(Constants.Url.Rooms, "post", oSecurityRequest);
            JavaScriptSerializer js = new JavaScriptSerializer();
             lst = js.Deserialize<List<ListRoomResponse>>(js.Serialize(oReply.data));

            return View(lst);
        }
    }
}