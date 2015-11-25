#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: IThirdPartyService.cs
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
using System.Collections.Generic;


#endregion
using NZBDash.Api.Models;
using NZBDash.Common.Models.Api;
using NZBDash.Core.Model;

namespace NZBDash.ThirdParty.Api.Interfaces
{
    public interface IThirdPartyService
    {
        void GetCouchPotatoMovies(string uri, string api);
        PlexServers GetPlexServers(string uri);
        List<SonarrSeries> GetSonarrSeries(string uri, string api);
        SonarrSystemStatus GetSonarrSystemStatus(string uri, string api);
        CouchPotatoStatus GetCouchPotatoStatus(string uri, string api);
        NzbGetHistory GetNzbGetHistory(string url, string username, string password);
        NzbGetList GetNzbGetList(string url, string username, string password);
        NzbGetStatus GetNzbGetStatus(string url, string username, string password);
        SabNzbHistory GetSabNzbHistory(string url, string apiKey);
        SabNzbQueue GetSanNzbQueue(string url, string apiKey);
    }
}