using APIProject.Common;
using APIProject.Models;
using APIProject.Common;
using APIProject.Models;
using Microsoft.Data.SqlClient;

namespace APIProject.Repository
{
    public class EmployeeRepository
    {
        private readonly Common.Common _common;

        public EmployeeRepository(Common.Common common)
        {
            _common = common;
        }

        // GET: All Employees
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT EmployeeId, EmployeeName, Email, Salary, DepartmentId
                FROM Employees
                ORDER BY EmployeeId";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeId = reader.GetInt32(0),
                    EmployeeName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Salary = reader.GetDecimal(3),
                    DepartmentId = reader.GetInt32(4)
                });
            }

            return employees;
        }

        // GET: Employee By ID
        public Employee? GetById(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT EmployeeId, EmployeeName, Email, Salary, DepartmentId
                FROM Employees
                WHERE EmployeeId = @EmployeeId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeId", id);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Employee
                {
                    EmployeeId = reader.GetInt32(0),
                    EmployeeName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Salary = reader.GetDecimal(3),
                    DepartmentId = reader.GetInt32(4)
                };
            }

            return null;
        }

        // INSERT: Employee
        public int Create(Employee employee)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                INSERT INTO Employees
                    (EmployeeName, Email, Salary, DepartmentId)
                VALUES
                    (@EmployeeName, @Email, @Salary, @DepartmentId)";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

            return cmd.ExecuteNonQuery();
        }

        // UPDATE: Employee
        public int Update(int id, Employee employee)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                UPDATE Employees
                SET EmployeeName = @EmployeeName,
                    Email = @Email,
                    Salary = @Salary,
                    DepartmentId = @DepartmentId
                WHERE EmployeeId = @EmployeeId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            cmd.Parameters.AddWithValue("@EmployeeId", id);

            return cmd.ExecuteNonQuery();
        }

        // DELETE: Employee
        public int Delete(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                DELETE FROM Employees
                WHERE EmployeeId = @EmployeeId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeId", id);

            return cmd.ExecuteNonQuery();
        }
    }
}