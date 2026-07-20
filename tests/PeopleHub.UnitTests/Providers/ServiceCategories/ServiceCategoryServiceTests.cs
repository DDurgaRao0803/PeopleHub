using FluentAssertions;
using Moq;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Providers.ServiceCategories;

namespace PeopleHub.UnitTests.Providers;

public class ServiceCategoryServiceTests
{
    private readonly Mock<IServiceCategoryRepository> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task CreateAsync_ShouldCreateCategory_WhenNameIsUnique()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetByNameAsync(
                "Plumbing",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ServiceCategory?)null);

        var service = new ServiceCategoryService(
            _repositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        var result = await service.CreateAsync(
            "Plumbing",
            "Plumbing services");

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Plumbing");
        result.Description.Should().Be("Plumbing services");
        result.IsActive.Should().BeTrue();

        _repositoryMock.Verify(
            r => r.AddAsync(
                It.IsAny<ServiceCategory>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
public async Task CreateAsync_ShouldThrow_WhenCategoryAlreadyExists()
{
    // Arrange
    var existingCategory = new ServiceCategory(
        "Plumbing",
        "Existing category");

    _repositoryMock
        .Setup(r => r.GetByNameAsync(
            "Plumbing",
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(existingCategory);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    var action = () => service.CreateAsync(
        "Plumbing",
        "Duplicate category");

    // Assert
    await action.Should()
        .ThrowAsync<InvalidOperationException>();

    _repositoryMock.Verify(
        r => r.AddAsync(
            It.IsAny<ServiceCategory>(),
            It.IsAny<CancellationToken>()),
        Times.Never);

    _unitOfWorkMock.Verify(
        u => u.SaveChangesAsync(
            It.IsAny<CancellationToken>()),
        Times.Never);
}
[Fact]
public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
{
    // Arrange
    var category = new ServiceCategory(
        "Electrical",
        "Electrical services");

    _repositoryMock
        .Setup(r => r.GetByIdAsync(
            category.Id,
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(category);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    var result = await service.GetByIdAsync(category.Id);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeSameAs(category);

    _repositoryMock.Verify(
        r => r.GetByIdAsync(
            category.Id,
            It.IsAny<CancellationToken>()),
        Times.Once);

    _repositoryMock.VerifyNoOtherCalls();
    _unitOfWorkMock.VerifyNoOtherCalls();
}
[Fact]
public async Task GetByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
{
    // Arrange
    var id = Guid.NewGuid();

    _repositoryMock
        .Setup(r => r.GetByIdAsync(
            id,
            It.IsAny<CancellationToken>()))
        .ReturnsAsync((ServiceCategory?)null);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    var result = await service.GetByIdAsync(id);

    // Assert
    result.Should().BeNull();

    _repositoryMock.Verify(
        r => r.GetByIdAsync(
            id,
            It.IsAny<CancellationToken>()),
        Times.Once);

    _repositoryMock.VerifyNoOtherCalls();
    _unitOfWorkMock.VerifyNoOtherCalls();
}
[Fact]
public async Task UpdateAsync_ShouldUpdateExistingCategory()
{
    // Arrange
    var category = new ServiceCategory(
        "Electrical",
        "Electrical services");

    _repositoryMock
        .Setup(r => r.GetByIdAsync(
            category.Id,
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(category);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    await service.UpdateAsync(
        category.Id,
        "Home Electrical",
        "Residential electrical services",
        false);

    // Assert
    category.Name.Should().Be("Home Electrical");
    category.Description.Should().Be("Residential electrical services");
    category.IsActive.Should().BeFalse();

    _repositoryMock.Verify(
    r => r.GetByIdAsync(
        category.Id,
        It.IsAny<CancellationToken>()),
    Times.Once);

_repositoryMock.Verify(
    r => r.UpdateAsync(
        category,
        It.IsAny<CancellationToken>()),
    Times.Once);

_unitOfWorkMock.Verify(
    u => u.SaveChangesAsync(
        It.IsAny<CancellationToken>()),
    Times.Once);

_repositoryMock.VerifyNoOtherCalls();
_unitOfWorkMock.VerifyNoOtherCalls();

    _unitOfWorkMock.Verify(
        u => u.SaveChangesAsync(
            It.IsAny<CancellationToken>()),
        Times.Once);

    _repositoryMock.VerifyNoOtherCalls();
    _unitOfWorkMock.VerifyNoOtherCalls();
}

[Fact]
public async Task UpdateAsync_ShouldThrow_WhenCategoryDoesNotExist()
{
    // Arrange
    var id = Guid.NewGuid();

    _repositoryMock
        .Setup(r => r.GetByIdAsync(
            id,
            It.IsAny<CancellationToken>()))
        .ReturnsAsync((ServiceCategory?)null);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    var action = () => service.UpdateAsync(
        id,
        "Electrical",
        "Electrical services",
        true);

    // Assert
    await action.Should()
        .ThrowAsync<KeyNotFoundException>();

    _repositoryMock.Verify(
        r => r.GetByIdAsync(
            id,
            It.IsAny<CancellationToken>()),
        Times.Once);

    _repositoryMock.Verify(
        r => r.UpdateAsync(
            It.IsAny<ServiceCategory>(),
            It.IsAny<CancellationToken>()),
        Times.Never);

    _unitOfWorkMock.Verify(
        u => u.SaveChangesAsync(
            It.IsAny<CancellationToken>()),
        Times.Never);

    _repositoryMock.VerifyNoOtherCalls();
    _unitOfWorkMock.VerifyNoOtherCalls();
}

[Fact]
public async Task GetActiveAsync_ShouldReturnActiveCategories()
{
    // Arrange
    var categories = new List<ServiceCategory>
    {
        new("Electrical", "Electrical services"),
        new("Plumbing", "Plumbing services")
    };

    _repositoryMock
        .Setup(r => r.GetActiveAsync(
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(categories);

    var service = new ServiceCategoryService(
        _repositoryMock.Object,
        _unitOfWorkMock.Object);

    // Act
    var result = await service.GetActiveAsync();

    // Assert
    result.Should().NotBeNull();
    result.Should().HaveCount(2);
    result.Should().BeEquivalentTo(categories);

    _repositoryMock.Verify(
        r => r.GetActiveAsync(
            It.IsAny<CancellationToken>()),
        Times.Once);

    _repositoryMock.VerifyNoOtherCalls();
    _unitOfWorkMock.VerifyNoOtherCalls();
}

}