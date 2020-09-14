using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repository
{
    public abstract class Repository
    {
        private IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection connection
        {
            get { return new SqlConnection(_configuration.GetConnectionString("minhaConexao")); }
        }

        //public void RunCommand()
        //{
        //    try
        //    {
        //        using (IDbConnection con = connection)
        //        {
        //            //con.Execute()
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
