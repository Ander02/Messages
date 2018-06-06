using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Message
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Message() { }

        public Message(string messageJson)
        {
            var deserializedObj = JsonConvert.DeserializeObject(messageJson, typeof(Message)) as Message;

            UserName = deserializedObj.UserName;
            Content = deserializedObj.Content;
            Date = deserializedObj.Date;
        }

        public override string ToString() => "(" + this.Date + ") " + this.UserName + ": " + this.Content;
    }
}
