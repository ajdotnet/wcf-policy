// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    /// Base class for message headers. Provides some common features for serialization
    /// </summary>
    internal abstract class BaseMessageHeader : MessageHeader
    {
        public abstract string NamespacePrefix { get;  }

        protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            Guard.AssertNotNull(writer, "writer");

            // default implementation does not use XML namespace prefixes...
            writer.WriteStartElement(this.NamespacePrefix, this.Name, this.Namespace);
            this.WriteHeaderAttributes(writer, messageVersion);
        }

        protected static BaseMessageHeader ReadHeader(MessageHeaders headers, string name, string ns, Func<XmlDictionaryReader, BaseMessageHeader> readHeaderContents)
        {
            int headerIndex = headers.FindHeader(name, ns);
            if (headerIndex < 0)
                return null;

            // Get an XmlDictionaryReader to read the header content
            var reader = headers.GetReaderAtHeader(headerIndex);
            var header = readHeaderContents(reader);

            // NOTE: We must(!) remove ("consume") the header if MustUnderstand=1, 
            // otherwise it will be considered "not understood" and an exception will be thrown.
            headers.RemoveAt(headerIndex);

            return header;
        }
    }
}
