using System;
using System.Net.Sockets;
using System.Threading;
using networking;
using services;

namespace server
{
    class StartLibraryServer
    {
        static void Main(string[] args)
        {
            ILibraryServer serviceImpl = new LibraryServerImpl();

			SerialChatServer server = new SerialChatServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();
            
        }
    }

    public class SerialChatServer: ConcurrentServer 
    {
        private ILibraryServer server;
        private LibraryClientWorker worker;
        public SerialChatServer(string host, int port, ILibraryServer server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new LibraryClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
    
}
