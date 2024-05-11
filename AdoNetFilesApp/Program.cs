using Microsoft.Data.SqlClient;
using System.Data;

Dictionary<string, string> files = new();

files.Add("image.jpg", "Photo of Ada Lavlase");
files.Add("docs.pdf", "Lesson for academy");

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AcademyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


//string commandString = @"CREATE TABLE Files
//                         (
//                            id INT PRIMARY KEY IDENTITY,
//                            title NVARCHAR(100) NULL,
//                            file_name NVARCHAR(100) NOT NULL,
//                            binary_data VARBINARY(MAX)
//                         )";

string commandString = @"INSERT INTO Files 
                            (title, file_name, binary_data)
                            VALUES
                            (@title, @file_name, @binary_data)";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();

    SqlCommand command = connection.CreateCommand();
    command.CommandText = commandString;
    command.Parameters.Add("@title", SqlDbType.NVarChar, 100);
    command.Parameters.Add("@file_name", SqlDbType.NVarChar, 100);
    command.Parameters.Add("@binary_data", SqlDbType.VarBinary);

    //command.Parameters.Add("@file_name", SqlDbType.VarBinary, 100);
    byte[] buffer;

    foreach(var file in files)
    {
        using(FileStream stream = new(file.Key, FileMode.Open))
        {
            buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            
        }

        command.Parameters["@title"].Value = file.Value;
        command.Parameters["@file_name"].Value = file.Key;
        command.Parameters["@binary_data"].Value = buffer;

        await command.ExecuteNonQueryAsync();
    }
}