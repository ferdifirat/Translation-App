using AFS.Business.Abstract;
using AFS.Business.Concrete;
using AFS.DataAccess.Abstract;
using AFS.DataAccess.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFS.Business.CustomExtensions
{
    public static class StartupExtensions
    {

        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITranslationHistoryService, TranslationHistoryManager>();
            services.AddScoped<ITranslationHistoryDal, efTranslationHistoryDal>();
        }
    }
}
