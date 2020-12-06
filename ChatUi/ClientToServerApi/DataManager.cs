using ClientToServerApi.Enums;
using ClientToServerApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClientToServerApi
{
    public delegate void Handler(OperationResultInfo data);
    public class DataManager
    {
        private Dictionary<ListenerType, Handler> listenerDict_;

        public DataManager() => listenerDict_ = new Dictionary<ListenerType, Handler>();
        public void AddListener(ListenerType key, Handler listener) => listenerDict_.Add(key, listener);
        public void RemoveListener(ListenerType key) => listenerDict_.Remove(key);
        public void HandleData(ListenerType key, OperationResultInfo data)
        {
            if (listenerDict_.ContainsKey(key))
            {
                //parse data (string) to object;
                
                listenerDict_[key].Invoke(data);
            }
            else
                throw new Exception("This key isn't contains in listener dictionary");
        }
    }
}
