using FerveApp.Api.Abstractions;
using FerveApp.Api.Contracts;
using FerveApp.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FerveApp.Api.Controllers;

[Route("api/brands")]
public sealed class BrandController : ApiController
{
    public BrandController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<ActionResult> BrandList()
    {
        var result = await _sender.Send(new GetAllBrandsQuery());

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<ActionResult> BrandCreate([FromBody] CreateBrandForm request)
    {
        var result = await _sender.Send(new CreateBrandCommand(request.Name));
        return result.IsSuccess ? Created($"/brands/{result.Value!.Brand.Slug}", result.Value) : HandleFailure(result);
    }

    [HttpGet]
    [Route("/{slug}")]
    public async Task<ActionResult> BrandDetail([FromRoute] string slug)
    {
        var result = await _sender.Send(new GetBrandBySlugQuery(slug));
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPut]
    [Route("/{slug}")]
    public async Task<ActionResult> BrandUpdate([FromRoute] string slug, [FromBody] UpdateBrandForm request)
    {
        var result = await _sender.Send(new UpdateBrandCommand(slug, request.Name));
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpDelete]
    [Route("/{slug}")]
    public async Task<ActionResult> BrandDelete([FromRoute] string slug)
    {
        var result = await _sender.Send(new DeleteBrandCommand(slug));
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}