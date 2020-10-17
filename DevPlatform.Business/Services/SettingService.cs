using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Configuration;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Setting service
    /// </summary>
    public partial class SettingService : ISettingService
    {
        #region Fields
        private readonly IRepository<Setting> _settingRepository;
        #endregion

        #region Ctor
        public SettingService(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        protected virtual IDictionary<string, IList<Setting>> GetAllSettingsDictionary()
        {
            var settings = GetAllSettings();

            var dictionary = new Dictionary<string, IList<Setting>>();
            foreach (var s in settings)
            {
                var resourceName = s.Name.ToLowerInvariant();
                var settingForCaching = new Setting
                {
                    Id = s.Id,
                    Name = s.Name,
                    Value = s.Value,
                };
                if (!dictionary.ContainsKey(resourceName))
                {
                    //first setting
                    dictionary.Add(resourceName, new List<Setting>
                        {
                            settingForCaching
                        });
                }
                else
                {
                    dictionary[resourceName].Add(settingForCaching);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        protected virtual void SetSetting(Type type, string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsDictionary();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                setting.Value = valueStr;
                UpdateSetting(setting);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                InsertSetting(setting);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Insert(setting);
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void UpdateSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Update(setting);
        }

        public void DeleteSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Delete(setting);
        }

        public void DeleteSetting<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                var key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            DeleteSettings(settingsToDelete);
        }

        public void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsDictionary();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching == null)
                return;

            //update
            var setting = GetSettingById(settingForCaching.Id);
            DeleteSetting(setting);
        }

        public void DeleteSettings(IList<Setting> settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settingRepository.Delete(settings);
        }

        public IList<Setting> GetAllSettings()
        {
            var query = from s in _settingRepository.Table
                        orderby s.Name
                        select s;

            return query.ToList();
        }

        public Setting GetSetting(string key, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var settings = GetAllSettingsDictionary();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
                return null;

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault();

            return setting != null ? GetSettingById(setting.Id) : null;
        }

        public Setting GetSettingById(int settingId)
        {
            if (settingId == 0)
                return null;

            return _settingRepository.GetById(settingId);
        }

        public T GetSettingByKey<T>(string key, T defaultValue = default, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsDictionary();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
                return defaultValue;

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault();

            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        public string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector) where TSettings : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

            return key;
        }

        public T LoadSetting<T>() where T : ISettings, new()
        {
            return (T)LoadSetting(typeof(T));
        }

        public ISettings LoadSetting(Type type)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(key, loadSharedValueIfNotFound: true);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }

        public void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(prop.PropertyType, key, value);
                else
                    SetSetting(key, string.Empty);
            }

        }

        public void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var key = GetSettingKey(settings, keySelector);
            var value = (TPropType)propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value);
            else
                SetSetting(key, string.Empty);
        }

        public void SetSetting<T>(string key, T value)
        {
            SetSetting(typeof(T), key, value);
        }

        public bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);

            var setting = GetSettingByKey<string>(key);
            return setting != null;
        }

        #endregion
    }
}
