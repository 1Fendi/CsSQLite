using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsSQLite.Models
{
    public class Columns
    {
        public Dictionary<string, string> DbColumns = new();
        public List<string> FieldNames = new();
        public List<string> FieldTypes = new();
        public List<string> FieldDataTypes = new();
        public Columns(List<string> Names, List<string> Types, List<string> DataType)
        {
            FieldNames = Names;
            FieldTypes = Types;
            FieldDataTypes = DataType;

            int Length = FieldNames.Count == FieldTypes.Count &&  FieldDataTypes.Count == Names.Count ? FieldNames.Count : 0;

            for (int item = 0; item < Length; item++)
            {
                DbColumns[$"{FieldNames[item]} {FieldTypes[item]}"] = FieldTypes[item];
            }
        }
    }
}