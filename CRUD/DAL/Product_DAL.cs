using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string OP, OPID, OPID1;

        public List<Product> Get_Data()
        {
            List<Product> productsList = new List<Product>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "";
                cmd.Parameters.AddWithValue("@OP", OP);
                cmd.Parameters.AddWithValue("@OPID", OPID);
                cmd.Parameters.AddWithValue("@OPID1", OPID1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    productsList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["Product_Name"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return productsList;
        }
    }
}