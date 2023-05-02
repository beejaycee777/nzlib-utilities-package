namespace NZLib.Utilities.CommunicationCore
{
    [System.Serializable]
    /// <summary>
    /// Contains IpConfig information.
    /// </summary>
    public sealed class IpConfig
    {
        /* ===================================================================================== */
        // Parameters

        public string Name;
        public string ServerIp;
        public int ServerPort;
        public int ServerAmount;
        public string ClientIp;
        public int ClientPort;
        public string ClientMac;
        public int Timeout;
    }   
}
