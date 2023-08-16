using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;
public class LoanRepo : BaseRepo<Loan>, ILoanRepo
{
    public LoanRepo(DatabaseContext context) : base(context)
    {

    }
}