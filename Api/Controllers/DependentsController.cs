﻿using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Dependent;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _dependentService.GetDependentByIdAsync(id);

        if (dependent == null)
            return NotFound();

        return new ApiResponse<GetDependentDto> { Success = true, Data = dependent };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = (await _dependentService.GetAllDependentsAsync()).ToList();

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };

        return result;
    }
}
