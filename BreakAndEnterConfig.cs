using Rocket.API;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public class BreakAndEnterConfig : IRocketPluginConfiguration
    {
        public bool AutoCloseDoors;
        public int AutoCloseDoorsDelay;
        
        public void LoadDefaults()
        {
            AutoCloseDoors = false;
            AutoCloseDoorsDelay = 2500;
        }
    }
}