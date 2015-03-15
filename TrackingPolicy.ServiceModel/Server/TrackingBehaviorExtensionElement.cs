// Source: https://github.com/ajdotnet/wcf-policy
using System;
using System.ServiceModel.Configuration;

namespace TrackingPolicy.ServiceModel.Server
{
    /// <summary>
    /// announce in the web.config and use it to configure the service:
    /// <code>
    /// <![CDATA[
    /// <system.serviceModel>
    /// <extensions>
    ///     <behaviorExtensions>
    ///         <add name="trackingBehavior" type="TrackingPolicy.ServiceModel.Server.TrackingBehaviorExtensionElement, TrackingPolicy.ServiceModel" />
    ///     </behaviorExtensions>
    /// </extensions>
    /// <behaviors>
    ///     <serviceBehaviors>
    ///         <behavior name="withPolicyBehavior">
    ///             <trackingBehavior />
    ///             <serviceMetadata httpGetEnabled="true" httpsGetEnabled="true" />
    ///             <serviceDebug includeExceptionDetailInFaults="true" />
    ///         </behavior>
    ///     </serviceBehaviors>
    /// </behaviors>
    /// </system.serviceModel>
    /// ]]>
    /// </code>
    /// Alternatively use the <see cref="TrackingBehaviorAttribute"/> in code.
    /// </summary>
    public class TrackingBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TrackingBehaviorAttribute); }
        }

        protected override object CreateBehavior()
        {
            return new TrackingBehaviorAttribute();
        }
    }
}
