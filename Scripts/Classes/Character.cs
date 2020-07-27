namespace Barter.Classes
{

    public class Character
    {
        public int id;
        public string name;
        public string appearanceConfig;
        public bool isActive;

        public Character(int id, string name, string appearanceConfig, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.appearanceConfig = appearanceConfig;
            this.isActive = isActive;
        }

        public override string ToString()
        {
            return name;
        }


        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            Character c = obj as Character;
            if ((System.Object)c == null)
                return false;
            return id == c.id;
        }

        public bool Equals(Character c)
        {
            if ((object)c == null)
                return false;
            return id == c.id;
        }


    }

}