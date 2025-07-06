using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Services.Implementations;
namespace Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ManageUserController : ControllerBase
{
    private readonly ManageUserService _manageUserService;
    public ManageUserController(ManageUserService manageUserService)
    {
        _manageUserService = manageUserService;
    }

    // CRUD Operation 

    [HttpGet]
    public async Task<ActionResult<List<ManageUser>>> GetAll()
    {
        var result = await _manageUserService.GetAll();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ResultResponse<ManageUser>>> GetById(int id) 
    {
        if (id < 0) return NotFound();
        var result = await _manageUserService.GetById(id);
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralResponse>> Update(ManageUser manageUser)
    {
        if (manageUser is null) return BadRequestMessage();
        var result = await _manageUserService.Update(manageUser);
        return Ok(result);

    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<GeneralResponse>> DeleteById(int id)
    {
        if (id <= 0) return BadRequestMessage();
        var result = await _manageUserService.DeleteById(id);
        return Ok(result);
    }

    private ActionResult<GeneralResponse> BadRequestMessage() => BadRequest(new GeneralResponse(false, "Invalid Data Provide"));
}
