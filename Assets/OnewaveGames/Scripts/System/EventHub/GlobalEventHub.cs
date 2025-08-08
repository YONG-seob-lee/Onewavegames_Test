namespace OnewaveGames.Scripts.EventHub
{
    public class GlobalEventHub
    {
        public static ManagerEventHub EventHub { get; private set; }
        public static Skill_EventHub SkillHub { get; private set; }

        public static void InitializeEventHub(ManagerEventHub hub)
        {
            EventHub = hub;
        }

        public static void InitializeSkillHub(Skill_EventHub hub)
        {
            SkillHub = hub;
        }

        public static void Reset()
        {
            EventHub = null;
            SkillHub = null;
        }
    }
}