using System;
using NsbServiceHostBasedGatewayWithSaga.Messages.Commands;

namespace NsbServiceHostBasedGatewayWithSaga.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      ServiceBus.Init();
      var rgn = new Random();
      var bus = ServiceBus.Bus;

      while (true)
      {
        Console.WriteLine("Enter 'S' to start a transfer");
        Console.WriteLine("Press any other key to exit");

        var input = (Console.ReadLine() ?? "").ToUpper();

        if (input != "S")
        {
          break;
        }

        var id = Guid.NewGuid();
        bus.Send<SendTransfer>("NsbServiceHostBasedGatewayWithSaga.LocalSite", m => {
          m.Id = id;
          m.Amount = Convert.ToDecimal(rgn.Next(10000) / 100.0);
          m.Currency = "EUR";
          m.Reference = Guid.NewGuid().ToString();
        });

        Console.WriteLine("request '{0}' sent", id);
      }

      bus.Dispose();
    }
  }
}
