using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IntegrifyLibrary.Controllers;
public class LoanController : BaseController<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>
{
    private readonly ILoanService _loanService;
    public LoanController(ILoanService loanService) : base(loanService)
    {
        _loanService = loanService;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override ActionResult<GetLoanDto> CreateOne([FromBody] CreateLoanDto dto)
    {
        var createdObject = _loanService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }
}
