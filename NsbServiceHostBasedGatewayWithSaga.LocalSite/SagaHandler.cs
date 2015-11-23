using System;
using System.Threading;
using NsbServiceHostBasedGatewayWithSaga.Messages.Commands;
using NsbServiceHostBasedGatewayWithSaga.Messages.Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace NsbServiceHostBasedGatewayWithSaga.LocalSite
{
  public class SagaHandler : Saga<LocalSiteSaga>, 
    IAmStartedByMessages<SendTransfer>,
    IHandleMessages<TransferPrepared>
  {
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<LocalSiteSaga> mapper)
    {
      mapper.ConfigureMapping<TransferPrepared>(s => s.Id)
            .ToSaga(m => m.TransferId);
    }

    public void Handle(SendTransfer message)
    {
      Data.TransferId = message.Id;
      Console.WriteLine("Starting saga for message '{0}'", message.Id);
      // fake processing
      Thread.Sleep(250);
      Bus.SendToSites(new[] {"RemoteSite"}, new PrepareTransfer() {
        Id = message.Id,
        Amount = message.Amount,
        Currency = message.Currency,
        Reference = message.Reference
      });

      Console.WriteLine("Message sent to remote site");
    }

    public void Handle(TransferPrepared message)
    {
      Console.WriteLine("Acknowlegment message for transfer '{0}' from remote site received", message.Id);
      Thread.Sleep(250);
      Console.WriteLine("Acknowlegment message for transfer '{0}' processed", message.Id);
      MarkAsComplete();
    }
  }
}
