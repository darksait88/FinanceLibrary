using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FinanceLibrary;
using FinanceLibrary.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnitTestFinanceService
{
    [TestClass]
    public class UnitTestConverter
    {
        [TestMethod]
        public void TestMethodConvertCsvToArrayQuotation()
        {
            string quotationsCsv = "Date,Open,High,Low,Close,Adj Close,Volume\n2019-12-16,3183.629883,3197.709961,3183.629883,3191.449951,3191.449951,4051790000";
            Quotation quotation = Converter.ConvertCsvToListQuotation(quotationsCsv).FirstOrDefault();
            Quotation quotationMock = new Quotation(dateQuotation: new DateTime(2019,12,16),
                                                open: 3183.629883,
                                                high: 3197.709961,
                                                low: 3183.629883,
                                                close: 3191.449951,
                                                adjClose: 3191.449951);
            Assert.AreEqual(quotationMock, quotation);
        }
        [TestMethod]
        public void TestMethodConvertCsvToXlsx()
        {
            string quotationsCsv = "Date,Open,High,Low,Close,Adj Close,Volume\n2019-12-16,3183.629883,3197.709961,3183.629883,3191.449951,3191.449951,4051790000";
            bool result = Converter.ConvertCsvToXlsx(quotationsCsv, Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodConvertToUnixTimestamp()
        {
            DateTime date = Converter.ConvertFromUnixTimestamp(1608061870);
            Assert.AreEqual(new DateTime(2020, 12, 15, 19, 51, 10), date);
        }

        [TestMethod]
        public void TestMethodConvertFromUnixTimestamp()
        {
            double unixDate = Converter.ConvertToUnixTimestamp(new DateTime(2020, 12, 14, 19, 51, 10));
            Assert.AreEqual(1607975470, unixDate);
        }

        [TestMethod]
        public void TestMethodConvertListQuotationToJArray()
        {
            Quotation quotation = new Quotation(dateQuotation: new DateTime(2020, 12, 16),
                                                open: 3183.629883,
                                                high: 3197.709961,
                                                low: 3183.629883,
                                                close: 3191.449951,
                                                adjClose: 3191.449951);
            quotation.ChangeCurrencyToRub(72);
            List<Quotation> quotations = new List<Quotation>();
            quotations.Add(quotation);
            JArray jArray = Converter.ConvertListQuotationToJArray(quotations);

            JArray jArrayMock = new JArray();
            JObject jObjectMock = new JObject();
            jObjectMock["date"] = "2020-12-16T00:00:00";
            jObjectMock["open"] = 229221.35157600002;
            jObjectMock["high"] = 230235.117192;
            jObjectMock["low"] = 229221.35157600002;
            jObjectMock["close"] = 229784.396472; 
            jObjectMock["adjClose"] = 229784.396472;
            jArrayMock.Add(jObjectMock);
            Assert.AreEqual(jArrayMock.ToString(), jArray.ToString());
        }
    }
}
