using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using APIQueueCommunication.Models;

namespace ApiNuvem-ldlm.Services
{
    public class FilaServices
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=buytofun;AccountKey=bLDR0ewVsim9P7JwRp18kPgTDsBXRT3sD+vhgc3bnIHR1yf+xDUSowOkCjuY/jdaJSv4eLsfhAfB3jQnfn9FXg==;EndpointSuffix=core.windows.net";
        private CloudStorageAccount cloudStorageAccount;
        private CloudQueue queueOne;

        public QueueService()
        {
            connect();
            queueOne = getQueue("queueone");
        }

        private void connect()
        {
            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Console.WriteLine("Failed to connect to StorageAccount");
                //throw new Exception("Failed to connect to StorageAccount");
                //TODO How can I do Exception treatment in C#??? I m just logging the error message insted to send to the client
            }
        }

        private CloudQueue getQueue(String queueName)
        {
            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            var queueInstance = cloudQueueClient.GetQueueReference(queueName);

            queueInstance.CreateIfNotExistsAsync();

            return queueInstance;
        }

        public void sendMessage(MessageQueue messageQueue)
        {
            var cloudQueueMessage = new CloudQueueMessage(messageQueue.message);
            queueOne.AddMessageAsync(cloudQueueMessage);
        }
    }
}
