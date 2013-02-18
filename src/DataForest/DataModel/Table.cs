using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DataForest.DataModel
{
     public class Table : DataTable
    {
         //Quelle: http://stackoverflow.com/questions/1050112/how-to-read-a-csv-file-into-a-net-datatable 
         //answered Jun 3 '11 at 15:09 Nodir

         public static Table ReadFromCSV(string filename, string separatorChar, out List<string> errors)
         {
             #region Import Grid from CSV
             errors = new List<string>();
             var table = new Table();
             using (var sr = new StreamReader(filename, Encoding.Default))
             {
                 string line;
                 var i = 0;
                 while (sr.Peek() >= 0)
                 {
                     try
                     {
                         line = sr.ReadLine();
                         if (string.IsNullOrEmpty(line)) continue;
                         var values = line.Split(new[] { separatorChar }, StringSplitOptions.None);
                         var row = table.NewRow();
                         for (var colNum = 0; colNum < values.Length; colNum++)
                         {
                             var value = values[colNum];
                             if (i == 0)
                             {
                                 table.Columns.Add(value, typeof(String));
                             }
                             else
                             {
                                 row[table.Columns[colNum]] = value;
                             }
                         }
                         if (i != 0) table.Rows.Add(row);
                     }
                     catch (Exception ex)
                     {
                         errors.Add(ex.Message);
                     }
                     i++;
                 }
             }
             return table;
             #endregion
         }

         // Quelle http://stackoverflow.com/questions/888181/convert-datatable-to-csv-stream
         // answered May 20 '09 at 15:46 adopilot

         public void CreateCSVFile(string strFilePath, string separator)
         {
             #region Export Grid to CSV
             // Create the CSV file to which grid data will be exported.
             StreamWriter sw = new StreamWriter(strFilePath, false);
             int iColCount = this.Columns.Count;

             // First we will write the headers.

             //DataTable dt = m_dsProducts.Tables[0];
             for (int i = 0; i < iColCount; i++)
             {
                 sw.Write(this.Columns[i]);
                 if (i < iColCount - 1)
                 {
                     sw.Write(separator);
                 }
             }
             sw.Write(sw.NewLine);

             // Now write all the rows.
             foreach (DataRow dr in this.Rows)
             {
                 for (int i = 0; i < iColCount; i++)
                 {
                     if (!Convert.IsDBNull(dr[i]))
                     {
                         sw.Write(dr[i].ToString());
                     }
                     if (i < iColCount - 1)
                     {
                         sw.Write(separator);
                     }
                 }
                 sw.Write(sw.NewLine);
             }
             sw.Close();

             #endregion
         }
    }
}
