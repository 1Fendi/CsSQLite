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
		public void CreateTable(string TableName, Dictionary<string, string> TableColumns)
		{
			try
			{
	            List<string> ColumnFormats = new();
				List<string> dataColumnName = TableColumns.Keys.AsList();
				List<string> dataColumnType = TableColumns.Values.AsList();//[int (P),text...(P,AI)]
				string ColumnString = string.Empty;

				if (dataColumnType.Any(value => value.Contains("3")))
				{
					for(int item = 0; item < dataColumnType.Count; item++)
					{
						dataColumnType[item] = dataColumnType[item].Replace("2", "");
					}
				}

				for (int item = 0; item < dataColumnName.Count; item++)
				{
	                List<string> dataColumnParts = dataColumnType[item].Split(' ').ToList<string>(); //["(NN)", "(P,AI)"]
	               	// For type				
					if (dataColumnParts.Contains("1")) ColumnString = " INTEGER";
					if (dataColumnParts.Contains("2")) ColumnString = " TEXT";
					if (dataColumnParts.Contains("3")) ColumnString = " REAL";
					if (dataColumnParts.Contains("4")) ColumnString = " BLOB";
					if (dataColumnParts.Contains("5")) ColumnString = " NUMERIC";
					Console.WriteLine(ColumnString);
					//For dataType
	                if (dataColumnParts.Contains("1")) ColumnString += " NOT NULL";//"id integer not null"
	                if (dataColumnParts.Contains("2")) ColumnString += " PRIMARY KEY";
	                if (dataColumnParts.Contains("3")) ColumnString += " PRIMARY KEY AUTOINCREMENT";//id integer not null PRIMARY KEY AUTOINCREMENT
	                if (dataColumnParts.Contains("4")) ColumnString += " UNIQUE";
					Console.WriteLine(ColumnString);

				    ColumnFormats.Add(ColumnString);
					ColumnString = string.Empty;
				}     
				
				string Code = $"CREATE TABLE IF NOT EXISTS \"{TableName}\" ({columnname}{string.Join(", ", ColumnFormats)});";
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