using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.ServiceCategories;
using PeopleHub.Contracts.Providers.ServiceCategories;

namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/service-categories")]
public sealed class ServiceCategoriesController : ControllerBase
{
    private readonly IServiceCategoryService _service;

    public ServiceCategoriesController(IServiceCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var categories = await _service.GetActiveAsync(cancellationToken);

        var response = categories
            .Select(category => new ServiceCategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var category = await _service.GetByIdAsync(id, cancellationToken);

        if (category is null)
        {
            return NotFound();
        }

        var response = new ServiceCategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive);

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateServiceCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _service.CreateAsync(
            request.Name,
            request.Description,
            cancellationToken);

        var response = new ServiceCategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateServiceCategoryRequest request,
        CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(
            id,
            request.Name,
            request.Description,
            request.IsActive,
            cancellationToken);

        return NoContent();
    }
}