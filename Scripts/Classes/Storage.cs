namespace Barter.Classes
{

    public static class Storage
    {
        private static User user;

        private static string token;


        public static User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }

        }

        public static string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = value;
            }
        }
    }
}