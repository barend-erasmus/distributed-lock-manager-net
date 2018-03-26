using System;
using System.Net.Sockets;
using System.Text;

namespace DistributedLockManagerNet
{
    public class DistributedLockManagerClient: IDisposable
    {
        private string _host { get; set; }
        
        private int _port { get; set; }

        private NetworkStream _tcpStream { get; set; }

        private TcpClient _tcpClient { get; set; }

        public DistributedLockManagerClient(string host, int port)
        {
            _host = host;
            _port = port;

            ConnectToServer();
        }

        public bool Acquire(string name)
        {
            var result = Execute($"acquire {name}\r\n");

            if (result == "TRUE\r\n")
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _tcpStream.Close();
            _tcpClient.Close();
        }

        public bool Release(string name)
        {
            var result = Execute($"release {name}\r\n");

            if (result == "OK\r\n")
            {
                return true;
            }

            return false;
        }

        public bool WaitAcquire(string name)
        {
            var result = Execute($"waitAcquire {name}\r\n");

            if (result == "TRUE\r\n")
            {
                return true;
            }

            return false;
        }

        protected string Execute(string command)
        {
            if (!_tcpClient.Connected)
            {
                ConnectToServer();
            }

            var commandBytes = Encoding.ASCII.GetBytes(command);
            _tcpStream.Write(commandBytes, 0, commandBytes.Length);

            byte[] bytesReceived = new byte[128];

            int numberOfBytesReceived = _tcpStream.Read(bytesReceived, 0, bytesReceived.Length);

            var response = "";
            for (int index = 0; index < numberOfBytesReceived; index++)
            {
                response += (char)bytesReceived[index];
            }

            return response; 
        }

        private void ConnectToServer()
        {
            _tcpClient = new TcpClient(_host, _port);

            _tcpStream = _tcpClient.GetStream();
        }
    }
}
