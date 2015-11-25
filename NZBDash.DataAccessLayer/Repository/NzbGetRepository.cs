#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: NzbGetRepository.cs
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

using Dapper.Contrib.Extensions;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.DataAccessLayer
{
	public class NzbGetRepository : ISqlRepository<NzbGetSettings>
	{
		private ISqliteConfiguration Config{ get; set; }
		private ILogger Logger {get;set;}
		public NzbGetRepository(ILogger logger, ISqliteConfiguration config)
		{
			Config = config;
			Logger = Logger;
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		public IEnumerable<NzbGetSettings> GetAll()
		{
			using (var db = Config.DbConnection().GetConnection())
			{
				db.Open();
                var result = db.GetAll<NzbGetSettings>();
				return result;
			}
		}

		public NzbGetSettings Get(long id)
		{
			using (var db = Config.DbConnection().GetConnection())
			{
				db.Open();
				var result = db.Get<NzbGetSettings>(id);
				return result;
			}
		}

		public void Delete(NzbGetSettings entity)
		{
			using (var db = Config.DbConnection().GetConnection())
			{
				db.Open();
				db.Delete(entity);
			}
		}

		public bool Update(NzbGetSettings entity)
		{
			using (var db = Config.DbConnection().GetConnection())
			{
				db.Open();
				return db.Update<NzbGetSettings>(entity);
			}
		}

		public long Insert(NzbGetSettings entity)
		{
			using (var cnn = Config.DbConnection().GetConnection())
			{
				cnn.Open();
				return cnn.Insert(entity);
			}
		}
	}
}
