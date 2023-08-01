using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SchoolManagementSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace SchoolManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        OracleConnection connection;
        OracleCommand command;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate

        }

        public ApplicationDbContext()
        {
            connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=27.147.159.194)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=BIODB_TEST;Password=ssle_testdb;";
            connection = new OracleConnection(connectionString);
            command = new OracleCommand();

        }

        public DbSet<AllUsers> AllUsers { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherAssignments> TeacherAssignments { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<StudentResults> StudentResults { get; set; }


        public static string connectionString = String.Empty;
        

        public DataTable CallStoredProcedure_getGaurdian(string get_all_guardians)
        {
            DataTable dt = new DataTable();
            using (connection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    command.Connection = connection;
                    command.CommandText = get_all_guardians;
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter outParameter = new OracleParameter();
                    outParameter.ParameterName = "guardians_cursor";
                    outParameter.OracleDbType = OracleDbType.RefCursor;
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);


                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        //adapter.SelectCommand = command;
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed) connection.Close();
                }
            }
        }

        public void CallStoredProcedure_insert_gaurdian(string insert_gaurdian)
        {

            using (connection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    command.Connection = connection;
                    command.CommandText = insert_gaurdian;
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable(insert_gaurdian);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        //adapter.SelectCommand = command;
                        adapter.Fill(dt);
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed) connection.Close();
                }

            }
        }

        public void CallStoredProcedure_update_gaurdian(string update_guardian)
        {

            using (connection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    command.Connection = connection;
                    command.CommandText = update_guardian;
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable(update_guardian);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed) connection.Close();
                }

            }
        }

        public void DeleteGuardian(string delete_guardian)
        {

            using (connection)
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                command.Connection = connection;
                command.CommandText = "delete_guardian";
                command.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable(delete_guardian);

                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    //adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }

                // Add the IN parameter for the GuardianId
                /*  OracleParameter guardianIdParameter = new OracleParameter();
                  guardianIdParameter.ParameterName = "GuardianId";
                  guardianIdParameter.OracleDbType = OracleDbType.Int32;
                  guardianIdParameter.Direction = ParameterDirection.Input;
                  guardianIdParameter.Value = guardianId;
                  command.Parameters.Add(guardianIdParameter);
                */

                command.ExecuteNonQuery();
            }
        }

        public DataTable CallStoredProcedure_getResults(string get_all_results)
        {
            DataTable dt = new DataTable();
            using (connection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    command.Connection = connection;
                    command.CommandText = get_all_results;
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter outParameter = new OracleParameter();
                    outParameter.ParameterName = "results_cursor";
                    outParameter.OracleDbType = OracleDbType.RefCursor;
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);


                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        //adapter.SelectCommand = command;
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed) connection.Close();
                }
            }
        }


        public void AddParameter(OracleParameter param)
        {
            command.Parameters.Add(param);
        }

    }

}