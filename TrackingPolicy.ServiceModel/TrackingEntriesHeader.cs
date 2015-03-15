// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Channels;
using System.Xml;

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    /// Helper class for "serializing" the SOAP header (i.e. low-level XML operations).
    /// </summary>
    internal class TrackingEntriesHeader : BaseMessageHeader
    {
        public override string Name { get { return Defines.HeaderElementTrackingEntries; } }
        public override string Namespace { get { return Defines.HeaderNamespace; } }
        public override string NamespacePrefix { get { return Defines.HeaderNamespacePrefix; } }
        public override bool MustUnderstand { get { return true; } }

        public IEnumerable<TrackingEntry> TrackingEntries { get; private set; }

        public TrackingEntriesHeader(IEnumerable<TrackingEntry> entries)
        {
            if (entries != null)
                TrackingEntries = entries.ToArray();
            else
                TrackingEntries = Enumerable.Empty<TrackingEntry>();
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            Guard.AssertNotNull(writer, "writer");

            foreach (var entry in TrackingEntries)
            {
                writer.WriteStartElement(NamespacePrefix, "Entry", Namespace);
                writer.WriteAttributeString("Source", entry.Source);
                writer.WriteAttributeString("TimeStamp", entry.Timestamp.ToString("o", CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Reason", entry.Reason);
                writer.WriteEndElement();
            }
        }

        public static TrackingEntriesHeader ReadHeader(MessageHeaders headers)
        {
            return (TrackingEntriesHeader)ReadHeader(headers, Defines.HeaderElementTrackingEntries, Defines.HeaderNamespace, ReadHeaderContents);
        }

        static TrackingEntriesHeader ReadHeaderContents(XmlDictionaryReader reader)
        {
            reader.ReadStartElement(Defines.HeaderElementTrackingEntries, Defines.HeaderNamespace); // skip ...
            var entries = new List<TrackingEntry>();
            while ((reader.LocalName == "Entry") && (reader.NamespaceURI == Defines.HeaderNamespace))
            {
                var entry = ReadEntry(reader);
                entries.Add(entry);

                reader.Read();
                if (reader.NodeType == XmlNodeType.EndElement)
                    reader.ReadEndElement();
            }
            return new TrackingEntriesHeader(entries);
        }

        static TrackingEntry ReadEntry(XmlDictionaryReader reader)
        {
            var source = reader.GetAttribute("Source");
            var timeStamp = reader.GetAttribute("TimeStamp");
            var message = reader.GetAttribute("Reason");
            var ts = DateTime.ParseExact(timeStamp, "o", null, DateTimeStyles.RoundtripKind);
            return new TrackingEntry(source, message, ts);
        }
    }
}
