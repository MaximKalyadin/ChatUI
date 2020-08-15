using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatUi
{
    public class ViewModel
    {

        public List<ChatListItems> ChatListItems
        {

            get
            {
                return new List<ChatListItems>
                {
                new ChatListItems(){ ContactProfilePic="/assets/profile1.jpg", ContactName="Пример", LastMessageTime="10:30 PM", Availability="Online", IsRead=true, Message="Как дела?", NewMsgCount="1", IsOnline=true},
                new ChatListItems() { ContactName = "Калядин Максим", LastMessageTime = "14:45 pm", Availability = "Offline", Message = "Пока", ContactProfilePic="/assets/profile1.jpg" },
                new ChatListItems() {IsChatSelected=true, ContactName = "Лагин Дмитрий", LastMessageTime = "06:18 am", Availability = "Offline", Message = "Как нибудь увидимся", IsRead = false, ContactProfilePic="/assets/profile1.jpg"},
                new ChatListItems() {IsChatSelected=true, ContactName = "Лагин Дмитрий", LastMessageTime = "06:18 am", Availability = "Offline", Message = "Как нибудь увидимся", IsRead = false, ContactProfilePic="/assets/profile1.jpg"},
                new ChatListItems() {IsChatSelected=true, ContactName = "Лагин Дмитрий", LastMessageTime = "06:18 am", Availability = "Offline", Message = "Как нибудь увидимся", IsRead = false, ContactProfilePic="/assets/profile1.jpg"}
                };
            }
        }

        public List<ConversationMessages> Messages
        {
            get
            {
                return new List<ConversationMessages>
                {
                    new ConversationMessages() { Message="Привет",MessageStatus ="Received", TimeStamp="Yesterday 14:26 PM" },
                    new ConversationMessages() { Message="Привет как дела?", MessageStatus="Sent", TimeStamp="Yesterday 14:38 PM"},
                    new ConversationMessages() { Message="Что делаешь?", MessageStatus="Sent", TimeStamp="Today 06:18 AM"}
                };
            }
        }

    }

    public class ChatListItems
    {
        public bool IsChatSelected { get; set; }

        public bool IsOnline { get; set; }

        public string ContactProfilePic { get; set; }

        public string ContactName { get; set; }

        public string LastMessageTime { get; set; }

        public string Availability { get; set; }

        public bool IsRead { get; set; }

        public string Message { get; set; }

        public string NewMsgCount { get; set; }
    }

    public class ConversationMessages
    {

        public string MessageStatus { get; set; }

        public string TimeStamp { get; set; }

        public string Message { get; set; }
    }
}
