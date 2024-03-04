



using System;
using DiscordRPC;
namespace Sparkle
{
    public class RPC
    {
        public static DiscordRpcClient client;
        public static Timestamps rpctimestamp { get; set; }
        private static RichPresence presence;
        public static void InitializeRPC()
        {
            client = new DiscordRpcClient("1188686354490609754");
            client.Initialize();
            Button[] buttons = { new Button() { Label = "Download Sparkle", Url = "https://parcoil.com/sparkle" } };/*, new Button() { Label = "Servers", Url = "https://www.tiktok.com/@scruztopic?lang=en" } };*/

            presence = new RichPresence()
            {
                Details = "The Finest Windows Opimizer",
                State = "Desktop version",
                Timestamps = rpctimestamp,
                Buttons = buttons,

                Assets = new Assets()
                {
                    LargeImageKey = "sparkle2",
                    LargeImageText = "Download Now!",
                    SmallImageKey = "small",
                    SmallImageText = "Rily#9999"
                }
            };
            SetState("Freeing up RAM");
        }
        public static void SetState(string state, bool watching = false)
        {
            if (watching)
                state = "Looking at " + state;

            presence.State = state;
            client.SetPresence(presence);
        }
    }
}
