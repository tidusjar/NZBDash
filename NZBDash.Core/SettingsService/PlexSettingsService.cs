#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: PlexSettingsService.cs
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
    public class PlexSettingsService : ISettingsService<PlexSettingsDto>
    {
        private ISqlRepository<PlexSettings> Repo { get; set; }
        public PlexSettingsService(ISqlRepository<PlexSettings> repo)
        {
            Repo = repo;
        }

        public PlexSettingsDto GetSettings()
        {
            var result = Repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new PlexSettingsDto();
            }

            var model = new PlexSettingsDto
            {
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Password = setting.Password,
                Port = setting.Port,
                Username = setting.Username,
                ShowOnDashboard = setting.ShowOnDashboard,
            };

            return model;
        }

        public bool SaveSettings(PlexSettingsDto model)
        {
            var entity = Repo.Get(model.Id);

            if (entity == null)
            {
                var newEntity = new PlexSettings
                {
                    Port = model.Port,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Id = model.Id,
                    ShowOnDashboard = model.ShowOnDashboard,
                    Username = model.Username,
                    Password = model.Password
                };

                var insertResult = Repo.Insert(newEntity);
                return insertResult != long.MinValue;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;
            entity.Username = model.Username;
            entity.Password = model.Password;

            var result = Repo.Update(entity);

            return result;
        }
    }
}
