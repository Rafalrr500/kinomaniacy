using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using projekt.Models;

namespace projekt.Data
{
    public class UserDB
    {
        private SqlConnection con;
        public UserDB(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MyProject");
            con = new SqlConnection(connectionString);
        }
        public void AddClient(Client c)
        {
            string query = "ClientCreate";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pfirstName", c.firstName);
            cmd.Parameters.AddWithValue("@plastName", c.lastName);
            cmd.Parameters.AddWithValue("@puserName", c.userName);
            cmd.Parameters.AddWithValue("@ppassword", c.password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                con.Close();
            }
        }
        public void AddStaff(Staff s)
        {
            string query = "StaffCreate";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pjobPosition", s.jobPosition);
            cmd.Parameters.AddWithValue("@puserName", s.userName);
            cmd.Parameters.AddWithValue("@ppassword", s.password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                con.Close();
            }
        }
        public void AddAdmin(Admin a)
        {
            string query = "AdminCreate";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@puserName", a.userName);
            cmd.Parameters.AddWithValue("@ppassword", a.password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                con.Close();
            }
        }
        public bool Exists(string userName, string query)
        {
            //string query = "UserExists";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
            }
            catch (SqlException) // jeżeli nie można połączyć się z bazą danych
            {
                con.Close();
                return false;
            }
            var reader = cmd.ExecuteReader();
            bool result = reader.Read();
            con.Close();
            reader.Close();
            return result;
        }
        public string GetPassword(string userName, string query)
        {
            //string query = "UserGetPassword";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
            }
            catch (SqlException) // jeżeli nie można połączyć się z bazą danych
            {
                con.Close();
                return null;
            }
            var reader = cmd.ExecuteReader();
            string password;
            if (reader.Read() == false)
                password = null;
            else
                password = (string)reader["password"]; // reader.GetString(0)
            con.Close();
            reader.Close();
            return password;
        }
        public void UpdatePassword(string userName, string password) // możnaby zrobić Update do aktualizowania całego użytkownika, ale na razie ma on tylko nazwę, czyli klucz i hasło, więc tutaj tak naprawdę aktualizujemy wszystkie niekluczowe pola
        {
            string query = "UserUpdatePassword";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            cmd.Parameters.AddWithValue("@pPassword", password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                con.Close();
            }
        }
        public void Delete(string userName, string query)
        {
            //string query = "UserDelete";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                con.Close();
            }
        }
    }
}
