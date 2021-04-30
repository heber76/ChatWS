using ChatWeb.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilitiesChat;
//using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    public class ChatController : BaseController
    {
        // GET: Chat
        public ActionResult Messages(int id)
        {
            GetSession();
            List<MessagesResponse> lst = new List<MessagesResponse>();

            MessagesRequest oMessagesRequest = new MessagesRequest();
            oMessagesRequest.AccessToken = oUserSession.AccessToken;
            oMessagesRequest.idRoom = id;

            Reply oReply = new Reply();
                       
            RequestUtil oRequestUtil = new RequestUtil();

            oReply = oRequestUtil.Execute<SecurityRequest>(Constants.Url.Messages,"post",oMessagesRequest);
            JavaScriptSerializer js = new JavaScriptSerializer();
            lst = js.Deserialize<List<MessagesResponse>>(js.Serialize(oReply.data));
            lst = lst.OrderBy(d => d.DateCreated).ToList();
            ViewBag.IdRoom = id;
            return View(lst);
        }
    }
}