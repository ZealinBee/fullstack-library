using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IntegrifyLibrary.Controllers;
public class LoanController : BaseController<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>
{
    private readonly ILoanService _loanService;
    public LoanController(ILoanService loanService) : base(loanService)
    {
        _loanService = loanService;
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public override async Task<ActionResult<List<GetLoanDto>>> GetAll([FromQuery] QueryOptions queryOptions)
    {
        var result = await _loanService.GetAll(queryOptions);
        return Ok(result);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override async Task<ActionResult<GetLoanDto>> CreateOne([FromBody] CreateLoanDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var createdObject = await _loanService.CreateLoan(dto, userId);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [Authorize(Roles = "User")]
    [HttpGet("own-loans")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult<List<GetLoanDto>>> GetOwnLoans()
    {
        var userIdClaim = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _loanService.GetOwnLoans(userIdClaim);
        return Ok(result);
    }

}
