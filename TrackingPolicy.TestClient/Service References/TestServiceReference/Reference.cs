﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackingPolicy.TestClient.TestServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://TrackingPolicy.TestServer/ITestService", ConfigurationName="TestServiceReference.ITestService")]
    [TrackingPolicy.ServiceModel.Client.TrackingContractBehaviorAttribute()]
    public interface ITestService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://TrackingPolicy.TestServer/ITestService/ITestService/Ping", ReplyAction="http://TrackingPolicy.TestServer/ITestService/ITestService/PingResponse")]
        string Ping(string text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestServiceChannel : TrackingPolicy.TestClient.TestServiceReference.ITestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestServiceClient : System.ServiceModel.ClientBase<TrackingPolicy.TestClient.TestServiceReference.ITestService>, TrackingPolicy.TestClient.TestServiceReference.ITestService {
        
        public TestServiceClient() {
        }
        
        public TestServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Ping(string text) {
            return base.Channel.Ping(text);
        }
    }
}