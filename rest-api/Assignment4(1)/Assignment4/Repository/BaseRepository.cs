using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace IMDBAPI.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;
        public BaseRepository(string connection) {
            _connectionString = connection;
        }
        public IEnumerable<T> GetAll(string query)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query);
        }
        public IEnumerable<T> GetAll(string query,object parameter)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query,parameter);
        }
        public T Get(string query,object parameters)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<T>(query,parameters);
        }
        public int ExecuteQuery(string query,object parameters)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.Execute(query,parameters);

        }
        public int ExecuteStoredProcedure(string procedurename,object parameters)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.Execute(procedurename,parameters,commandType:System.Data.CommandType.StoredProcedure);
        }
        public int ExecuteInsertProcedure(string procedurename,object args)
        {
            var connection = new SqlConnection(_connectionString);
            var p = new DynamicParameters();
            p.Add("output", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var properties = args.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var key = prop.Name;
                var value = prop.GetValue(args);

                p.Add(key, value);
            }

            connection.Execute(procedurename, p, commandType: CommandType.StoredProcedure);

            int id = p.Get<int>("output");
            return id;
        }
        public int QueryPerson(string query,object parameters)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.QuerySingle<int>(query, parameters);
        }
    }
}
