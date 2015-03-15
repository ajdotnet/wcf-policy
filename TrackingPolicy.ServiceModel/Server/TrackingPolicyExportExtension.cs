// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace TrackingPolicy.ServiceModel.Server
{
    /// <summary>
    /// This binding element provides the policy information for the WSDL.
    /// Use <see cref="TrackingBehaviorExtensionElement"/> in the web.config 
    /// or <see cref="TrackingBehaviorAttribute"/> to create this binding.
    /// </summary>
    internal class TrackingPolicyExportExtension : BindingElement, IPolicyExportExtension
    {
        public override BindingElement Clone()
        {
            return new TrackingPolicyExportExtension();
        }

        public override T GetProperty<T>(BindingContext context)
        {
            Guard.AssertNotNull(context, "context");

            return context.GetInnerProperty<T>();
        }

        void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
        {
            Guard.AssertNotNull(policyContext, "policyContext");

            // add the XML fragment to list of policy assertions
            var assertionElement = CreateAssertionElement();
            policyContext.GetBindingAssertions().Add(assertionElement);
        }

        private static XmlElement CreateAssertionElement()
        {
            // the XML fragment consists of a simple element, but could contain any kind of valid XML content
            var doc = new XmlDocument();
            var elem = doc.CreateElement(Defines.PolicyNamespacePrefix, Defines.PolicyElement, Defines.PolicyNamespace);
            return elem;
        }
    }
}