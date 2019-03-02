using System;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Stockfighter.Model.WebSocket
{
    public abstract class BaseTicker
    {
        public string Url { get; protected set; }
        public string Account { get; protected set; }
        protected EventHandler ConnectionOpened;
        protected EventHandler ConnectionClosed;
        protected EventHandler<MessageReceivedEventArgs> MessageReceived;
        protected EventHandler<ErrorEventArgs> Error;
        private WebSocket4Net.WebSocket _socket;

        protected BaseTicker(string account)
        {
            Account = account;
        }

        public void Execute()
        {
            _socket = new WebSocket4Net.WebSocket(Url);
            _socket.Opened += OnConnected;
            _socket.Error += OnError;
            _socket.Closed += OnClosed;
            _socket.MessageReceived += OnMessage;
            _socket.Open();
        }

        private void OnConnected(object sender, EventArgs e)
        {
            EventHandler handler = ConnectionOpened;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            EventHandler handler = ConnectionClosed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            EventHandler<MessageReceivedEventArgs> handler = MessageReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error occured in WebSockets: " + e.Exception);
            EventHandler<ErrorEventArgs> handler = Error;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
