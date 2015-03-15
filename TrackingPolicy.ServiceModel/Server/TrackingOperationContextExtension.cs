// Source: https://github.com/ajdotnet/wcf-policy
using System.ServiceModel;

namespace TrackingPolicy.ServiceModel.Server
{
    /// <summary>
    /// maintains header values during the operation; available to client code via
    /// <code>
    ///     var headers = OperationContext.Current.Extensions.Find&lt;ITrackingInformation&gt;();
    /// </code>
    /// </summary>
    internal class TrackingOperationContextExtension : TrackingInformation, IExtension<OperationContext>
    {
        void IExtension<OperationContext>.Attach(OperationContext owner) { }
        void IExtension<OperationContext>.Detach(OperationContext owner) { }
    }
}
