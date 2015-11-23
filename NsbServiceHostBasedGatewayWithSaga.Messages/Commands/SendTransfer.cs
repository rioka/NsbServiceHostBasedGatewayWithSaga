using System;

namespace NsbServiceHostBasedGatewayWithSaga.Messages.Commands
{
  public class SendTransfer
  {
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Reference { get; set; }
  }
}
