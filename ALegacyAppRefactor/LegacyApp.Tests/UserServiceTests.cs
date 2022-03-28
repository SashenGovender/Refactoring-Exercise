using LegacyApp.Models;
using LegacyApp.Tests.TestHelpers;
using LegacyApp.Validators;
using NSubstitute;
using NUnit.Framework;
using System;

namespace LegacyApp.Tests
{
  [TestFixture]
  public class UserServiceTests
  {
    private UserService GetSystemUnderTest(TestDoubles testDoubles)
    {
      return new UserService(testDoubles.ClientRepository, new UserValidator(testDoubles.DateTimeProvider), testDoubles.UserDataAccess, testDoubles.UserCreditService);
    }

    [TestCase("")]
    [TestCase(null)]
    public void AddUser_WhenFirNameInvalid_ThenShouldReturnFalse(string firName)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      var userService = GetSystemUnderTest(testDoubles);

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
      var testDoubles = TestDoubles.GetDoubles();
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase("sashen@gmailcoza")]
    public void AddUser_WhenEmailInvalid_ThenShouldReturnFalse(string email)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase(2000, 04,04)]
    [TestCase(2013, 04, 03)]
    public void AddUser_WhenAgeUnder21_ThenShouldReturnFalse(int year, int month, int day)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.DateTimeProvider.DateTimeNow.Returns(new DateTime(year, month, day));

      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void AddUser_WhenVeryImportantClient_ThenShouldSkipCreditCheckAndReturnTrue()
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.ClientRepository.GetById(Arg.Any<int>()).Returns(TestConstants.GetClient("VeryImportantClient"));
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsTrue(result);
    }

    [TestCase(50)]
    [TestCase(249)]
    public void AddUser_WhenImportantClientAndCreditCheckIsLessThen500_ThenShoulReturnFalse(int creditLimit)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.ClientRepository.GetById(Arg.Any<int>()).Returns(TestConstants.GetClient("ImportantClient"));
      testDoubles.UserCreditService.GetCreditLimit(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>()).Returns(creditLimit);
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase(250)]
    [TestCase(251)]
    public void AddUser_WhenImportantClientAndCreditCheckIsMoreThen500_ThenShoulReturnTrue(int creditLimit)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.ClientRepository.GetById(Arg.Any<int>()).Returns(TestConstants.GetClient("ImportantClient"));
      testDoubles.UserCreditService.GetCreditLimit(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>()).Returns(creditLimit);
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsTrue(result);
    }

    [TestCase(0)]
    [TestCase(499)]
    public void AddUser_WhenDefaultClientAndCreditCheckIsLessThen500_ThenShoulReturnFalse(int creditLimit)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.ClientRepository.GetById(Arg.Any<int>()).Returns(TestConstants.GetClient("DefaultClient"));
      testDoubles.UserCreditService.GetCreditLimit(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>()).Returns(creditLimit);
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsFalse(result);
    }

    [TestCase(500)]
    [TestCase(501)]
    public void AddUser_WhenDefaultClientAndCreditCheckIsMoreThen500_ThenShoulReturnTrue(int creditLimit)
    {
      // Arrange
      var testDoubles = TestDoubles.GetDoubles();
      testDoubles.ClientRepository.GetById(Arg.Any<int>()).Returns(TestConstants.GetClient("DefaultClient"));
      testDoubles.UserCreditService.GetCreditLimit(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>()).Returns(creditLimit);
      var userService = GetSystemUnderTest(testDoubles);

      //Act
      var result = userService.AddUser(TestConstants.Firname, TestConstants.Surname, TestConstants.Email, TestConstants.DateOfBirth, TestConstants.ClientId);

      //Assert
      Assert.IsTrue(result);
    }

  }
}