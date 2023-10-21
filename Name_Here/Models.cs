namespace Name_Here
{
    /// <summary>
    /// General user class, email could (should maybe) 
    /// be populated from some oauth provider, google microsoft etc.
    /// 
    /// the Role info will be managed by the application, (aka persisted somewhere safe)
    /// </summary>
    public class User
    {
        // oauth
        public string Email { get; set; }

        // application or auth? talk about
        public string Name { get; set; }

        // this is us for sure - application
        public Role Role { get; set; }
    }

    /// <summary>
    /// best guess at roles for this application
    /// </summary>
    public enum Role
    {
        User = 1,
        Editor = 2,
        PowerUser = 3,
        Admin = 4
    }

    /// <summary>
    ///     
    /// </summary>
    public class UserProfile
    {
        public string Email { get; set; }

        public byte[]? Picture { get; set; }
    }
}