#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrSettingsService.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//  
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.Core.SettingsService
{
    public class SonarrSettingsService : ISettingsService<SonarrSettingsViewModelDto>
    {
        private ISqlRepository<SonarrSettings> Repo { get; set; }
		public SonarrSettingsService(ISqlRepository<SonarrSettings> repo)
        {
            Repo = repo;
        }

        public SonarrSettingsViewModelDto GetSettings()
        {
            var result = Repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new SonarrSettingsViewModelDto();
            }

            return new SonarrSettingsViewModelDto
            {
                ApiKey = setting.ApiKey,
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Port = setting.Port,
                ShowOnDashboard = setting.ShowOnDashboard
            };
        }

        public bool SaveSettings(SonarrSettingsViewModelDto model)
        {
			var entity = Repo.Get(model.Id);

            if (entity == null)
            {
                var newEntity = new SonarrSettings
                {
                    Port = model.Port,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Id = model.Id,
                    ApiKey = model.ApiKey,
                    ShowOnDashboard = model.ShowOnDashboard
                };

                var insertResult = Repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.ApiKey = model.ApiKey;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;

			var result = Repo.Update(entity);

            return result;
        }
    }
}
