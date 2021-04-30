using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UtilitiesChat.Models.WS;

namespace UtilitiesChat
{
    public class RequestUtil
    {


        public Reply oReply { get; set; }
        //constructor
        public RequestUtil()
        {

            oReply = new Reply();

        }


        public Reply Execute<T>(string url, string method, T objectRequest)
        {
            oReply.result = 0;
            string result = "";

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(objectRequest);


                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8";
                request.Timeout = 60000;

                using (var oStreamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    oStreamWriter.Write(json);
                    oStreamWriter.Flush();
                }


                var oHttpResponse = (HttpWebResponse)request.GetResponse();

                using (var oStreamReader = new StreamReader(oHttpResponse.GetResponseStream()))
                {

                    result = oStreamReader.ReadToEnd();

                }

                oReply = js.Deserialize<Reply>(result);




            }
            catch (TimeoutException ex)
            {
                oReply.message = "Servidor sin respuesta";
            }
            catch (Exception ex)
            {


                oReply.message = "Ocurrio un error";
            }
            return oReply;

        }


    }
}
