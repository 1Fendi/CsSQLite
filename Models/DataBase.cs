using System.Data;
using System;
using System.Data.SQLite;
using Dapper;

namespace CsSQLite.Models
{
	public class DataBase
	{
		public IDbConnection dbConnection;
		public string DatabaseFilePath;
		public bool ActiveConnector;
		public DataBase(string ConnectionString)
		{
			DatabaseFilePath = ConnectionString;
			dbConnection = Connect(ConnectionString);
			dbConnection.Open();
		}

		public IDbConnection Connect(string ConnectionString)
		{	
			ActiveConnector = true;
			return new SQLiteConnection($"Data Source={ConnectionString}; Version=3 ; Foreign Keys=true");
		}

		public void DisConnect(bool ActiveMessage = false)
		{
			if (dbConnection.State == ConnectionState.Open)
			{
				ActiveConnector = false;
				dbConnection.Close();
	            if (ActiveMessage)Console.WriteLine($"Disconnection with {DatabaseFilePath} successful");
				return;
			}
			if (ActiveMessage) Console.WriteLine("No active connection to close.");

		}

		// public void PrintToConsole(List<DataBase> database)
        // {
        //     foreach (DataBase column in database)
        //     {
        //         Console.WriteLine($"ID: {student.Id}\t\tName: {student.Name}\t\tGrade: {student.Grade:0.00}");
        //     }
        // }
		public void CreateTable(string TableName, ColumnsInfo TableColumns)
		{
			try
			{
				string Code = $"CREATE TABLE IF NOT EXISTS \"{TableName}\" ({string.Join(" ,", TableColumns.ColumnFormats)});";
				Console.WriteLine($"tableC: {Code}");

				dbConnection.Execute(Code);
			}

			catch (SQLiteException e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}