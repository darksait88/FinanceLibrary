using FinanceLibrary;
using FinanceLibrary.Api;
using FinanceLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;

namespace TestFinance
{
    class Program
    {
        static void Main(string[] args)
        {
            FinanceService.GetArrayQuotations(@"D:\", DateTime.Now.AddDays(-10), DateTime.Now);
        }
    }
}
