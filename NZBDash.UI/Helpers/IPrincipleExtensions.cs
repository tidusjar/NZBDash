using System;
using System.Security.Principal;

using NZBDash.Common;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;

namespace NZBDash.UI.Helpers
{
    public class PrincipleExtension
    {
        private ISettingsService<SabNzbSettingsDto> SabService { get; set; }
        private ISettingsService<CouchPotatoSettingsDto> CpService { get; set; }
        private ISettingsService<PlexSettingsDto> PlexService { get; set; }
        private ISettingsService<NzbGetSettingsDto> NzbService { get; set; }
        private ISettingsService<SonarrSettingsViewModelDto> SonarrService { get; set; }

        public PrincipleExtension(ISettingsService<SabNzbSettingsDto> sab, ISettingsService<CouchPotatoSettingsDto> cp,
            ISettingsService<SonarrSettingsViewModelDto> sonarr, ISettingsService<PlexSettingsDto> plex, ISettingsService<NzbGetSettingsDto> nzbget)
        {
            SabService = sab;
            CpService = cp;
            SonarrService = sonarr;
            PlexService = plex;
            NzbService = nzbget;
        }
        public bool IsApplicationEnabled(IPrincipal principal, Applications application)
        {
            switch (application)
            {
                case Applications.SabNZB:
                    return SabService.GetSettings().Enabled;
                case Applications.Sickbeard:
                    break;
                case Applications.CouchPotato:
                    return CpService.GetSettings().Enabled;
                case Applications.Kodi:
                    break;
                case Applications.Sonarr:
                    return SonarrService.GetSettings().Enabled;
                case Applications.Plex:
                    return PlexService.GetSettings().Enabled;
                case Applications.NzbGet:
                    return NzbService.GetSettings().Enabled;
                case Applications.Headphones:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("application");
            }
            return false;
        }
    }
}