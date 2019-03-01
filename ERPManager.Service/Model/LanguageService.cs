using ERPManager.Service.Abstract;
using System.Collections.Generic;
using ERPManager.Entities.Model.Language;
using ERPManager.DataAccess.Abstract;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ERPManager.Service.Model
{
    public class LanguageService : ILanguageService
    {

        private IHttpContextAccessor  _httpContext { get; set; }
        private ICacheService _cacheService { get; set; }
        private IRepository _languageRepository;
        private string allLanguagesKey;
        public LanguageService(ICacheService cacheService, IHttpContextAccessor httpContext, IRepository languageRepository)
        {
            _cacheService = cacheService;
            _httpContext = httpContext;
            _languageRepository = languageRepository;

            allLanguagesKey = "AllLanguages";

        }


        public Language GetById(string id)
        {
            return _languageRepository.Single<Language>(k => k.Id == id);
        }

        public void Insert(Language language)
        {
            _languageRepository.Add<Language>(language);
        }

        public void Update(Language language)
        {
            _languageRepository.Single<Language>(k=> k.Id == language.Id);

        }
        public List<Language> GetAll()
        {
            List <Language> allLanguages = (List<Language>)_cacheService.GetFromCache(allLanguagesKey);
            if(allLanguages == null)
            {
                var languages = _languageRepository.All <Language> ();
                _cacheService.InsertToCache(allLanguagesKey, languages);
            }

            return allLanguages;
        }

        public void InsertLocaleString(LanguageItem item)
        {
            _languageRepository.Add<LanguageItem>(item);
        }

        public List<LanguageItem> GetAllLocaleStrings(string languageId)
        {
            return _languageRepository.Where<LanguageItem>(k => k.LanguageId == languageId).ToList();
        }

        public string GetLocaleString( string key)
        {
            string languageId = "tr-TR";
            var languageCookie =  _httpContext.HttpContext.Request.Cookies["culture"];
            if(languageCookie != null)
            {
                languageId = languageCookie;
            }

            var localeData = (List<LanguageItem>)_cacheService.GetFromCache(languageId);
            if (localeData == null)
            {
                localeData = new List<LanguageItem>();
                var localeStrings = _languageRepository.Where<LanguageItem>(k => k.LanguageId == languageId).ToList();
                localeData = localeStrings;
                _cacheService.InsertToCache(languageId, localeStrings);
            }


            var item = localeData.Where(k=> k.Key == key).FirstOrDefault();
            if(item == null)
            {
                return key;
            }
            else
            {
                return item.Value;
            }
        }

        public string GetLocaleString(string key, object[] parameters)
        {
            string languageId = "tr-TR";
            var languageCookie = _httpContext.HttpContext.Request.Cookies["culture"];
            if (languageCookie != null)
            {
                languageId = languageCookie;
            }

            var localeData = (List<LanguageItem>)_cacheService.GetFromCache(languageId);
            if (localeData == null)
            {
                localeData = new List<LanguageItem>();
                var localeStrings = _languageRepository.Where<LanguageItem>(k => k.LanguageId == languageId).ToList();
                localeData = localeStrings;
                _cacheService.InsertToCache(languageId, localeStrings);
            }


            var item = localeData.Where(k => k.Key == key).FirstOrDefault();
            if (item == null)
            {
                return key;
            }
            else
            {
                 return string.Format(item.Value, parameters);
            }
        }
    }
}
