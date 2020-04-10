using System;
using System.Collections.Generic;
using System.Text;
using TLSharp;
using TLSchema;

namespace TaskManager
{
    class Telegram
    {
        public Telegram()
        {
        }

        public static async void A()
        {
            var client = new TelegramClient(978753, "1129d1ce464fd46a499ec9744df3a563");
            //send message
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = 913702988 }, "OUR_MESSAGE");
        }
    }
}
