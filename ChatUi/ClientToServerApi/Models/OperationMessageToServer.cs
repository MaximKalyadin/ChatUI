using ClientToServerApi.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerApi.Models
{
    public class OperationMessageToServer
    {
        public Operations Operation { get; set; }
        public object Data { get; set; }
    }
}
