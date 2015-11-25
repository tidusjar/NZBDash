#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrController.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Common.Mapping;
using NZBDash.Common.Models.ViewModels.Sonarr;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;

using UrlHelper = NZBDash.UI.Helpers.UrlHelper;
using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers.Application
{
    public class SonarrController : BaseController
    {
        private IThirdPartyService ApiService { get; set; }
        private ISettingsService<SonarrSettingsViewModelDto> SettingsService { get; set; }

        public SonarrController(IThirdPartyService apiService, ISettingsService<SonarrSettingsViewModelDto> settingsService)
            : base(typeof(SonarrController))
        {
            ApiService = apiService;
            SettingsService = settingsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSeries()
        {
            var config = SettingsService.GetSettings();
			var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();

			var series = ApiService.GetSonarrSeries(formattedUri, config.ApiKey);
            var viewModel = new List<SonarrSeriesViewModel>();

            foreach (var s in series)
            {
                var seriesViewModel = new SonarrSeriesViewModel();
                var mappedResult = (SonarrSeriesViewModel)seriesViewModel
                    .InjectFrom(new JsonSerializerTargetMapper<SonarrSeriesViewModel>(), s);

                var images = s.images.Select(x => x.url);
                if (images.Any())
                {
                    mappedResult.ImageUrls = new List<string>(images);
                }
                viewModel.Add(mappedResult);
            }

            return PartialView("SeriesList", viewModel);
        }
    }
}