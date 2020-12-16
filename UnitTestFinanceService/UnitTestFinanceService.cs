using FinanceLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestFinanceService
{
    [TestClass]
    public class UnitTestFinanceService
    {
        [TestMethod]
        public void TestMethodFinanceService()
        {
            Quotation quotationMock = new Quotation(dateQuotation: new DateTime(2019, 12, 16),
                                                open: 229221.35157600002,
                                                high: 230235.117192,
                                                low: 229221.35157600002,
                                                close: 229784.396472,
                                                adjClose: 229784.396472);

            Quotation quotation = new Quotation(dateQuotation: new DateTime(2019, 12, 16),
                                                open: 3183.629883,
                                                high: 3197.709961,
                                                low: 3183.629883,
                                                close: 3191.449951,
                                                adjClose: 3191.449951);
            quotation.ChangeCurrencyToRub(72);
            Assert.AreEqual(quotationMock, quotation);
        }
    }
}
