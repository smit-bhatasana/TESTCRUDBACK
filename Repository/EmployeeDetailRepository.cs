using APIProject.Common;
using APIProject.Models;
using APIProject.Common;
using APIProject.Models;
using Microsoft.Data.SqlClient;

namespace APIProject.Repository
{
    public class EmployeeDetailRepository
    {
        private readonly Common.Common _common;

        public EmployeeDetailRepository(Common.Common common)
        {
            _common = common;
        }

        // GET: All Employee Details
        public List<EmployeeDetail> GetAll()
        {
            List<EmployeeDetail> details = new List<EmployeeDetail>();

            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT EmployeeDetailId, EmployeeId, Phone, Address, JoiningDate
                FROM EmployeeDetails
                ORDER BY EmployeeDetailId";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                details.Add(new EmployeeDetail
                {
                    EmployeeDetailId = reader.GetInt32(0),
                    EmployeeId = reader.GetInt32(1),
                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                    JoiningDate = reader.GetDateTime(4)
                });
            }

            return details;
        }

        // GET: Employee Details By ID
        public EmployeeDetail? GetById(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                SELECT EmployeeDetailId, EmployeeId, Phone, Address, JoiningDate
                FROM EmployeeDetails
                WHERE EmployeeDetailId = @EmployeeDetailId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeDetailId", id);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new EmployeeDetail
                {
                    EmployeeDetailId = reader.GetInt32(0),
                    EmployeeId = reader.GetInt32(1),
                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                    JoiningDate = reader.GetDateTime(4)
                };
            }

            return null;
        }

        // INSERT: Employee Details
        public int Create(EmployeeDetail detail)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                INSERT INTO EmployeeDetails
                    (EmployeeId, Phone, Address, JoiningDate)
                VALUES
                    (@EmployeeId, @Phone, @Address, @JoiningDate)";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeId", detail.EmployeeId);
            cmd.Parameters.AddWithValue("@Phone", (object?)detail.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)detail.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@JoiningDate", detail.JoiningDate.Date);

            return cmd.ExecuteNonQuery();
        }

        // UPDATE: Employee Details
        public int Update(int id, EmployeeDetail detail)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                UPDATE EmployeeDetails
                SET EmployeeId = @EmployeeId,
                    Phone = @Phone,
                    Address = @Address,
                    JoiningDate = @JoiningDate
                WHERE EmployeeDetailId = @EmployeeDetailId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeId", detail.EmployeeId);
            cmd.Parameters.AddWithValue("@Phone", (object?)detail.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)detail.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@JoiningDate", detail.JoiningDate.Date);
            cmd.Parameters.AddWithValue("@EmployeeDetailId", id);

            return cmd.ExecuteNonQuery();
        }

        // DELETE: Employee Details
        public int Delete(int id)
        {
            using SqlConnection con = _common.GetConnection();
            con.Open();

            string query = @"
                DELETE FROM EmployeeDetails
                WHERE EmployeeDetailId = @EmployeeDetailId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@EmployeeDetailId", id);

            return cmd.ExecuteNonQuery();
        }
    }
}