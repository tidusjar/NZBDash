#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: LinksConfigurationService.cs
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

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.Core.Configuration
{
    public class LinksConfigurationService : ILinksConfiguration
    {
        public LinksConfigurationService(ISqlRepository<LinksConfiguration> repo)
        {
            Repo = repo;
        }

        private ISqlRepository<LinksConfiguration> Repo { get; set; }

        public IEnumerable<LinksConfigurationDto> GetLinks()
        {
            var reuslt = Repo.GetAll();
            var dto = reuslt.Select(r => new LinksConfigurationDto
            {
                LinkEndpoint = r.LinkEndpoint,
                Id = r.Id,
                LinkName = r.LinkName
            }).ToList();

            return dto;
        }

        public bool UpdateLink(LinksConfigurationDto dto)
        {
            var entity = Repo.Get(dto.Id);
            entity.LinkEndpoint = dto.LinkEndpoint;
            entity.LinkName = dto.LinkName;

            var result = Repo.Update(entity);
            return result;
        }

        public LinksConfigurationDto AddLink(LinksConfigurationDto link)
        {
            var entity = new LinksConfiguration { LinkEndpoint = link.LinkEndpoint, LinkName = link.LinkName };

            var id = Repo.Insert(entity);
            var result = Repo.Get(id);
            return new LinksConfigurationDto{ Id = result.Id, LinkName = result.LinkName, LinkEndpoint = result.LinkEndpoint };
        }

        public void RemoveLink(int id)
        {
            var entity = Repo.Get(id);
            if (entity != null)
            {
                Repo.Delete(entity);
            }
        }
    }
}
