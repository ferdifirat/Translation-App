using AFS.Business.Abstract;
using AFS.Core.Helpers;
using AFS.Core.Request;
using AFS.Core.Response;
using AFS.DataAccess.Abstract;
using AFS.Entity.Concrete;
using AFS.Entity.Dtos;
using AspNetCoreIdentityExample.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AFS.Business.Concrete
{
    public class TranslationHistoryManager : ITranslationHistoryService
    {
        private readonly ITranslationHistoryDal _translationHistoryDal;
        private readonly HttpRequestHelper _client;
        private readonly string _funTranslationUrl = "https://api.funtranslations.com/translate/yoda";
        private readonly HttpContext _context;
        UserManager<User> _userManager;
        public TranslationHistoryManager(ITranslationHistoryDal translationHistoryDal, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _translationHistoryDal = translationHistoryDal;
            _client = new HttpRequestHelper();
            _userManager = userManager;
            _context = contextAccessor.HttpContext;
        }

        public ResultStatus Add(TranslationHistoryDto translationHistoryDto)
        {
            var customIdentityUser = _userManager.FindByNameAsync(_context.User.Identity.Name).Result;

            try
            {
                var resp = _client.PostAsync<FunTranslationRequestModel, FunTranslationResponseModel>($"{_funTranslationUrl}", new FunTranslationRequestModel()
                {
                    text = translationHistoryDto.Text
                }).Result;

                var translation = new TranslationHistory()
                {
                    CreatedByUser = customIdentityUser,
                    CreateDate = DateTime.UtcNow.TimeOfDay,
                    Text = resp.contents.text,
                    Translated = resp.contents.translated,
                    Translation = resp.contents.translation
                };
                _translationHistoryDal.Add(translation);
                _translationHistoryDal.SaveChanges();

                return new ResultStatus()
                {
                    Result = true,
                    FeedBack = "Successful",
                    Object = translation
                };
            }
            catch (Exception exception)
            {
                return new ResultStatus()
                {
                    Result = false,
                    FeedBack = "Failed",
                    Object = null
                };
            }
        }
    }
}