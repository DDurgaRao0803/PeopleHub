using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.Exceptions;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.UnitTests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_Should_Set_PendingVerification_Status()
    {
        var user = new User(
    "John",
    "Doe",
    Email.Create("john@example.com"),
    PhoneNumber.Create("+15551234567"),
    "DummyPasswordHash");

        Assert.Equal(UserStatus.PendingVerification, user.Status);
    }

    [Fact]
    public void Activate_Should_Throw_When_Phone_Is_Not_Verified()
    {
        var user = new User(
    "John",
    "Doe",
    Email.Create("john@example.com"),
    PhoneNumber.Create("+15551234567"),
    "DummyPasswordHash");

        Assert.Throws<DomainException>(() => user.Activate());
    }

    [Fact]
    public void VerifyPhone_Should_Set_IsPhoneVerified()
    {
        var user = new User(
    "John",
    "Doe",
    Email.Create("john@example.com"),
    PhoneNumber.Create("+15551234567"),
    "DummyPasswordHash");

        user.VerifyPhone();

        Assert.True(user.IsPhoneVerified);
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var user = new User(
    "John",
    "Doe",
    Email.Create("john@example.com"),
    PhoneNumber.Create("+15551234567"),
    "DummyPasswordHash");

        user.VerifyPhone();

        user.Activate();

        Assert.Equal(UserStatus.Active, user.Status);
    }
}