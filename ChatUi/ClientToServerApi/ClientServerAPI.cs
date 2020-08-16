using ClientToServerApi.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
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

        public async Task<string> SendAsync(Operations operation, object data)
        {
            try
            {
                byte[] binary_data = Encoding.UTF8.GetBytes(data.ToString());
                await networkStream_.WriteAsync(binary_data, 0, binary_data.Length);
                return await RecieveAsync();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> RecieveAsync()
        {
            try
            {
                byte[] buffer = new byte[64];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    await networkStream_.ReadAsync(buffer, 0, 64);
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer));
                } while (networkStream_.DataAvailable);
                return stringBuilder.ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
