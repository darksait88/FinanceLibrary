using FinanceLibrary.Exceptions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace FinanceLibrary.Api
{
    public static class CbrApi
    {
        public static double GetValCursUsd(DateTime date)
        {
            string dateString = $"{String.Format("{0:d2}", date.Day)}/{date.Month}/{date.Year}";
            string url = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={dateString}";

            XDocument xDoc;
            try
            {
                xDoc = XDocument.Load(url);
            }
            catch(Exception ex)
            {
                throw new CbrApiException($"Error load valute curs: {ex.Message}");
            }

            if(xDoc.Element("ValCurs") == null)
                throw new CbrApiException("Not load valute curs");

            XElement item = xDoc.Element("ValCurs").Elements("Valute")
                    .Where(xe => xe.Attribute("ID").Value == "R01235")
                    .Select(xe => xe.Element("Value"))
                    .FirstOrDefault();
            if (item == null)
                throw new CbrApiException("Not found valute curs USD");
            return Double.Parse(item.Value);
        }
    }
}
