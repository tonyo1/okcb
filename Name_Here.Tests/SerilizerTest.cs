using Name_Here.Models;

namespace Name_Here.Tests
{
    public class SerilizerTest
    {
        [Fact]
        public void UserToJsonandBack()
        {
            AppUser testuser = new AppUser { Email = "test1@gmail.com", UserName = "test1", Role = Roles.Admin };

            string json = testuser.Serialize();

            AppUser testuser2 = json.Deserialize<AppUser>();

            Assert.True(testuser.UserName == testuser2.UserName);
        }
    }
}
