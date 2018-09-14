using System;
using System.Data;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            
            SqlConnection con = new SqlConnection(@"Data Source=WAP-SQL2-002; Initial Catalog=DblEic04; Integrated Security=False;User ID=usrdbleic04#02;Password=steffi572;Connect Timeout=15;Encrypt=false;TrustServerCertificate=False");
            String str = "";
            SqlCommand cmd = new SqlCommand(str,con);
            try
            {
                con.Open();
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            Console.WriteLine("db created");


            execNonQuery("IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblMitarbeiter') DROP TABLE tblMitarbeiter");
            execNonQuery("CREATE TABLE tblMitarbeiter" +
                "(AbteilungId INTEGER," +
                "Name CHAR(50))");
            Console.WriteLine("table created");

            execNonQuery("INSERT INTO tblMitarbeiter(AbteilungId, Name)" +
                "VALUES (1, 'Peter')");
            Console.WriteLine("table filled");

            execNonQuery("INSERT INTO tblMitarbeiter(AbteilungId, Name)" +
               "VALUES (2, 'Hans')");
            Console.WriteLine("table filled");

            execNonQuery("INSERT INTO tblMitarbeiter(AbteilungId, Name)" +
               "VALUES (3, 'Horst')");
            Console.WriteLine("table filled");


            str = "SELECT * FROM tblMitarbeiter";
            cmd = new SqlCommand(str, con);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, "tblMitarbeiter");
            sda.Fill(ds, "tblAbteilung");




            foreach (DataRow dr in ds.Tables["tblMitarbeiter"].Rows)
            {
                Console.WriteLine("{0}\t{1}", dr["AbteilungId"], dr["Name"]);
            }


            Console.Read();
        }

        static private void execNonQuery(String str)
        {
            SqlConnection con = new SqlConnection(@"Data Source=WAP-SQL2-002; Initial Catalog=DblEic04; Integrated Security=False;User ID=usrdbleic04#02;Password=steffi572;Connect Timeout=15;Encrypt=false;TrustServerCertificate=False");
            SqlCommand cmd = new SqlCommand(str, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
