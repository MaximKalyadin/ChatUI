using ClientToServerApi.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientToServerApi.Models
{
    public class OperationResultInfo
    {
        public OperationsResults OperationResult { get; set; }
        public string Info { set; get; }
        public string Data { get; set; }
    }
}
