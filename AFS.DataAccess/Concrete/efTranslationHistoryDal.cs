using AFS.Core.DataAccess.EntityFramework;
using AFS.DataAccess.Abstract;
using AFS.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AFS.DataAccess.Concrete
{
    public class efTranslationHistoryDal : efRepositoryBase<TranslationHistory>, ITranslationHistoryDal
    {
        public efTranslationHistoryDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
