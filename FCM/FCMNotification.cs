using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace FCM
{
    public class FCMNotification
    {
        public FCMNotification()
        {}       
        public bool Successful
        {
            get;
            set;
        }
        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }

        public FCMNotification SendTopicNotification(string _title, string _message, string _topic, string AuthKey, string SenderID)
        {
            FCMNotification result = new FCMNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;  
                string serverKey = AuthKey;
                string senderId = SenderID;
                var requestUri = "https://fcm.googleapis.com/fcm/send";
                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                webRequest.ContentType = "application/json";
                var data = new
                {
                    to = "/topics/" + _topic, // this is for topic  
                    priority = "high",
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        show_in_foreground = "True",
                        icon = "myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }

        public FCMNotification SendDeviceNotification(string _title, string _message, string _topic, string deviceId, string AuthKey, string SenderID)
        {
            FCMNotification result = new FCMNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;  
                string serverKey = AuthKey;
                string senderId = SenderID;
                var requestUri = "https://fcm.googleapis.com/fcm/send";
                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                webRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId, // this if you want to test for single device  
                    priority = "high",
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        show_in_foreground = "True",
                        icon = "myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}
