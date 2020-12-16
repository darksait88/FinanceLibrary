using FinanceLibrary.Exceptions;
using FinanceLibrary.Models;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace FinanceLibrary
{
    public static class Converter
    {
        public static DataTable ConvertCsvToDataTable(string csvText)
        {
            try
            {
                byte[] arrayByte = Encoding.UTF8.GetBytes(csvText);
                MemoryStream streamText = new MemoryStream(arrayByte);
                StreamReader sr = new StreamReader(streamText);

                string[] headers = sr.ReadLine().Split(',');
                DataTable dt = new DataTable();

                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    string[] columns = row.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = columns[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch(Exception ex)
            {
                throw new ConverterException($"Error convert csv to datatable: {ex.Message}");
            }
        }

        public static List<Quotation> ConvertCsvToListQuotation(string csvText)
        {
            try
            {
                byte[] arrayByte = Encoding.UTF8.GetBytes(csvText);
                MemoryStream streamText = new MemoryStream(arrayByte);
                StreamReader sr = new StreamReader(streamText);

                Dictionary<string, int> dictHeaders = new Dictionary<string, int>();
                string[] headers = sr.ReadLine().Split(',');

                for (int i = 0; i < headers.Length; i++)
                    dictHeaders.Add(headers[i], i);

                List<Quotation> quotations = new List<Quotation>();
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    string[] columns = row.Split(',');

                    Quotation quotation = new Quotation(dateQuotation: DateTime.ParseExact(columns[dictHeaders["Date"]], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                                open: columns[dictHeaders["Open"]].Contains(",") ? Double.Parse(columns[dictHeaders["Open"]], new CultureInfo("ru-Ru")) : Double.Parse(columns[dictHeaders["Open"]], new CultureInfo("en-En")),
                                                high: columns[dictHeaders["High"]].Contains(",") ? Double.Parse(columns[dictHeaders["High"]], new CultureInfo("ru-Ru")) : Double.Parse(columns[dictHeaders["High"]], new CultureInfo("en-En")),
                                                low: columns[dictHeaders["Low"]].Contains(",") ? Double.Parse(columns[dictHeaders["Low"]], new CultureInfo("ru-Ru")) : Double.Parse(columns[dictHeaders["Low"]], new CultureInfo("en-En")),
                                                close: columns[dictHeaders["Close"]].Contains(",") ? Double.Parse(columns[dictHeaders["Close"]], new CultureInfo("ru-Ru")) : Double.Parse(columns[dictHeaders["Close"]], new CultureInfo("en-En")),
                                                adjClose: columns[dictHeaders["Adj Close"]].Contains(",") ? Double.Parse(columns[dictHeaders["Adj Close"]], new CultureInfo("ru-Ru")) : Double.Parse(columns[dictHeaders["Adj Close"]], new CultureInfo("en-En")));
                    quotations.Add(quotation);
                }
                return quotations;
            }
            catch(Exception ex)
            {
                throw new ConverterException($"Error convert csv to array quotation: {ex.Message}");
            }
        }

        public static JArray ConvertListQuotationToJArray(List<Quotation> quotations)
        {
            try
            {
                JArray jArray = JArray.FromObject(quotations);

                return jArray;
            }
            catch (Exception ex)
            {
                throw new ConverterException($"Error convert list quotation to jarray quotation: {ex.Message}");
            }
        }

        public static bool ConvertCsvToXlsx(string csvText, string path)
        {
            try
            {
                string worksheetsName = "Finance";
                bool firstRowIsHeader = false;

                var excelTextFormat = new ExcelTextFormat()
                {
                    Delimiter = ',',
                    EOL = "\n",
                    DataTypes = new eDataTypes[] { eDataTypes.String, eDataTypes.String,
                    eDataTypes.String, eDataTypes.String, eDataTypes.String,
                    eDataTypes.String }
                };

                string fullname = Path.Combine(path, $"FinanceQuotations{DateTime.Now:HH_mm_ss_dd_MM_yyyy}.xlsx");
                var excelFileInfo = new FileInfo(fullname);

                using (ExcelPackage package = new ExcelPackage(excelFileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet.Cells["A1"].LoadFromText(csvText, excelTextFormat, OfficeOpenXml.Table.TableStyles.Medium25, firstRowIsHeader);
                    package.Save();
                }
                if (File.Exists(fullname))
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw new ConverterException($"Error convert csv to xlsx: {ex.Message}");
            }
        }

        public static double ConvertToUnixTimestamp(this DateTime date)
        {
            try
            {
                if (date == null)
                    throw new ConverterException("DateTime is null");
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                TimeSpan diff = date - origin;
                return Math.Floor(diff.TotalSeconds);
            }
            catch (Exception ex)
            {
                throw new ConverterException($"Error convert datetime to unixtimestamp: {ex.Message}");
            }

        }

        public static DateTime ConvertFromUnixTimestamp(this double timestamp)
        {
            try
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(timestamp);
            }
            catch (Exception ex)
            {
                throw new ConverterException($"Error convert unixtimestamp to datetime: {ex.Message}");
            }
        }
    }
}
