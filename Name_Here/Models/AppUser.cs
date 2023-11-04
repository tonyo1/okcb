using Microsoft.AspNetCore.Identity;

namespace Name_Here.Models
{
    /// <summary>
    /// General user class, email could (should maybe) 
    /// be populated from some oauth provider, google microsoft etc.
    /// 
    /// the Role info will be managed by the application, (aka persisted somewhere safe)
    /// </summary>
    public class AppUser : IdentityUser
    {
        public string PartitionKey { get; } = "AppUser";

        public string ETag { get; set; }

        public AppUser()
        {
             
        }
        // this is us for sure - application
        public Roles Role { get; set; }
         
    }
}