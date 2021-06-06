using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_SERVER_LIBRARY
{
    public class Search
    {

        public string znajdzLek(string nazwa)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query = "SELECT nazwa FROM dbo.leki WHERE nazwa= @nazwa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                connection.Open();
                string x = command.ExecuteScalar().ToString();
                connection.Close();
                return x;
            }
        }
        public string szukajZamiennik(string nazwa)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query = "SELECT czy_oryginal FROM dbo.leki WHERE nazwa = @nazwa";
            //string query = "SELECT nazwa FROM dbo.leki WHERE lek_oryginalny = @nazwa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                connection.Open();
                string x = command.ExecuteScalar().ToString();
                if (x == "NIE")
                {
                    query = "SELECT lek_oryginalny FROM dbo.leki WHERE nazwa = @nazwa";
                    command = new SqlCommand(query, connection);
                    command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                    command.Parameters["@nazwa"].Value = nazwa;
                    x = command.ExecuteScalar().ToString();
                    connection.Close();
                    return x;
                }
                else
                {
                    query = "SELECT nazwa FROM dbo.leki WHERE lek_oryginalny = @nazwa";
                    command = new SqlCommand(query, connection);
                    command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                    command.Parameters["@nazwa"].Value = nazwa;
                    x = command.ExecuteScalar().ToString();
                    connection.Close();
                    return x;
                }
            }
        }
        public string porownajCene(string nazwaApteki, string lek1, string lek2)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string magazyn = "dbo.m_" + nazwaApteki;
            string query;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "SELECT id FROM dbo.leki WHERE nazwa = @lek1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@lek1", SqlDbType.VarChar);
                command.Parameters["@lek1"].Value = lek1;
                connection.Open();
                string idlek1 = command.ExecuteScalar().ToString();
                query = "SELECT cena FROM " + magazyn + " WHERE idleku = @idlek1";
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@idlek1", SqlDbType.VarChar);
                command.Parameters["@idlek1"].Value = idlek1;
                string cenaLek1 = command.ExecuteScalar().ToString();

                query = "SELECT id FROM dbo.leki WHERE nazwa = @lek2";
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@lek2", SqlDbType.VarChar);
                command.Parameters["@lek2"].Value = lek2;
                string idlek2 = command.ExecuteScalar().ToString();
                query = "SELECT cena FROM " + magazyn + " WHERE idleku = @idlek2";
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@idlek2", SqlDbType.VarChar);
                command.Parameters["@idlek2"].Value = idlek2;
                string cenaLek2 = command.ExecuteScalar().ToString();
                float cena1 = float.Parse(cenaLek1);
                float cena2 = float.Parse(cenaLek2);
                if (cena1 > cena2)
                {
                    return lek1;
                }
                else if (cena1 == cena2)
                {
                    return "taka sama cena";
                }
                else
                {
                    return lek2;
                }
            }
        }
        public string szukajApteki(string nazwaApteki)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "SELECT ulica FROM dbo.apteki WHERE nazwa = @nazwaApteki";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwaApteki", SqlDbType.VarChar);
                command.Parameters["@nazwaApteki"].Value = nazwaApteki;
                connection.Open();
                string ulica = command.ExecuteScalar().ToString();
                query = "SELECT miasto FROM dbo.apteki WHERE nazwa = @nazwaApteki";
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwaApteki", SqlDbType.VarChar);
                command.Parameters["@nazwaApteki"].Value = nazwaApteki;
                string miasto = command.ExecuteScalar().ToString();
                string wynik = ulica + " , " + miasto;
                return wynik;
            }
        }
    }
}
