using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ServerLibrary.Repositores.Contracts;
using System.Reflection.Metadata;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]

public class GenericController<T> : ControllerBase where T : class
{
    // Injected IGeneric Repository
    private readonly IGenericRepositoryInterface<T> _genericReposiroty;
    public GenericController(IGenericRepositoryInterface<T> genericReposiroty) 
    {
        _genericReposiroty = genericReposiroty;
    }


    // CRUD Operation
    [HttpGet]
    public async Task<ActionResult<List<T>>> GetAll()
    {
        var result = await _genericReposiroty.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultResponse<T>>> GetById(int id)
    {
        if (id <= 0) return BadRequest(ResultResponse<T>.Failure("Invalid data provided"));
        var result = await _genericReposiroty.GetById(id);
        return Ok(result);

    }

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> Create([FromBody] T model)
    {
        if(model is null) return BadRequestMessegae();
        var result = await _genericReposiroty.Create(model);
        return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralResponse>> Update([FromBody] T model)
    {
        if (model is null) return BadRequestMessegae();
        var result = await _genericReposiroty.Update(model);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<GeneralResponse>> DeleteById(int id)
    {
        if (id <= 0) return BadRequestMessegae();
        var result = await _genericReposiroty.DeleteById(id);
        return Ok(result);
    }


    // reusable propety and methode
    private ActionResult<GeneralResponse> 
        BadRequestMessegae() => BadRequest(new GeneralResponse(false, "Invalid data provided"));
}
