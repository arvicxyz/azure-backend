using AppServiceHelpers;
using AppServiceHelpers.Abstractions;

namespace AzureBackend.Shared
{
    public class AzureConnection
    {
        private IEasyMobileServiceClient client;
        public IEasyMobileServiceClient Client
        {
            get { return client; }
        }

        public AzureConnection()
        {
            client = EasyMobileServiceClient.Create();
            client.Initialize("http://azurebackendloginregister.azurewebsites.net/");
            client.RegisterTable<User>();
            client.FinalizeSchema();
        }
    }
}
