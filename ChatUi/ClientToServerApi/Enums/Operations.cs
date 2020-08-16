using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerApi.Enums
{
    public enum Operations
    {
        Authorization,
        Registration,
        UpdateProfile,
        SendMessage,

        CreateChat,
        DeleteChat,
        AddUserToChat,
        RemoveUserToChat
    }
}
