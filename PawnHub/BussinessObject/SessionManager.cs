namespace BussinessObject
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; }

        public static int UserId { get; set; }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
    }


}
