using System.Globalization;

namespace WalloneInstaller.Services
{
    class LanguageService
    {
        public enum Translation
        {
            English,
            Russia
        }
        
        /**
         * Установка локализации
         */
        public static void Set(Translation langCode)
        {
            string lang = langCode switch
            {
                Translation.Russia => "ru-RU",
                Translation.English => "en-US",
                _ => "en-US"
            };

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(lang);
        }

        /**
         * Получение языкового пакета
         */
        public static CultureInfo Get()
        {
            return CultureInfo.DefaultThreadCurrentCulture;
        }
    }
}
