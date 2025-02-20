using System.Data;
using System;
using System.Data.SQLite;
using Dapper;

namespace CsSQLite.Models
{
	public class DataBase
	{
		public IDbConnection connection;
		public string FileName;
		public bool ActiveConnect;
		public DataBase(string Con)
		{
			FileName = Con;
			connection = Connect(Con);
			connection.Open();
		}

		public IDbConnection Connect(string Con)
		{	
			ActiveConnect = true;
			return new SQLiteConnection($"Data Source={Con}; Version=3 ; Foreign Keys=true");
		}

		public void DisConnect(bool ActiveMessage = false)
		{
			if (connection.State == ConnectionState.Open)
			{
				ActiveConnect = false;
				connection.Close();
	            if (ActiveMessage)Console.WriteLine($"Disconnection with {FileName} successful");
				return;
			}
			if (ActiveMessage) Console.WriteLine("No active connection to close.");

		}

		public void CreateTable(string TableName, Dictionary<string, string> Columns)
		{
			try
			{
	            List<string> FormattedColumns = new();
	            bool PrimarySet = false;  // To track if PRIMARY KEY has been set

	            foreach(var item in Columns)  //"id INTEGER" : " (NN) (P,AI)"
				{
					string field = item.Key;
					string dataType = item.Value;

	                List<string> parts = dataType.Split(' ').ToList<string>(); //["(NN)", "(P,AI)"]
	               
	                // Check if column is NOT NULL
	                if (parts.Contains("(NN)")) field += " NOT NULL";//"id integer not null"
	                if (parts.Contains("(U)")) field += " UNIQUE";
	
	                // Check if column is PRIMARY KEY AUTOINCREMENT
	                if (parts.Contains("(P,AI)"))
					{
	                    if (PrimarySet) field = field.Replace("PRIMARY KEY", "");
	
	                    field += " PRIMARY KEY AUTOINCREMENT";//id integer not null PRIMARY KEY AUTOINCREMENT
	                    PrimarySet = true;
					}
	                // Check if column is PRIMARY KEY
	                else if (parts.Contains("(P)") && !PrimarySet)
	                {    
						field += " PRIMARY KEY";
	                    PrimarySet = true;
					}   
					
				    FormattedColumns.Add(field);
				}     

				string Code = $"CREATE TABLE \"{TableName}\" ({string.Join(", ", FormattedColumns)});";
				connection.Execute(Code);
			}

			catch (SQLiteException e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}