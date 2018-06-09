using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CustomerDao
    {

        public CustomerDao(String connectionString)
        {
            this._connectionString = connectionString;
        }

        private String _connectionString;

        public int Customers { get; private set; }

        public List<Customer> LoadCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Customers";
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Customer c = new Customer()
                            {
                                CustomerId = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Email = dr.GetString(3)
                            };

                            customers.Add(c);
                        }
                    }

                }

            }

            return customers;
        }

        public virtual IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }


        public virtual IDbCommand CreateCommand(IDbConnection con, String sql)
        {
            IDbCommand cmd = con.CreateCommand();

            return cmd;
        }

    }
}
