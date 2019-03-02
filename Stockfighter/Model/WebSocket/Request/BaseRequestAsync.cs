using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stockfighter.Config;

namespace Stockfighter.Model.WebSocket.Request
{
    public class BaseRequestAsync<TReply>
        where TReply : IReply
    {
        [JsonIgnore]
        public string Url { get; protected set; }

        [JsonIgnore]
        public EventHandler<TReply> ExecuteComplete { get; set; }

        [JsonIgnore]
        public EventHandler<ErrorEventArgs> Error { get; set; }

        private readonly TimeSpan _timeout;

        public BaseRequestAsync()
        {
            _timeout = new TimeSpan(0, 0, 0, 10);
        }

        public virtual void Execute()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Headers.Add(Utils.AuthorisationKey, Utils.ApiKey);

                request.BeginGetResponse(ExecuteResponse, request);
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        public Task<bool> Wait()
        {
            var tcs = new TaskCompletionSource<bool>();

            ExecuteComplete += (sender, reply) => tcs.SetResult(true);
            Error += (sender, args) => tcs.SetResult(false);
            Execute();

            return tcs.Task;
        }

        protected void Execute<TRequest>(TRequest args)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers.Add(Utils.AuthorisationKey, Utils.ApiKey);

                request.BeginGetRequestStream(GetRequestStreamComplete<TRequest>,
                    new Tuple<TRequest, HttpWebRequest>(args, request));
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        private void GetRequestStreamComplete<TRequest>(IAsyncResult result)
        {
            try
            {
                Tuple<TRequest, HttpWebRequest> tuple = (Tuple<TRequest, HttpWebRequest>) result.AsyncState;
                HttpWebRequest request = tuple.Item2;
                TRequest args = tuple.Item1;

                if (result.AsyncWaitHandle.WaitOne(_timeout))
                {
                    using (StreamWriter writer = new StreamWriter(request.EndGetRequestStream(result)))
                    {
                        string json = JsonConvert.SerializeObject(args);
                        writer.Write(json);
                        writer.Flush();
                        writer.Close();
                    }

                    request.BeginGetResponse(ExecuteResponse, request);
                }
                else
                {
                    OnError("Timeout expired waiting for Async Request Stream result ");
                }
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        private void ExecuteResponse(IAsyncResult result)
        {
            try
            {
                if (result.AsyncWaitHandle.WaitOne(_timeout))
                {
                    HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                    WebResponse response = request.EndGetResponse(result);

                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        TReply reply = JsonConvert.DeserializeObject<TReply>(json);
                        reply.Json = json;
                        if (reply.Validate())
                        {
                            OnExecuteComplete(reply);
                        }
                    }
                }
                else
                {
                    OnError("Timeout expired waiting for Response Stream Async result");
                }
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        private void OnExecuteComplete(TReply e)
        {
            EventHandler<TReply> hander = ExecuteComplete;
            if (hander != null)
            {
                hander(this, e);
            }
        }

        private void OnError(string error)
        {
            EventHandler<ErrorEventArgs> handler = Error;
            if (Error != null)
            {
                handler(this, new ErrorEventArgs(new Exception(error)));
            }
        }
    }
}
