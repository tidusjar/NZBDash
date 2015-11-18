﻿using System;
using System.Linq;

using NZBDash.Common;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Interfaces;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class NzbGetSettingsService : ISettingsService<NzbGetSettingsDto>
    {
        private IRepository<NzbGetSettings> Repo { get; set; }
        public NzbGetSettingsService(IRepository<NzbGetSettings> repo)
            : this(new NLogLogger(typeof(NzbGetSettingsService)))
        {
            Repo = repo;
        }

        public NzbGetSettingsService(ILogger logger)
        {
            _logger = logger;
        }

        private ILogger _logger { get; set; }

        public NzbGetSettingsDto GetSettings()
        {
            _logger.Trace("Started NzbGetRepository");
            try
            {
                _logger.Trace("Getting all items from NzbGetRepository");
                var result = Repo.GetAll();
                var setting = result.FirstOrDefault();
                if (setting == null)
                {
                    _logger.Trace("There are no items returned from NzbGetRepository. Returning new empty DTO");
                    return new NzbGetSettingsDto();
                }

                _logger.Trace("Creating dto from the results from NzbGetRepository");
                var model = new NzbGetSettingsDto();
                model.InjectFrom(setting);

                return model;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                throw new Exception(e.Message,e);
            }
        }

        public bool SaveSettings(NzbGetSettingsDto model)
        {
            _logger.Trace("Started NzbGetRepository");

            _logger.Trace(string.Format("Looking for id {0} in the NzbGetRepository", model.Id));
            var entity = Repo.Find(model.Id);

            if (entity == null)
            {
                _logger.Trace("Our entity is null so we are going to insert one");
                var newEntity = new NzbGetSettings();
                newEntity.InjectFrom(model);

                _logger.Trace("Inserting now");
                var insertResult = Repo.Insert(newEntity);

                _logger.Trace(string.Format("Our insert was {0}", insertResult != null));
                return insertResult != null;
            }

            _logger.Trace("We found an entity so we are going to modify the existing one");
            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Password = model.Password;
            entity.Port = model.Port;
            entity.Username = model.Username;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            _logger.Trace("Updating modified record");
            var result = Repo.Modify(entity);

            _logger.Trace(string.Format("Our modify was {0}", result == 1));
            return result == 1;
        }
    }
}
