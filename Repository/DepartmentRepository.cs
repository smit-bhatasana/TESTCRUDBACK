using APIProject.Common;
using APIProject.Models;
using APIProject.Common;
using APIProject.Models;
using Microsoft.Data.SqlClient;

namespace APIProject.Repository
{
    public class DepartmentRepository
    {
        private readonly Common.Common _common;

        public DepartmentRepository(Common.Common common)
        {
            _common = common;
        }

        // GET: All Departments
        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();

            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT DepartmentId, DepartmentName, Description
                FROM Departments
                ORDER BY DepartmentId";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                departments.Add(new Department
                {
                    DepartmentId = reader.GetInt32(0),
                    DepartmentName = reader.GetString(1),
                    Description = reader.IsDBNull(2)
                        ? null
                        : reader.GetString(2)
                });
            }

            return departments;
        }

        // GET: Department By ID
        public Department? GetById(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT DepartmentId, DepartmentName, Description
                FROM Departments
                WHERE DepartmentId = @DepartmentId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentId", id);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Department
                {
                    DepartmentId = reader.GetInt32(0),
                    DepartmentName = reader.GetString(1),
                    Description = reader.IsDBNull(2)
                        ? null
                        : reader.GetString(2)
                };
            }

            return null;
        }

        // INSERT: Department
        public int Create(Department department)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                INSERT INTO Departments (DepartmentName, Description)
                VALUES (@DepartmentName, @Description)";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@DepartmentName", department.DepartmentName);

            cmd.Parameters.AddWithValue(
                "@Description", (object?)department.Description ?? DBNull.Value);

            return cmd.ExecuteNonQuery();
        }

        // UPDATE: Department
        public int Update(int id, Department department)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                UPDATE Departments
                SET DepartmentName = @DepartmentName,
                    Description = @Description
                WHERE DepartmentId = @DepartmentId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@DepartmentName", department.DepartmentName);

            cmd.Parameters.AddWithValue(
                "@Description", (object?)department.Description ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@DepartmentId", id);

            return cmd.ExecuteNonQuery();
        }

        // DELETE: Department
        public int Delete(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                DELETE FROM Departments
                WHERE DepartmentId = @DepartmentId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentId", id);

            return cmd.ExecuteNonQuery();
        }
    }
}