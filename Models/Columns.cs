using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CsSQLite.Models
{
    public class ColumnsInfo
    {
        public List<string> FieldNames = new();
        public List<string> FieldTypes = new();
        public List<string> FieldDataTypes = new();
        public List<string> ColumnFormats = new();

        public ColumnsInfo(List<string> Names, List<string> Types, List<string> DataTypes)
        {
            if (Names.Count != Types.Count && Types.Count != DataTypes.Count)
            {
                // TODO: تأكد من طول الأعمدة إذا كان غير متساوي

                Console.WriteLine("Columns length are not equal");
				return;
            }
        
            // TODO: تحقق اذا كان فيه "3" في DataTypes
            var targets = Extensions.FindObjPosition("3", 2, DataTypes);
            
            List<string> formatedcolumns = new();
            List<List<string>> dataColumnTypeParts = new();
            List<List<string>> dataColumnDataParts = new();
            for (int item = 0; item < Names.Count; item++)
            {
                string ColumnString = string.Empty;

                // TODO: لازم نتأكد إذا لازم نضيف جزء البيانات إلى dataColumnTypeParts و dataColumnDataParts

                dataColumnTypeParts.Add(Types[item].Split(' ').ToList<string>());
                dataColumnDataParts.Add(DataTypes[item].Split(' ').ToList());
                // TODO: استخدم indexOf عشان تغيّر النوع

                if (dataColumnTypeParts[item].Contains("1"))
                {
                    ColumnString = $"{Names[item]} INTEGER";
                    dataColumnTypeParts[item][dataColumnTypeParts[item].IndexOf("1")] = "INTEGER";
                }
                // TODO: كرر العملية لبقية الأنواع (2، 3، 4، 5)

                if (dataColumnTypeParts[item].Contains("2"))
                {
                    ColumnString = $"{Names[item]} TEXT";
                    dataColumnTypeParts[item][dataColumnTypeParts[item].IndexOf("2")] = "TEXT";
                }
                if (dataColumnTypeParts[item].Contains("3"))
                {
                    ColumnString = $"{Names[item]} REAL";
                    dataColumnTypeParts[item][dataColumnTypeParts[item].IndexOf("3")] = "REAL";
                }
                if (dataColumnTypeParts[item].Contains("4"))
                {
                    ColumnString = $"{Names[item]} BLOB";
                    dataColumnTypeParts[item][dataColumnTypeParts[item].IndexOf("4")] = "BLOB";
                }
                if (dataColumnTypeParts[item].Contains("5"))
                {
                    ColumnString = $"{Names[item]} NUMERIC";
                    dataColumnTypeParts[item][dataColumnTypeParts[item].IndexOf("5")] = "NUMERIC";
                }
                // TODO: بالنسبة لdataType، لازم تكون دقيق في التعديلات هنا

                if (dataColumnDataParts[item].Contains("1"))
                {
                    ColumnString += " NOT NULL";//"id integer not null"
                    dataColumnDataParts[item][dataColumnDataParts[item].IndexOf("1")] = "NOT NULL";
                }
                if (dataColumnDataParts[item].Contains("2"))
                {
                    ColumnString += " PRIMARY KEY";
                    dataColumnDataParts[item][dataColumnDataParts[item].IndexOf("2")] = "PRIMARY KEY";
                }
                if (dataColumnDataParts[item].Contains("3"))
                {
                    ColumnString += " PRIMARY KEY AUTOINCREMENT";//id integer not null PRIMARY KEY AUTOINCREMENT
                    dataColumnDataParts[item][dataColumnDataParts[item].IndexOf("3")] = "PRIMARY KEY ";
                }
                if (dataColumnDataParts[item].Contains("4"))
                {
                    ColumnString += " UNIQUE";
                    dataColumnDataParts[item][dataColumnDataParts[item].IndexOf("4")] = "UNIQUE";
                }
                // TODO: احفظ البيانات في الفورمات النهائي

                formatedcolumns.Add(ColumnString);
            }
            FieldNames = Names;
            FieldTypes = Types;
            FieldDataTypes = DataTypes;
            ColumnFormats = formatedcolumns;
            
        }
    }
}
