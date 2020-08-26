using ClientToServerApi.Enums;
using ClientToServerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientToServerApi
{
    public class ClientServerService
    {
        private readonly TcpClient tcpClient_;
        private readonly NetworkStream networkStream_;
        private readonly DataManager dataManager_;

        private static Thread receiveThread_;
        private static ClientServerService clientServerService_;
        private static string ServerIp { set; get; }
        private static string ServerPort { set; get; }

        private ClientServerService()
        {
            tcpClient_ = new TcpClient();
            dataManager_ = new DataManager();
            networkStream_ = Connect(ServerIp, ServerPort);
        }

        #region DataManagerRegion
        public void AddListener(ListenerType key, Handler listener) => dataManager_.AddListener(key, listener);
        public void RemoveListener(ListenerType key) => dataManager_.RemoveListener(key);
        #endregion

        public static void SetApiConfig(string serverIp, string serverPort)
        {
            ServerIp = serverIp;
            ServerPort = serverPort;
        }

        public static ClientServerService GetInstanse()
        {
            if (clientServerService_ == null)
                clientServerService_ = new ClientServerService();
            return clientServerService_;
        }

        ~ClientServerService()
        {
            tcpClient_.Close();
            if(networkStream_ != null)
                networkStream_.Close();
        }

        public static void ShutdownReceiving() => receiveThread_.Abort();

        private NetworkStream Connect(string ServerIp, string ServerPort)
        {
            tcpClient_.ConnectAsync(IPAddress.Parse(ServerIp), Convert.ToInt32(ServerPort)).Wait();
            if (tcpClient_.Connected)
            {
                receiveThread_ = new Thread(ReceiveData);
                receiveThread_.Start();
                return tcpClient_.GetStream();
            }
            throw new Exception("Не удалось установить соединение с сервером!");
        }

        public async void SendAsync(OperationMessageToServer messageToServer)
        {
            try
            {
                byte[] binary_data = Encoding.UTF8.GetBytes(messageToServer.ToString());
                await networkStream_.WriteAsync(binary_data, 0, binary_data.Length).ConfigureAwait(false);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void ReceiveData()
        {
            try
            {
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();
                while (true)
                {
                    do
                    {
                        int countBytes = networkStream_.Read(buffer, 0, 256);
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, countBytes));
                    } while (networkStream_.DataAvailable);

                    var operationInfo = stringBuilder.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    var listenerAndResult = operationInfo[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var infoAndData = operationInfo[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    Debug.WriteLine("Получено сообщение - " + stringBuilder.ToString());

                    dataManager_.HandleData((ListenerType)Enum.Parse(typeof(ListenerType), listenerAndResult[0]), new OperationResultInfo()
                    {
                       OperationResult = (OperationsResults)Enum.Parse(typeof(OperationsResults), listenerAndResult[1]),
                       Info = infoAndData[0],
                       Data = infoAndData.Length == 2 ? infoAndData[1] : string.Empty
                    });
                    stringBuilder.Clear();
                    buffer = new byte[256];
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
