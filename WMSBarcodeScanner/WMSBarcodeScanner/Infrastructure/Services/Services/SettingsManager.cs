using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.IO;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.Services.Services
{
    public class SettingsManager : ISettingsManager
    {
        public string WMSServiceAddress
        {
            get
            {
                string settingsPath = getSettingsPath();
                string wmsServerAddress = string.Empty;

                bool settingsFileExist = File.Exists(settingsPath);

                if (!settingsFileExist)
                    File.Create(settingsPath);
                else
                {
                    Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsPath));
                    if (settings != null)
                        wmsServerAddress = settings.WMSServerAddress;
                }

                return wmsServerAddress;
            }

            set
            {
                string settingsPath = getSettingsPath();
                string wmsServerAddress = value;

                bool settingsFileExist = File.Exists(settingsPath);
                Settings settings;

                if (!settingsFileExist)
                {
                    File.Create(settingsPath);
                    settings = new Settings();
                }
                else
                {
                    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsPath));
                    if (settings == null)
                        settings = new Settings();
                }

                settings.WMSServerAddress = wmsServerAddress;
                try
                {
                    File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings));
                    UserDialogs.Instance.Toast("Zapisano ustawienia", TimeSpan.FromSeconds(3));
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert($"Nie udało się zapisać ustawień. Szczegóły: {e.Message}", "Błąd", "Zamknij");
                }
            }
        }

        private string getSettingsPath()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string settingsFilename = "settings.json";
            string settingsPath = Path.Combine(appDataPath, settingsFilename);

            return settingsPath;
        }
    }
}
