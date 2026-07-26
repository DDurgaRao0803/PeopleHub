using FluentAssertions;
using Moq;
using PeopleHub.Application.Authentication;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Domain.Aggregates.Otp;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.Infrastructure.Authentication;
using Xunit;

namespace PeopleHub.UnitTests.Authentication;

public sealed class OtpServiceTests
{
    private readonly Mock<IOtpRepository> _otpRepository = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IOtpGenerator> _otpGenerator = new();
    private readonly Mock<IOtpHasher> _otpHasher = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private readonly OtpService _sut;

    public OtpServiceTests()
    {
        _sut = new OtpService(
            _otpRepository.Object,
            _userRepository.Object,
            _otpGenerator.Object,
            _otpHasher.Object,
            _unitOfWork.Object);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateOtp()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _otpGenerator
            .Setup(x => x.Generate())
            .Returns("123456");

        _otpHasher
            .Setup(x => x.Hash("123456"))
            .Returns("hashed-otp");

        _otpRepository
            .Setup(x => x.AddAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.GenerateAsync(
            userId,
            OtpPurpose.Registration);

        // Assert
        result.Should().Be("123456");

        _otpGenerator.Verify(
            x => x.Generate(),
            Times.Once);

        _otpHasher.Verify(
            x => x.Hash("123456"),
            Times.Once);

        _otpRepository.Verify(
            x => x.AddAsync(
                It.Is<OtpCode>(o =>
                    o.UserId == userId &&
                    o.Purpose == OtpPurpose.Registration &&
                    o.CodeHash == "hashed-otp"),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static User CreateUser()
    {
        return new User(
    "John",
    "Doe",
    Email.Create("john@example.com"),
    PhoneNumber.Create("+966500000000"),
    "password-hash");
    }

    private static OtpCode CreateOtp(
        Guid userId,
        bool verified = false)
    {
        var otp = OtpCode.Create(
            userId,
            "hashed-otp",
            OtpPurpose.Registration,
            TimeSpan.FromMinutes(5));

        if (verified)
        {
            otp.MarkVerified();
        }

        return otp;
    }

    [Fact]
    public async Task VerifyAsync_ShouldReturnSuccess_WhenOtpIsValid()
    {
        // Arrange
        var user = CreateUser();

        var otp = CreateOtp(user.Id);

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                user.Id,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(otp);

        _otpHasher
            .Setup(x => x.Verify(
                "123456",
                otp.CodeHash))
            .Returns(true);

        _userRepository
            .Setup(x => x.GetByIdAsync(
                user.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _otpRepository
            .Setup(x => x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _userRepository
            .Setup(x => x.UpdateAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.VerifyAsync(
            user.Id,
            "123456",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.Success);

        user.IsEmailVerified.Should().BeTrue();

        _otpRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _userRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWork.Verify(x =>
            x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task VerifyAsync_ShouldReturnInvalidOtp_WhenOtpIsIncorrect()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var otp = CreateOtp(userId);

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                userId,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(otp);

        _otpHasher
            .Setup(x => x.Verify(
                "111111",
                otp.CodeHash))
            .Returns(false);

        _otpRepository
            .Setup(x => x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.VerifyAsync(
            userId,
            "111111",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.InvalidOtp);

        otp.FailedAttempts.Should().Be(1);

        _otpRepository.Verify(x =>
            x.UpdateAsync(
                otp,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _userRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task VerifyAsync_ShouldReturnNotFound_WhenOtpDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                userId,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((OtpCode?)null);

        // Act
        var result = await _sut.VerifyAsync(
            userId,
            "123456",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.NotFound);

        _userRepository.Verify(x =>
            x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

        [Fact]
    public async Task VerifyAsync_ShouldReturnExpired_WhenOtpHasExpired()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var otp = OtpCode.Create(
            userId,
            "hashed-otp",
            OtpPurpose.Registration,
            TimeSpan.FromMilliseconds(-1));

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                userId,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(otp);

        // Act
        var result = await _sut.VerifyAsync(
            userId,
            "123456",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.Expired);

        _otpRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _unitOfWork.Verify(x =>
            x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task VerifyAsync_ShouldReturnAlreadyVerified_WhenOtpIsAlreadyVerified()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var otp = CreateOtp(
            userId,
            verified: true);

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                userId,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(otp);

        // Act
        var result = await _sut.VerifyAsync(
            userId,
            "123456",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.AlreadyVerified);

        _otpRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _userRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task VerifyAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var otp = CreateOtp(userId);

        _otpRepository
            .Setup(x => x.GetLatestAsync(
                userId,
                OtpPurpose.Registration,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(otp);

        _otpHasher
            .Setup(x => x.Verify(
                "123456",
                otp.CodeHash))
            .Returns(true);

        _userRepository
            .Setup(x => x.GetByIdAsync(
                userId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _sut.VerifyAsync(
            userId,
            "123456",
            OtpPurpose.Registration);

        // Assert
        result.Should().Be(OtpVerificationResult.NotFound);

        _otpRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _userRepository.Verify(x =>
            x.UpdateAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task ResendAsync_ShouldGenerateNewOtp()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _otpGenerator
            .Setup(x => x.Generate())
            .Returns("654321");

        _otpHasher
            .Setup(x => x.Hash("654321"))
            .Returns("hashed-otp");

        _otpRepository
            .Setup(x => x.AddAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.ResendAsync(
            userId,
            OtpPurpose.Registration);

        // Assert
        result.Should().Be("654321");

        _otpGenerator.Verify(x =>
            x.Generate(),
            Times.Once);

        _otpRepository.Verify(x =>
            x.AddAsync(
                It.IsAny<OtpCode>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWork.Verify(x =>
            x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}