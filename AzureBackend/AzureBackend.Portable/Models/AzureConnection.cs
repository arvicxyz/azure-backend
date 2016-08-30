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
            // 1. Create a new EasyMobileServiceClient.
            client = EasyMobileServiceClient.Create();

            // 2. Initialize the library with the URL of the Azure Mobile App you created in Step #1.
            // Example: http://appservicehelpers.azurewebsites.net
            client.Initialize("Your_Mobile_App_Backend_Url_Here");

            // 3. Register a model with the EasyMobileServiceClient to create a table.
            client.RegisterTable<User>();

            // 4. Finalize the schema for our database. All table registrations must be done before this step.
            client.FinalizeSchema();
        }
    }
}
