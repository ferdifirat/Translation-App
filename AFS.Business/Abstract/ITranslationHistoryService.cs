using AFS.Core.Response;
using AFS.Entity.Dtos;

namespace AFS.Business.Abstract
{
    public interface ITranslationHistoryService
    {
        ResultStatus Add(TranslationHistoryDto translationHistoryDto);
    }
}
