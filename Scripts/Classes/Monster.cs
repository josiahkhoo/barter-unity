namespace Barter.Classes
{

    public class Monster
    {
        public int id;
        public string name;
        public string appearanceConfig;
        public bool isActive;
        public int level;
        public int duration;
        public int partySize;

        public Monster(int id, string name, string appearanceConfig, bool isActive, int level, int duration, int partySize)
        {
            this.id = id;
            this.name = name;
            this.appearanceConfig = appearanceConfig;
            this.isActive = isActive;
            this.level = level;
            this.duration = duration;
            this.partySize = partySize;
        }

        public override string ToString()
        {
            return name;
        }

    }

}