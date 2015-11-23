using System;
using NServiceBus.Saga;

namespace NsbServiceHostBasedGatewayWithSaga.LocalSite
{
  public class LocalSiteSaga : ContainSagaData
  {
    /// <summary>
    /// Id of the transfer
    /// </summary>
    [Unique]
    public virtual Guid TransferId { get; set; }
  }
}