namespace NZBDash.Core.Interfaces
{
    public interface ISettingsService<T>
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns></returns>
        T GetSettings();

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        bool SaveSettings(T model);
    }
}
