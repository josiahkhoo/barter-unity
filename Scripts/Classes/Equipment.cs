

namespace Barter.Classes
{
    public class Equipment
    {
        public int id;
        public string name;
        public int type;
        public string appearanceConfig;

        public Equipment(int id, string name, int type, string appearanceConfig)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.appearanceConfig = appearanceConfig;
        }
    }
}