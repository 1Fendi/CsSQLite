using System.Data;
using System;
using Dapper;
using SQLitePCL;

using CsSQLite.Models;

namespace CsSQLite
{
    internal class Program
    {
        static string? input;
        static DataBase? file;
        static ColumnsInfo? DataBaseColumns;
        static string path = string.Empty;
        static object Input = new object();
        static void Main(string[] args)
        {
            Display();
        }

        private static void Display()
        {
            while (true) // Keeps looping until user exits
            {
                MainMenu();

                switch (input)
                {
                    case "1":
                        ConnectionOptions();
                        break;

                    case "2":
                        TableOptions();
                        break;

                    case "3":
                        ColumnsOptions();
                        break;

                    case "4":
                        ComponentOptions();
                        break;

                    case "5":
                        Console.WriteLine("Exiting program...");
                        Thread.Sleep(500);
                        return; // Exit the loop & program

                    default:
                        // Console.Clear();
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }


        private static void ConnectionOptions()//Done #
        {

            bool SitConnection = true;
            while (SitConnection)
            {
                input = "1";

                if (file != null && file.ActiveConnector)
                {
                    ConnectionMenu();
                }
                
                switch (input)
                {
                    case "1":
                        // Connect to an existing database or create a new one

                        path = (string) Input.Input("Enter file name/path: ");
                        file = new DataBase(path);
                        Console.WriteLine($"Connect with {file.DatabaseFilePath} has been succeed");

                        SitConnection = false;
                        break;

                    case "2":
                        file = new DataBase(path);
                        file.DisConnect(true);
                        break;


                    case "3":
                        Display();
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }


        private static void TableOptions()
        {
            if (file == null || !file.ActiveConnector)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            while (true)
            {
                TableMenu();

                string Table = string.Empty;

                switch (input)
                {
                    case "1":
                        // Create a new table
                        List<string> ColumnName = new();
                        List<string> ColumnType = new();
                        List<string> ColumnDataType = new();

                        Table = (string)Table.Input("Table name: ");

                        while (true)
                        {
                            Console.WriteLine("To Stop Press (/) or Press Enter");

                            string ColumnNameInput = (string)Input.Input("Enter Column name: ");

                            if (string.IsNullOrWhiteSpace(ColumnNameInput) || ColumnNameInput == "/") break;


                            string ColumnTypeInput = (string)Input.Input($"""
                                    Enter {ColumnNameInput} type 
                                    1 : INTEGER
                                    2 : TEXT
                                    3 : REAL
                                    4 : BLOB
                                    5 : NUMERIC
                                    Enter your choice: 
                                    """);
                            if (string.IsNullOrWhiteSpace(ColumnTypeInput) || ColumnTypeInput == "/") break;

                            string columnDataTypeInput = (string)Input.Input($"""
                                    Enter {ColumnNameInput} type
                                    1 : NOT NULL
                                    2 : PRIMARY KEY
                                    3 : PRIMARY KEY AUTOINCREMENT                    
                                    4 : UNIQUE
                                    Enter your choice: 
                                    """);
                            if (string.IsNullOrWhiteSpace(columnDataTypeInput) || columnDataTypeInput == "/") break;

                             
                            ColumnType.Add(ColumnTypeInput);
                            ColumnDataType.Add(columnDataTypeInput);
                        }

                        ColumnsInfo NewColumns = new(ColumnName, ColumnType, ColumnDataType);
                        file.CreateTable(Table, NewColumns);
                        break;

                    case "2":
                        // Rename an existing table
                        break;

                    case "3":
                        // Delete a table
                        break;

                    case "4":
                        Display();
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }


        private static void ColumnsOptions()
        {
            if (file == null || !file.ActiveConnector)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            while (true)
            {
                ColumnMenu();

                switch (input)
                {
                    case "1":
                        // Add a new column to a table
                        break;

                    case "2":
                        // Rename an existing column
                        break;

                    case "3":
                        // Delete a column from a table
                        break;

                    case "4":
                        Display();
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }


        private static void ComponentOptions()
        {
            if (file == null || !file.ActiveConnector)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            while (true)
            {
                ComponentMenu();

                switch (input)
                {
                    case "1":
                        // Add a new row to a table
                        break;

                    case "2":
                        // Edit an existing row
                        break;

                    case "3":
                        // Delete a row from a table
                        break;

                    case "4":
                        Display();
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }


        // Functions for menu
        
        private static void MainMenu()
        {
            // Console.Clear();

            input = (string)Input.Input($"""

                        --- SQLite Options Menu ---
                        1. Connection Options       {(file != null ? (file.ActiveConnector ? $"Connected with {file.DatabaseFilePath}" : "") : "")}
                        2. Display database
                        3. Table Options
                        4. Columns Options
                        5. Component Options
                        6. Exit

                        Enter your choice: 
                        """);
            Console.WriteLine($"input: {input}");
            
        }

        private static void ConnectionMenu()
        {
            // Console.Clear();

            input = (string)Input.Input("""

                        --- Connection Options ---
                        1. Enter the path of the DataBase / Make a DataBase
                        2. Disconnect
                        3. Return back

                        Enter your choice: 
                        """);
            Console.WriteLine($"input: {input}");
        }

        private static void TableMenu()
        {
            // Console.Clear();

            input = (string)Input.Input("""

                        --- Table Options ---
                        1. Make a table
                        2. Rename a table
                        3. Delete a table
                        4. Return back
                        Enter your choice: 
                        """);
            Console.WriteLine($"input: {input}");
            
        }

        private static void ColumnMenu()
        {
            // Console.Clear();

            input = (string)Input.Input("""
                        --- Column Options ---
                        1. Add a column
                        2. Rename a column
                        3. Delete a column
                        4. Return back

                        Enter your choice: "
                        """);
            Console.WriteLine($"input: {input}");
        }
        private static void ComponentMenu()
        {
            // Console.Clear();

            input = (string)Input.Input("""

                        --- Component Options ---
                        1. Add a component
                        2. Edit a component
                        3. Delete a component
                        4. Return back

                        Enter your choice: 
                        """);
            Console.WriteLine($"input: {input}");          
        }
    }
}
