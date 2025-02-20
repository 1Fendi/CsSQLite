using System.Data;
using System;
using Dapper;
using SQLitePCL;

using CsSQLite.Models;

namespace CsSQLite
{
    internal class Program
    {
        static int? input;
        static DataBase? file;
        static string path = string.Empty;
        static object Input = (string) new object();
        static void Main(string[] args)
        {
            Display();
        }

        private static void Display()
        {
            while (true) // Keeps looping until user exits
            {
                Console.Clear();
                input = (int) Input.Input($"""

                                    --- SQLite Options Menu ---
                                    1. Connection Options\t{(file != null ? (file.ActiveConnect ? $"Connected with {file.FileName}" : "") : "")}
                                    2. Table Options
                                    3. Columns Options
                                    4. Component Options
                                    5. Exit
                                
                                    """);

                switch (input)
                {
                    case 1:
                        ConnectionOptions();
                        break;
                    
                    case 2:
                        TableOptions();
                        break;

                    case 3:
                        ColumnsOptions();
                        break;

                    case 4:
                        ComponentOptions();
                        break;

                    case 5:
                        Console.WriteLine("Exiting program...");
                        return; // Exit the loop & program

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }

        private static void ConnectionOptions()//Done #
        {

            Console.Clear();
            bool SitConnection = true;
            while (SitConnection)
            {
                
                input = (int) Input.Input("""

                        --- Connection Options ---
                        1. Enter the path of the DataBase / Make a DataBase
                        2. Disconnect
                        3. Return back

                        """);


                switch (input)
                {
                    case 1:
                        // Connect to an existing database or create a new one
                        path = (string) Input.Input("Enter file name/path: ");
                        file = new DataBase(path);
                        Console.WriteLine($"Connect with {file.FileName} has been succeed");

                        new object().Input("Press to return");
                        SitConnection = false;
                        break;

                    case 2:
                        file = new DataBase(path);
                        file.DisConnect(true);
                        break;


                    case 3:
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
            // TODO a func take a keyword like int and text and so on and it will convert it to the columns type`
            // TODO i just need to make the func ask what type does the user want 
            // TODO then it will ask him about the column like (NN)...
            if (file == null || !file.ActiveConnect)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            Console.Clear();
            while (true)
            {
                
                input = (int) Input.Input("""

                        --- Table Options ---
                        1. Make a table
                        2. Rename a table
                        3. Delete a table
                        4. Return back

                    """);

                string Table = string.Empty;

                switch (input)
                {
                    case 1:
                        // Create a new table
                        List<string> FieldName = new();
                        List<string> FieldType = new();
                        List<string> FieldDataType = new();

                        Table = (string)Table.Input("Table name: ");

                        bool SitColumn = true;
                        while (SitColumn)
                        {
                            Console.WriteLine("To Stop Press (/) or Press Enter");

                            string item = (string) Input.Input("Enter Column name: ");

                            if (string.IsNullOrWhiteSpace(item) || item == "/") break;

                            FieldName.Add(item);

                            string type = (string) Input.Input($"""
                                    Enter {item} type 
                                    int : INTEGER
                                    str : TEXT
                                    real | float : REAL
                                    blob : BLOB
                                    num : NUMERIC

                                    """);

                            string DataType = (string)Input.Input($"""
                                    Enter {item} type
                                    NN : NOT NULL
                                    P : PRIMARY KEY
                                    AI : AUTOINCREMENT
                                    P,AI : PRIMARY KEY AUTOINCREMENT
                                    U : UNIQUE

                                    """);
                                    
                            FieldType.Add($"({type.ToUpper()})");
                        };

                        Columns columns = new(FieldName, FieldType, DataTy);
                        file.CreateTable(Table, columns.DbColumns);
                        break;

                    case 2:
                        // Rename an existing table
                        break;

                    case 3:
                        // Delete a table
                        break;

                    case 4:
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
            if (file == null || !file.ActiveConnect)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            Console.Clear();
            while (true)
            {
                
                Console.WriteLine("""
                        --- Column Options ---");
                        1. Add a column
                        2. Rename a column
                        3. Delete a column
                        4. Return back

                        """);

                input = GetUserInput();

                switch (input)
                {
                    case 1:
                        // Add a new column to a table
                        break;

                    case 2:
                        // Rename an existing column
                        break;

                    case 3:
                        // Delete a column from a table
                        break;

                    case 4:
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
            if (file == null || !file.ActiveConnect)
            {
                Console.WriteLine("There's no connection");
                return;
            }
            Console.Clear();
            while (true)
            {
            
                input = (int) Input.Input("""

                        --- Component Options ---
                        1. Add a component
                        2. Edit a component
                        3. Delete a component
                        4. Return back

                        """);

                switch (input)
                {
                    case 1:
                        // Add a new row to a table
                        break;

                    case 2:
                        // Edit an existing row
                        break;

                    case 3:
                        // Delete a row from a table
                        break;

                    case 4:
                        Display();
                        return; 

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }

        

        private static int? GetUserInput()
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            return null; // Return null if input is invalid
        }
    }
}
