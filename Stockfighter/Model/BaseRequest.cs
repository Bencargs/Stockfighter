using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stockfighter.Config;

namespace Stockfighter.Model
{
    public abstract class BaseRequest : IRequest
    {
        [JsonIgnore]
        public string Url { get; set; }

        [JsonIgnore]
        public IReply Reply { get; set; }

        protected BaseRequest()
        {
        }

        protected BaseRequest(string url)
            : this()
        {
            Url = url;
        }

        public abstract bool Execute();

        /// <summary>
        /// Get request
        /// </summary>
        /// <typeparam name="TReply"></typeparam>
        /// <returns></returns>
        protected async virtual Task<bool> Execute<TReply>()
            where TReply : IReply
        {
            string json = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Headers.Add(Utils.AuthorisationKey, Utils.ApiKey);

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                    Reply = JsonConvert.DeserializeObject<TReply>(json);
                    Reply.Json = json;
                    return Reply.Validate();
                }
            }
            catch (Exception ex)
            {
                Reply = new BaseReply(false, ex.Message) {Json = json};
            }
            return Reply.Success;
        }

        /// <summary>
        /// Post request
        /// </summary>
        /// <typeparam name="TReply"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual bool Execute<TReply, TRequest>(TRequest args)
            where TReply : IReply
            where TRequest : IRequest
        {
            string json = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers.Add(Utils.AuthorisationKey, Utils.ApiKey);

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    json = JsonConvert.SerializeObject(args);
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                    Reply = JsonConvert.DeserializeObject<TReply>(json);
                    Reply.Json = json;
                    return Reply.Validate();
                }
            }
            catch (Exception ex)
            {
                Reply = new BaseReply(false, ex.Message) {Json = json};
            }
            return Reply.Success;
        }
    }
}