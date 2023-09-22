using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;
public class LoanRepo : BaseRepo<Loan>, ILoanRepo
{
    public LoanRepo(DatabaseContext context) : base(context)
    {

    }

    public override async Task<List<Loan>> GetAll(QueryOptions queryOptions)
    {
        return _dbSet
            .Include(loan => loan.LoanDetails)
            .ToList();
    }

    public override async Task<Loan> GetOne(Guid id)
    {
        var entity = _dbSet.Include(loan => loan.LoanDetails).FirstOrDefault(l => l.LoanId == id);
        if (entity is null)
        {
            throw new KeyNotFoundException("Id is not found");
        }

        return entity;
    }
}