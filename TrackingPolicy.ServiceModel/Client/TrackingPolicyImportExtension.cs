// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.ServiceModel.Description;
using System.Xml;

namespace TrackingPolicy.ServiceModel.Client
{
    /// <summary>
    /// announce in app.config:
    /// <code>
    /// <![CDATA[
    ///  <client>
    ///     <endpoint ... />
    ///     <metadata>
    ///         <policyImporters>
    ///             <extension type="TrackingPolicy.ServiceModel.Client.TrackingPolicyImportExtension, TrackingPolicy.ServiceModel" />
    ///         </policyImporters>
    ///     </metadata>
    ///  </client>
    /// ]]>
    /// </code>
    /// </summary>
    public sealed class TrackingPolicyImportExtension : IPolicyImportExtension
    {
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            Guard.AssertNotNull(context, "context");

            // Locate the custom assertion and remove it.
            XmlElement customAssertion = context.GetBindingAssertions().Remove(Defines.PolicyElement, Defines.PolicyNamespace);
            if (customAssertion == null)
                return;

            // actually process it
            context.Contract.Behaviors.Add(new TrackingPolicyCodeGenerator());
        }
    }
}
