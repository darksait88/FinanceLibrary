using FinanceLibrary.Exceptions;
using System;
using System.Net;

namespace FinanceLibrary.Api
{
    public static class FinanceApi
    {
        public static string GetFinanceQuotations(DateTime dateBegin, DateTime dateEnd)
        {
            if(dateBegin > dateEnd)
                throw new FinanceApiException($"The start date cannot be greater than the end date");
            string url = $"https://query1.finance.yahoo.com/v7/finance/download/%5EGSPC?period1={dateBegin.ConvertToUnixTimestamp()}&period2={dateEnd.ConvertToUnixTimestamp()}&interval=1d&events=history&includeAdjustedClose=true";
            using (var webClient = new WebClient())
            {
                try
                {
                    var quotations = webClient.DownloadString(url);
                    return quotations;
                }
                catch (Exception ex)
                {
                    throw new FinanceApiException($"Error load finance quotations S&P 500: {ex.Message}");
                }
            }
        }
    }
}
