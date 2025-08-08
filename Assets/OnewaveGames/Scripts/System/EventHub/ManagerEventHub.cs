namespace OnewaveGames.Scripts.EventHub
{
    public class ManagerEventHub
    {
        
    }

    public class Signal_InitializeManagers
    {
        public ManagerEventHub EventHub;

        public Signal_InitializeManagers(ManagerEventHub eventHub)
        {
            EventHub = eventHub;
        }
    }
}