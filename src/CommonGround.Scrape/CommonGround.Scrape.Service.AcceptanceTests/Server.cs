using Microsoft.Owin.Testing;

namespace CommonGround.Scrape.Service.AcceptanceTests
{
    public class Server
    {
        private static readonly object SyncRoot = new object();
        private static volatile TestServer _instance;

        private Server()
        { }

        public static TestServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = TestServer.Create<Startup>();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}