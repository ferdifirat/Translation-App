using AFS.Core.DataAccess.EntityFramework;
using AFS.DataAccess.Abstract;
using AFS.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AFS.DataAccess.Concrete
{
    public class efTranslationHistory : efRepositoryBase<TranslationHistory>, ITranslationHistoryDal
    {
        public efTranslationHistory(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
