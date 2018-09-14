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
            // verbinden ohne Auswahl um DB zu erzeugen (Server=(localdb)\Projects -> DB ist in SQL Server-Objekt-Explorer sichtbar
            SqlConnection con = new SqlConnection(@"Server=(localdb)\Projects; Integrated Security=true;"); 
            String str = "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MyDatabase') CREATE DATABASE MyDatabase";
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
            Console.WriteLine("db created");



            // Tabelle erzeugen und füllen
            execNonQuery("IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblPerson') DROP TABLE tblPerson");
            execNonQuery("CREATE TABLE tblPerson" +
                "(personId INTEGER PRIMARY KEY," +
                "name CHAR(50), adress CHAR(255))");
            Console.WriteLine("table created");

            execNonQuery("INSERT INTO tblPerson(personId, name, adress) " +
                "VALUES (1, 'Max Mustermann', 'Beispieladresse')");
            Console.WriteLine("table filled");

            // Tabelle auf Konsole ausgeben
            con = new SqlConnection("Server=(localdb)\\Projects; Integrated Security=true; Database=MyDatabase;");
            str = "SELECT * FROM tblPerson";
            cmd = new SqlCommand(str, con);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, "tblPerson");

            foreach (DataRow dr in ds.Tables["tblPerson"].Rows)
                Console.WriteLine("{0}\t{1}", dr["personId"], dr["name"]);

            Console.Read();
        }

        static private void execNonQuery(String str)
        {
            SqlConnection con = new SqlConnection("Server=(localdb)\\Projects; Integrated Security=true; Database=MyDatabase;");
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
