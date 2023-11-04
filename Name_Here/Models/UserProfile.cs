namespace Name_Here.Models
{

    /// <summary>
    ///     
    /// </summary>
    public class UserProfile
    {   
        public string ETag { get; set; }

        public string Email { get; set; }

        // base64 for now
        public string Picture { get; set; }

        public string PartitionKey { get; } = "UserProfile";
    }
}