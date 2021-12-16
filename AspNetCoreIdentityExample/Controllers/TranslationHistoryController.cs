using AFS.Business.Abstract;
using AFS.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityExample.Controllers
{
    public class TranslationHistoryController : Controller
    {
        ITranslationHistoryService _translationHistoryService;
        public TranslationHistoryController(ITranslationHistoryService translationHistoryService)
        {
            _translationHistoryService = translationHistoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(TranslationHistoryDto translationHistoryDto)
        {
            var resp = _translationHistoryService.Add(translationHistoryDto);
            return Json(resp);
        }
    }
}