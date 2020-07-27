using Barter.Classes;

namespace Barter.Classes
{

    public class Party
    {
        public int id;
        public int chatId;
        public string accessCode;
        public Monster monster;

        public Party(int id, int chatId, string accessCode, Monster monster)
        {
            this.id = id;
            this.chatId = chatId;
            this.accessCode = accessCode;
            this.monster = monster;
        }
    }

}