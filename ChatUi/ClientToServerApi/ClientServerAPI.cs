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
    public class ClientServerAPI
    {
        private readonly TcpClient tcpClient_;
        private readonly NetworkStream networkStream_;
        private static ClientServerAPI clientServerApi;
        private static string ServerIp { set; get; }
        private static string ServerPort { set; get; }

        private ClientServerAPI()
        {
            tcpClient_ = new TcpClient();
            networkStream_ = Connect(ServerIp, ServerPort);
        }

        public static void SetApiConfig(string serverIp, string serverPort)
        {
            ServerIp = serverIp;
            ServerPort = serverPort;
        }

        public static ClientServerAPI GetInstanse()
        {
            if (clientServerApi == null)
                clientServerApi = new ClientServerAPI();
            return clientServerApi;
        }

        ~ClientServerAPI()
        {
            tcpClient_.Close();
            if (networkStream_ != null)
                networkStream_.Close();
        }

        private NetworkStream Connect(string ServerIp, string ServerPort)
        {
            var connectTask = tcpClient_.ConnectAsync(IPAddress.Parse(ServerIp), Convert.ToInt32(ServerPort));
            connectTask.Wait();
            if (tcpClient_.Connected)
                return tcpClient_.GetStream();
            throw new Exception("Не удалось установить соединение с сервером!");
        }

        public async Task<OperationResultInfo> SendAsync(OperationMessageToServer messageToServer)
        {
            try
            {
                byte[] binary_data = Encoding.UTF8.GetBytes(Enum.GetName(typeof(Operations), messageToServer.Operation) + ":" + messageToServer.Data.ToString());
                await networkStream_.WriteAsync(binary_data, 0, binary_data.Length).ConfigureAwait(false);
                return await RecieveAsync().ConfigureAwait(false);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<OperationResultInfo> RecieveAsync()
        {
            try
            {
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    int countBytes = await networkStream_.ReadAsync(buffer, 0, 256).ConfigureAwait(false);
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, countBytes));
                } while (networkStream_.DataAvailable);
                var operationInfo = stringBuilder.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                Debug.WriteLine("Получено сообщение - " + stringBuilder.ToString());
                return new OperationResultInfo()
                {
                    OperationResult = (OperationsResults)Enum.Parse(typeof(OperationsResults), operationInfo[0]),
                    Info = operationInfo[1]
                };
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
