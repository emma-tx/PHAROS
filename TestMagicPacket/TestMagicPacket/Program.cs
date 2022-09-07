using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace TestMagicPacket
{
    class Program
    {
        static void Main(string[] args)
        {
            var macAddress = "98:90:96:BA:0F:77";
            string broadcastAddress = "193.63.135.255";

            var targetIp = IPAddress.Parse(broadcastAddress);

            UdpClient udpClient = new UdpClient();

            // enable UDP broadcasting for UdpClient
            udpClient.EnableBroadcast = true;

            var dgram = new byte[1024];

            // 6 magic bytes
            for (int i = 0; i < 6; i++)
            {
                dgram[i] = 255;
            }

            // convert MAC-address to bytes
            byte[] address_bytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                address_bytes[i] = byte.Parse(macAddress.Substring(3 * i, 2), NumberStyles.HexNumber);
            }

            // repeat MAC-address 16 times in the datagram
            var macaddress_block = dgram.AsSpan(6, 16 * 6);
            for (int i = 0; i < 16; i++)
            {
                address_bytes.CopyTo(macaddress_block.Slice(6 * i));
            }

            // Included a commented out line for generic WOL packet to 255.255.255.
            // Both work, but sends on a different network interface, so we can assume the Uni broadcast address will also work.
            udpClient.Send(dgram, dgram.Length, new System.Net.IPEndPoint(targetIp, 9));
            //udpClient.Send(dgram, dgram.Length, new System.Net.IPEndPoint(IPAddress.Broadcast, 9));

            udpClient.Close();

            var computerStatus = "Waking";

            Console.WriteLine(computerStatus);

            Console.ReadKey();
        }
    }

}