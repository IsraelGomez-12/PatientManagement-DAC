using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PatientManagement.Infrastructure.Repository
{
	public abstract class RepositoryBase
	{
		private readonly string _connectionString;

		public RepositoryBase(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected async Task<IDbConnection> GetConnectionAsync()
		{
			var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			return connection;
		}
	}
}

