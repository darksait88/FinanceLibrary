using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FinanceLibrary.Api;

namespace UnitTestFinanceService
{
    [TestClass]
    public class UnitTestApi
    {
        [TestMethod]
        public void TestFinanceApi()
        {
            string quotationsCsv = FinanceApi.GetFinanceQuotations(new DateTime(2020, 12, 14), new DateTime(2020, 12, 15));
            Assert.AreEqual(2, quotationsCsv.Split('\n').Length);
        }

        [TestMethod]
        public void TestCbrApi()
        {
            double cursValute = CbrApi.GetValCursUsd(new DateTime(2020, 12, 15));
            Assert.AreEqual(72.9272, cursValute);
        }
    }
}
