using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp_Learning
{
    class SocketTest
    {
        public static void Run()
        {
            // Example 13-24. Using a Socket to fetch data from a daytime server
            IPHostEntry hostDnsEntry = Dns.GetHostEntry("time-nw.nist.gov");
            IPAddress serverIp = hostDnsEntry.AddressList[0];

            Socket daytimeSocket = new Socket(
                serverIp.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            daytimeSocket.Connect(serverIp, 13);
            string data;

            // Example 13-23. Retrieving ASCII data from a TCP socket
            using (Stream timeServiceStream = new NetworkStream(daytimeSocket, true))
            using (StreamReader timeServiceReader = new StreamReader(timeServiceStream,
            Encoding.ASCII))
            {
                data = timeServiceReader.ReadToEnd();
            }

            Console.WriteLine(data);
        }
    }
}
