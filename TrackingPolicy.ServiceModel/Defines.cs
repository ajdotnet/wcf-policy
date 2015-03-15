// Source: https://github.com/ajdotnet/wcf-policy

namespace TrackingPolicy.ServiceModel
{
    public static class Defines
    {
        public const string HeaderNamespace = "http://schemas.ajdotnet.wordpress.com/2014/06/Tracking";
        public const string HeaderNamespacePrefix = "t";
        public const string HeaderElementTrackingEntries = "Tracking";

        public const string PolicyNamespace = HeaderNamespace + "/policy";
        public const string PolicyNamespacePrefix = HeaderNamespacePrefix;
        public const string PolicyElement = "UsingTracking"; // WS-Addressing uses "UsingAddressing"...
    }
}
