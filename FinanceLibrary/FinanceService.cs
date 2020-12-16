using FinanceLibrary.Api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FinanceLibrary
{
    public static class FinanceService
    {
        public static JArray GetArrayQuotations(string path, DateTime dateBegin, DateTime dateEnd)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

            var quotationsCsv = FinanceApi.GetFinanceQuotations(dateBegin, dateEnd);
            Converter.ConvertCsvToXlsx(quotationsCsv, path);
            var quotations = Converter.ConvertCsvToListQuotation(quotationsCsv);

            for(int i = 0; i < quotations.Count; i++)
            {
                var valCurs = CbrApi.GetValCursUsd(quotations[i].DateQuotation);
                quotations[i].ChangeCurrencyToRub(valCurs);
            }

            JArray jArray = Converter.ConvertListQuotationToJArray(quotations);

            return jArray;
        }
    }
}
