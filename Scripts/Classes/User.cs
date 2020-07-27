using System.Collections.Generic;
using SimpleJSON;

namespace Barter.Classes
{
    public class User
    {
        public int id;
        public string username;
        public string email;
        public int status;

        public List<Character> characters;

        public Character selectedCharacter;

        public User(int id, string username, string email, int status, List<Character> characters)
        {
            this.id = id;
            this.username = username;
            this.email = email;
            this.status = status;
            this.characters = characters;
            this.selectedCharacter = null;
        }

        public User SelectCharacter(Character character)
        {
            this.selectedCharacter = character;
            return this;
        }

    }
}