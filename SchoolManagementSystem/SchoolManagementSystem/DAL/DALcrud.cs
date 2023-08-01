using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Data;
using System.Diagnostics.Metrics;

namespace SchoolManagementSystem.DAL
{
    public class DALcrud
    {
        public DALcrud()
        {

        }
        public void InsertGuardian(Guardian guardian)
        {
            ApplicationDbContext _odm = new ApplicationDbContext();

            try
            {
                _odm.AddParameter(new OracleParameter("GaurdianName", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.GuardianName });
                // _odm.AddParameter(new OracleParameter("GaurdianId", OracleDbType.Int32, ParameterDirection.InputOutput) { Value = guardian.GuardianId });
                _odm.AddParameter(new OracleParameter("PhoneNumber", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.PhoneNumber });
                _odm.AddParameter(new OracleParameter("Relation", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.Relation });
                _odm.AddParameter(new OracleParameter("StudentName", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.StudentName });
                _odm.AddParameter(new OracleParameter("StudentId", OracleDbType.Int32, ParameterDirection.InputOutput) { Value = guardian.StudentId });

                _odm.CallStoredProcedure_insert_gaurdian("insert_gaurdian");

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public void updateGuardian(Guardian guardian)
        {
            ApplicationDbContext _odm = new ApplicationDbContext();

            try
            {
                _odm.AddParameter(new OracleParameter("StudentId", OracleDbType.Int32, ParameterDirection.InputOutput) { Value = guardian.StudentId });
                _odm.AddParameter(new OracleParameter("GaurdianName", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.GuardianName });
                _odm.AddParameter(new OracleParameter("PhoneNumber", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.PhoneNumber });
                _odm.AddParameter(new OracleParameter("Relation", OracleDbType.Varchar2, ParameterDirection.InputOutput) { Value = guardian.Relation });

                _odm.CallStoredProcedure_update_gaurdian("update_guardian");

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public List<Guardian> GetAllGuardiansFromDatabase()
        {
            ApplicationDbContext _odm = new ApplicationDbContext();
            List<Guardian> guardians = new List<Guardian>();

            // Assuming you have an instance of ApplicationDbContext named "dbContext"
            string storedProcedureName = "get_all_guardians";
            DataTable dt = _odm.CallStoredProcedure_getGaurdian(storedProcedureName);

            foreach (DataRow row in dt.Rows)
            {
                Guardian guardian = new Guardian
                {
                    GuardianId = Convert.ToInt32(row["GaurdianId"]),
                    GuardianName = row["GaurdianName"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Relation = row["Relation"].ToString(),
                    StudentName = row["StudentName"].ToString(),
                    StudentId = Convert.ToInt32(row["StudentId"])
                };

                guardians.Add(guardian);
            }

            return guardians;
        }

       

        public List<StudentResults> GetResultsFromDatabase()
        {
            ApplicationDbContext _odm = new ApplicationDbContext();
            List<StudentResults> results = new List<StudentResults>();

            // Assuming you have an instance of ApplicationDbContext named "dbContext"
            string storedProcedureName = "get_all_results";
            DataTable dt = _odm.CallStoredProcedure_getResults(storedProcedureName);

            foreach (DataRow row in dt.Rows)
            {
                StudentResults result = new StudentResults
                {
                    StudentId = Convert.ToInt32(row["StudentId"]),
                    StudentName = row["StudentName"].ToString(),
                    Bangla = Convert.ToInt32(row["Bangla"]),
                    English = Convert.ToInt32(row["English"]),
                    Math = Convert.ToInt32(row["Math"]),
                    Science = Convert.ToInt32(row["Science"]),
                    Average = Convert.ToInt32(row["Average"]),
                    Status = Convert.ToInt32(row["Status"])
                };

                results.Add(result);
            }

            return results;
        }

        public void DeleteGuardian(Guardian guardian)
        {
            ApplicationDbContext _odm = new ApplicationDbContext();

            try
            {
                _odm.AddParameter(new OracleParameter("GaurdianId", OracleDbType.Int32, ParameterDirection.InputOutput) { Value = guardian.GuardianId });
                _odm.DeleteGuardian("delete_guardian");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
