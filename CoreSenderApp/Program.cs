namespace CoreSenderApp
{
  using System;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using Microsoft.Azure.ServiceBus;

  class Program
  {
    const string ServiceBusConnectionString = "Endpoint=sb://ubidyhellomessagequeueing.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=c8++r5kNbNYBuZe1SVq2IB4HbhBq8vCMhPVEG52/2/I=";
    const string TopicName = "HelloMessageQueueing";
    static ITopicClient topicClient;

    static void Main(string[] args)
    {
      MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
      const int numberOfMessages = 10;
      topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

      Console.WriteLine("======================================================");
      Console.WriteLine("Press ENTER key to exit after sending all the messages.");
      Console.WriteLine("======================================================");

      // Send messages.
      await SendMessagesAsync(numberOfMessages);

      Console.ReadKey();

      await topicClient.CloseAsync();
    }

    static async Task SendMessagesAsync(int numberOfMessagesToSend)
    {
      try
      {
        for (var i = 0; i < numberOfMessagesToSend; i++)
        {
          // Create a new message to send to the topic
          string messageBody = $"Message {i}";
          var message = new Message(Encoding.UTF8.GetBytes(messageBody));

          // Write the body of the message to the console
          Console.WriteLine($"Sending message: {messageBody}");

          // Send the message to the topic
          await topicClient.SendAsync(message);
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
      }
    }
  }
}