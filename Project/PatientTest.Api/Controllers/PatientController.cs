using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientTest.Controllers.Dtos;
using PatientTest.DataAccess;
using PatientTest.Models;

namespace PatientTest.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly AppDbContext _dbContext;

    public PatientController(ILogger<PatientController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Get all patients
    /// </summary>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _dbContext.Patients.Select(x => PatientDto.ToDto(x)).ToArrayAsync());
    }

    /// <summary>
    /// Get patient by id
    /// </summary>
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _dbContext.Patients.FirstAsync(x => x.Id == id));
    }
    
    /// <summary>
    /// Get patient by date
    /// </summary>
    [HttpGet("GetByDate")]
    public async Task<IActionResult> GetByDateAsync(DateTimeOffset dateTime)
    {
        return Ok(await _dbContext.Patients
            .Where(x => x.Birthdate.Date == dateTime.Date)
            .Select(x => PatientDto.ToDto(x)).ToArrayAsync());
    }

    /// <summary>
    /// Add a patient
    /// </summary>
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync(AddPatientDto dto)
    {
        // if (string.IsNullOrWhiteSpace(dto.Name))
        //     return BadRequest("Name is not valid");
        //
        // if (await _dbContext.Patients.AnyAsync(x => x.Name.Family == dto.Name))
        //     return BadRequest("Email is already exists");

        await _dbContext.AddAsync(AddPatientDto.ToModel(dto));
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Edit a patient
    /// </summary>
    [HttpPost("Edit")]
    public async Task<IActionResult> EditAsync(EditPatientDto dto)
    {
        var obj = await _dbContext.Patients.FirstAsync(x => x.Id == dto.Id);
        dto.Edit(obj);
        await _dbContext.SaveChangesAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Remove a patient
    /// </summary>
    [HttpDelete("Remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var obj = await _dbContext.Patients.FirstAsync(x => x.Id == id);
        _dbContext.Patients.Remove(obj);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}