namespace Name_Here.Tests
{
    public class SerilizerTest
    {
        [Fact]
        public void UserToJsonandBack()
        {
            User testuser = new User { Email = "test1@gmail.com", Name = "test1", Role = Role.User };

            string json = testuser.Serialize();

            User testuser2 = json.Deserialize<User>();

            Assert.True(testuser.Name == testuser2.Name);
        }
    }
}
