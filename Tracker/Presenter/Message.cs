using System;
using System.Collections.Generic;

namespace Tracker
{
    public class Messages : List<Message>
    {
        int maxMessages = 100;

        public void Add(DateTime time, string message)
        {
            if (this.Count == maxMessages)
                this.RemoveAt(0);
            this.Add(new Message(time, message));
        }
    }

    public struct Message
    {
        DateTime time;
        string message;

        public Message(DateTime time, string message)
        {
            this.time = time;
            this.message = message;
        }

        public override string ToString()
        {
            return this.time.ToString() + " : " + this.message;
        }
    }
}
