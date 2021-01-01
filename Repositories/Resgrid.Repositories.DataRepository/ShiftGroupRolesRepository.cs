﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Resgrid.Framework;
using Resgrid.Model;
using Resgrid.Model.Repositories;
using Resgrid.Model.Repositories.Connection;
using Resgrid.Model.Repositories.Queries;
using Resgrid.Repositories.DataRepository.Configs;
using Resgrid.Repositories.DataRepository.Queries.Shifts;

namespace Resgrid.Repositories.DataRepository
{
	public class ShiftGroupRolesRepository : RepositoryBase<ShiftGroupRole>, IShiftGroupRolesRepository
	{
		private readonly IConnectionProvider _connectionProvider;
		private readonly SqlConfiguration _sqlConfiguration;
		private readonly IQueryFactory _queryFactory;
		private readonly IUnitOfWork _unitOfWork;

		public ShiftGroupRolesRepository(IConnectionProvider connectionProvider, SqlConfiguration sqlConfiguration, IUnitOfWork unitOfWork, IQueryFactory queryFactory)
			: base(connectionProvider, sqlConfiguration, unitOfWork, queryFactory)
		{
			_connectionProvider = connectionProvider;
			_sqlConfiguration = sqlConfiguration;
			_queryFactory = queryFactory;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<ShiftGroupRole>> GetShiftGroupRolesByGroupIdAsync(int shiftGroupId)
		{
			try
			{
				var selectFunction = new Func<DbConnection, Task<IEnumerable<ShiftGroupRole>>>(async x =>
				{
					var dynamicParameters = new DynamicParameters();
					dynamicParameters.Add("ShiftGroupId", shiftGroupId);

					var query = _queryFactory.GetQuery<SelectShiftGroupRolesByGroupIdQuery>();

					return await x.QueryAsync<ShiftGroupRole, PersonnelRole, ShiftGroupRole>(sql: query,
						param: dynamicParameters,
						transaction: _unitOfWork.Transaction,
						map: (sgr, pr) => { sgr.Role = pr; return sgr; },
						splitOn: "PersonnelRoleId");
				});

				DbConnection conn = null;
				if (_unitOfWork?.Connection == null)
				{
					using (conn = _connectionProvider.Create())
					{
						await conn.OpenAsync();

						return await selectFunction(conn);
					}
				}
				else
				{
					conn = _unitOfWork.CreateOrGetConnection();

					return await selectFunction(conn);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);

				throw;
			}
		}
	}
}