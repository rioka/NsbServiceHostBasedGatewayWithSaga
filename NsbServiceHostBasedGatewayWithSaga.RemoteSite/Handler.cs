using System;
using System.Threading;
using NsbServiceHostBasedGatewayWithSaga.Messages.Messages;
using NServiceBus;

namespace NsbServiceHostBasedGatewayWithSaga.RemoteSite
{
  public class Handler : IHandleMessages<PrepareTransfer>
  {
    public IBus Bus { get; set; }

    public void Handle(PrepareTransfer message)
    {
      Console.WriteLine("PrepareTransfer received: {0}", message.Id);

      // fake processing
      Thread.Sleep(250);

      // Notify the remote host
      Bus.Reply<TransferPrepared>(m => {
        m.Id = message.Id;
      });

      Console.WriteLine("Aknowledgment message for transfer {0} sent", message.Id);
    }
  }
}
