// Source: https://github.com/ajdotnet/wcf-policy
using System.ServiceModel;

namespace TrackingPolicy.TestServer.Services
{
    [ServiceContract(Namespace = "http://TrackingPolicy.TestServer/ITestService")]
    public interface ITestService
    {
        [OperationContract]
        string Ping(string message);
    }
}
