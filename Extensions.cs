using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsSQLite
{
    public static class Extensions
    {
        public static object Input(this object? Value, object? Message = null)
        {
            Console.Write(Message?.ToString() ?? "");
            string? userInput = Console.ReadLine();  
            return userInput ?? Value ?? ""; 
            
        }
    }
}