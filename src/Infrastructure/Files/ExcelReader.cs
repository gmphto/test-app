using System.Data;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Application.Common.Interfaces;
using ExcelDataReader;

namespace Infrastructure.Files;

public class ExcelReader : IExcelReader
{
    public DataTable ReadExcelDocument(Stream stream)
    {
        using (var streamReader = new StreamReader(stream))
        {
            try
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    //// reader.IsFirstRowAsColumnNames
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet(conf);
                    // Now you can get data from each sheet by its index or its "name"
                    return result.Tables[0];

                    // The result of each spreadsheet is in result.Tables
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

