namespace PersonalRecord.Services.Interfaces
{
    using PersonalRecord.Infrastructure;

    public interface ISettingsService
    {
        Setting GetSettings();

        void UpdateSettings(Setting newSetting);
    }
}