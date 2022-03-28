using NUnit.Framework;
using System;

namespace LegacyApp.Tests
{
  [TestFixture]
  public class Tests
  {
    private UserService GetSystemUnderTest()
    {
      return new UserService();
    }

    [TestCase("")]
    [TestCase(null)]
    public void AddUser_WhenFirNameInvalid_ThenShouldReturnFalse(string firName)
    {
      // Arrange
      var userService = GetSystemUnderTest();

      //Act
      var result = userService.AddUser(firName, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase("")]
    [TestCase(null)]
    public void AddUser_WhenSurnameInvalid_ThenShouldReturnFalse(string surname)
    {
      // Arrange
      var userService = GetSystemUnderTest();

      //Act
      var result = userService.AddUser(TestConstants.Firname, surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase("sashengmail.co.za")]
    [TestCase("sashen@gmail")]
    [TestCase("sashengmail")]
    public void AddUser_WhenEmailInvalid_ThenShouldReturnFalse(string email)
    {
      // Arrange
      var userService = GetSystemUnderTest();

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }


    public void AddUser_WhenAgeUnder21_ThenShouldReturnFalse()
    {
      // Arrange
      var userService = GetSystemUnderTest();

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, email, DateTime.Now, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }
  }
}