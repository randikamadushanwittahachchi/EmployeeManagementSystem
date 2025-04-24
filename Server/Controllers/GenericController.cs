using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericController<T> : ControllerBase where T : class
{
    private readonly IGenericRepositoryInterface<T> _genericReposiroty;
    public GenericController(IGenericRepositoryInterface<T> genericReposiroty) 
    {
        _genericReposiroty = genericReposiroty;
    }

    [HttpGet]
    public async Task<ActionResult<List<T>>> GetAll()
    {
        var result = await _genericReposiroty.GetAll();
        return result.Any() ? Ok(result) : NoContent(); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id)
    {
        if(id <= 0) return BadRequestMessegae();
        var result = await _genericReposiroty.GetById(id);
        return result == null ? NotFound(new GeneralResponse(false, "No recode found with the given ID")) : Ok(result);

    }

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> Create([FromBody] T model)
    {
        if(model is null) return BadRequestMessegae();
        var result = await _genericReposiroty.Create(model);
        return ResultResponseConflict(result);

    }

    [HttpPost("{id}")]
    public async Task<ActionResult<GeneralResponse>> Update([FromBody] T model)
    {
        if (model is null) return BadRequestMessegae();
        var result = await _genericReposiroty.Update(model);
        return ResultResponseNotFound(result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<GeneralResponse>> Delete(int id)
    {
        if (id <= 0) return BadRequestMessegae();
        var result = await _genericReposiroty.DeleteById(id);
        return ResultResponseNotFound(result);
    }


    // reusable propety and methode
    private BadRequestObjectResult BadRequestMessegae() => BadRequest(new GeneralResponse(false, "Invalid data provided"));
    private ActionResult<GeneralResponse> ResultResponseNotFound(GeneralResponse result) => !result.Flag ? NotFound(result) : Ok(result);
    private ActionResult<GeneralResponse> ResultResponseConflict(GeneralResponse result) => Conflict(result);

}
