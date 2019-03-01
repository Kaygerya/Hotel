using ERPManager.Entities.Model.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ILanguageService
    {
        Language GetById(string id);

        void Insert(Language language);

        void Update(Language language);

        List<Language> GetAll();

        void InsertLocaleString(LanguageItem item);

        List<LanguageItem> GetAllLocaleStrings(string languageId);

        string GetLocaleString(string key);

        string GetLocaleString(string key, object[] parameters);
        

    }
}
