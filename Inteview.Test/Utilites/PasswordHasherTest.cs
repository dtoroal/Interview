using Interview.Authenticator.Utilites;

namespace Inteview.Test;

public class PasswordHasherTest
{

    [Theory]
    [InlineData("Test123.")]
    public void HashPassword(string password)
    {
        string expected = "HUA+kDG8PADWP4Los/kRo9omIK4dsKVLaOhtzKF1G2I=";
        var passwordHasher = new PasswordHasher();

        var passwordHashed = passwordHasher.HashPassword(password);

        Assert.Equal(expected, passwordHashed);
    }

    [Theory]
    [InlineData("Test123.", "HUA+kDG8PADWP4Los/kRo9omIK4dsKVLaOhtzKF1G2I=")]
    public void VerifyPassword(string password, string passwordHash)
    {
        var passwordHasher = new PasswordHasher();

        var result = passwordHasher.VerifyPassword(password, passwordHash);

        Assert.True(result);
    }

}
