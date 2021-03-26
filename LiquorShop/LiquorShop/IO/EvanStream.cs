using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EvanDangol.IO
{
   public static class EvanStream
    {
        public static void Copy(this string from, string to)
        {
            FileStream sourcestream = new FileStream(from, FileMode.Open);
            FileStream destinationstream = new FileStream(to, FileMode.OpenOrCreate);
            //CharacterWriter(sourcestream, destinationstream);

            BinaryReader breader = new BinaryReader(sourcestream);
            BinaryWriter bwriter = new BinaryWriter(destinationstream);
            byte[] bytes = new byte[sourcestream.Length];
            breader.Read(bytes, 0, (int)sourcestream.Length);
            bwriter.Write(bytes);
            sourcestream.Close();
            destinationstream.Close();

        }

        public static void CopyBuffer(this string from, string to)
        {
            const int BUFFER_SIZE = 8192;
            FileStream sourcestream = new FileStream(from, FileMode.Open);
            FileStream destinationstream = new FileStream(to, FileMode.OpenOrCreate);

            //CharacterWriter(sourcestream, destinationstream);

            BufferedStream bsin = new BufferedStream(sourcestream);
            BufferedStream bsout = new BufferedStream(destinationstream);
            byte[] bytes = new byte[BUFFER_SIZE];
            int bytesRead;
            while ((bytesRead = bsin.Read(bytes, 0, BUFFER_SIZE)) > 0)
            {
                bsout.Write(bytes, 0, bytesRead);
            }
            bsout.Flush();
            bsin.Close();
            bsout.Close();
            sourcestream.Close();
            destinationstream.Close();

        }

        private static void CharacterCopy(FileStream sourcestream, FileStream destinationstream)
        {
            StreamReader reader = new StreamReader(sourcestream);
            StreamWriter writer = new StreamWriter(destinationstream);
            char[] buffer = new char[sourcestream.Length];
            reader.Read(buffer, 0, (int)sourcestream.Length);
            writer.Write(buffer);
            reader.Close();
            writer.Close();
            sourcestream.Close();
            destinationstream.Close();
        }

        public static void NetworkStreamServer()
        {
            const int PORT_NO = 5000;
            const string SERVER_IP = "127.0.0.1";
            //---listen at the specified IP and port no.---           
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);


            listener.Start();
            while (true)
            {
                Console.WriteLine("Listening...");
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + dataReceived);

                //---write back the text to the client---
                Console.WriteLine("Sending back: " + dataReceived);
                nwStream.Write(buffer, 0, bytesRead);


                client.Close();
            }
           
        }

        public static void NetworkStreamClient()
        {
            const int PORT_NO = 5000;
            const string SERVER_IP = "127.0.0.1";
            //---data to send to the server---            
            string textToSend = DateTime.Now.ToString();

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---            
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---           
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

            Console.WriteLine("Received: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();

            client.Close();

        }


    }
}
