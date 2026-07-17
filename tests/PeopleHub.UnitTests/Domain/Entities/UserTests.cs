using PeopleHub.Domain.Entities;
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
            PhoneNumber.Create("+919876543210"));

        Assert.Equal(UserStatus.PendingVerification, user.Status);
    }

    [Fact]
    public void Activate_Should_Throw_When_Phone_Is_Not_Verified()
    {
        var user = new User(
            "John",
            "Doe",
            Email.Create("john@example.com"),
            PhoneNumber.Create("+919876543210"));

        Assert.Throws<DomainException>(() => user.Activate());
    }

    [Fact]
    public void VerifyPhone_Should_Set_IsPhoneVerified()
    {
        var user = new User(
            "John",
            "Doe",
            Email.Create("john@example.com"),
            PhoneNumber.Create("+919876543210"));

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
            PhoneNumber.Create("+919876543210"));

        user.VerifyPhone();

        user.Activate();

        Assert.Equal(UserStatus.Active, user.Status);
    }
}