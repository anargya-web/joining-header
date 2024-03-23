using TerrariaApi.Server;

namespace PluginTemplate
{
    [ApiVersion(2, 1)]
    public class StatusText : Module
    {
        public bool BlockVanillaStatus { get; set; } = true;
        private bool _isSendingStatus;

        public StatusText(Plugin p) : base(p)
        {
            ServerApi.Hooks.NetSendData.Register(p, OnNetSendData);
        }

        private void OnNetSendData(SendDataEventArgs args)
        {
            if (args.MsgId == PacketTypes.Status && !_isSendingStatus)
                args.Handled = true;
        }

        public void SendStatusPacket(TSPlayer player, string text, int max = 0, byte flags = 1)
        {
            _isSendingStatus = true;
            player.SendData(PacketTypes.Status, text, max, flags);
            _isSendingStatus = false;
        }

        public override void Dispose()
        {
        }
    }
}
