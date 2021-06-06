using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_SERVER_LIBRARY
{
    public class DataBase
    {
        public void dodajLek(string nazwa, string opis, string sklad, float cena, string czy_oryginal, string lek_oryginalny)
        {
            
           string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query;
            if (czy_oryginal == "NIE")
            {
                query = "INSERT INTO dbo.leki (nazwa, opis, sklad, cena, czy_oryginal, lek_oryginalny) VALUES (@nazwa, @opis, @sklad, @cena, @czy_oryginal, @lek_oryginalny)";
            }else
            {
                query = "INSERT INTO dbo.leki (nazwa, opis, sklad, cena, czy_oryginal) VALUES (@nazwa, @opis, @sklad, @cena, @czy_oryginal)";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters.Add("@opis", SqlDbType.VarChar);
                command.Parameters.Add("@sklad", SqlDbType.VarChar);
                command.Parameters.Add("@cena", SqlDbType.Float);
                command.Parameters.Add("@czy_oryginal", SqlDbType.VarChar);
                if (czy_oryginal == "NIE")
                {
                    command.Parameters.Add("@lek_oryginalny", SqlDbType.VarChar);
                }
                command.Parameters["@nazwa"].Value = nazwa;
                command.Parameters["@opis"].Value = opis;
                command.Parameters["@sklad"].Value = sklad;
                command.Parameters["@cena"].Value = cena;
                command.Parameters["@czy_oryginal"].Value = czy_oryginal;
                if (czy_oryginal == "NIE")
                {
                    command.Parameters["@lek_oryginalny"].Value = lek_oryginalny;
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void usunLek(string nazwa)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query = "DELETE FROM dbo.leki WHERE nazwa = @nazwa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void dodajApteke(string nazwa, string ulica, string miasto, string telefon)
        {
            string nazwamagazynu = "m_" + nazwa;
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query1 = "CREATE TABLE "+ nazwamagazynu + " (id INT IDENTITY(1,1) PRIMARY KEY NOT NULL, idleku INT FOREIGN KEY(id) REFERENCES leki(id), datawaznosci DATE NOT NULL, cena money NOT NULL, ilosc INT NOT NULL,)";
            string query2 = "INSERT INTO dbo.magazyny (nazwa) VALUES (@nazwamagazynu)";
            string query3 = "SELECT id from dbo.magazyny WHERE nazwa = @nazwamagazynu";
            string query = "INSERT INTO dbo.Apteki (nazwa, magazyn, ulica, miasto, telefon) VALUES (@nazwa, @ulica, @miasto, @telefon)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query1, connection);
                //command.Parameters.Add("@nazwam", SqlDbType.VarChar);
                //command.Parameters["@nazwa"].Value = nazwamagazynu;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.Add("@nazwamagazynu", SqlDbType.VarChar);
                command.Parameters["@nazwamagazynu"].Value = nazwamagazynu;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query3, connection);
                command.Parameters.Add("@nazwamagazynu", SqlDbType.VarChar);
                command.Parameters["@nazwamagazynu"].Value = nazwamagazynu;
                connection.Open();
                int n = (Int32) command.ExecuteScalar();
                connection.Close();
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters.Add("@magazyn", SqlDbType.Int);
                command.Parameters.Add("@ulica", SqlDbType.VarChar);
                command.Parameters.Add("@miasto", SqlDbType.VarChar);
                command.Parameters.Add("@telefon", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                command.Parameters["@magazyn"].Value = n;
                command.Parameters["@ulica"].Value = ulica;
                command.Parameters["@miasto"].Value = miasto;
                command.Parameters["@telefon"].Value = telefon;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void usunApteke(string nazwa)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query = "DELETE FROM dbo.apteki WHERE nazwa = @nazwa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void dodajLekdomagazynu(string nazwamagazynu, string nazwaleku,string datawaznosci, float cena, int ilosc)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query = "INSERT INTO " + nazwamagazynu +" (idleku, datawaznosci, cena, ilosc) VALUES (@idleku, @datawaznosci, @cena, @ilosc)";
            string query1 = "SELECT id FROM dbo.leki WHERE nazwa = @nazwaleku";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query1, connection);
                command.Parameters.Add("@nazwaleku", SqlDbType.VarChar);
                command.Parameters["@nazwaleku"].Value = nazwaleku;
                connection.Open();
                int x = (Int32)command.ExecuteScalar();
                connection.Close();
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@idleku", SqlDbType.Int);
                command.Parameters.Add("@datawaznosci", SqlDbType.Date);
                command.Parameters.Add("@cena", SqlDbType.Money);
                command.Parameters.Add("@ilosc", SqlDbType.Int);
                command.Parameters["@idleku"].Value = x;
                command.Parameters["@datawaznosci"].Value = datawaznosci;
                command.Parameters["@cena"].Value = cena;
                command.Parameters["@ilosc"].Value = ilosc;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void usunLekzmagazynu(string nazwamagazynu, string nazwaleku, string datawaznosci)
        {
            string connectionString = "Data Source=DESKTOP-MA57D0J;Initial Catalog=DrugSystem;Integrated Security=True";
            string query1 = "SELECT id FROM dbo.leki WHERE nazwa = @nazwaleku";
            string query = "DELETE FROM "+ nazwamagazynu + " WHERE nazwaleku = @idleku, datawaznosci = @datawaznosci";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query1, connection);
                command.Parameters.Add("@nazwaleku", SqlDbType.VarChar);
                command.Parameters["@nazwaleku"].Value = nazwaleku;
                connection.Open();
                int x = (Int32)command.ExecuteScalar();
                connection.Close();
                command = new SqlCommand(query, connection);
                command.Parameters.Add("@idleku", SqlDbType.Int);
                command.Parameters.Add("@datawaznosci", SqlDbType.Date);
                command.Parameters["@idleku"].Value = x;
                command.Parameters["@datawaznosci"].Value = datawaznosci;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
