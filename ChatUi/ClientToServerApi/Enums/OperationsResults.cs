using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerApi.Enums
{
    public enum OperationsResults
    {
        SuccessfullyRegistration,
        UnsuccessfullyRegistration,

        SuccessfullyAuthorization,
        UnsuccessfullyAuthorization,

        SuccessfullyProfileUpdate,
        UnsuccessfullyProfileUpdate,

        SuccessfullyChatCreate,
        UnsuccessfullyChatCreate,

        SuccessfullyChatUpdate,
        UnsuccessfullyChatUpdate,

        SuccessfullyChatRemove,
        UnsuccessfullyChatRemove,

        SuccessfullyAddUserToChat,
        UnsuccessfullyAddUserToChat,

        SuccessfullyRemoveUserFromChat,
        UnsuccessfullyRemoveUserFromChat
    }
}
