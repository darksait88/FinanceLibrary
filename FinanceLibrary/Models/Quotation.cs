using Newtonsoft.Json;
using System;

namespace FinanceLibrary.Models
{
    public class Quotation
    {
        [JsonProperty("date")]
        internal DateTime DateQuotation { get; private set; }
        [JsonProperty("open")]
        internal double Open { get; private set; }
        [JsonProperty("high")]
        internal double High { get; private set; }
        [JsonProperty("low")]
        internal double Low { get; private set; }
        [JsonProperty("close")]
        internal double Close { get; private set; }
        [JsonProperty("adjClose")]
        internal double AdjClose { get; private set; }

        public Quotation(DateTime dateQuotation,
                        double open,
                        double high,
                        double low,
                        double close,
                        double adjClose
            )
        {
            DateQuotation = dateQuotation;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            AdjClose = adjClose;
        }

        public void ChangeCurrencyToRub(double curs)
        {
            Open *= curs;
            High *= curs;
            Low *= curs;
            Close *= curs;
            AdjClose *= curs;
        }
        public override bool Equals(object obj)
        {
            var item1 = this;
            var item2 = obj as Quotation;

            if (item1 == null && item2 != null ||
                item1 != null && item2 == null)
                return false;

            if (item1.DateQuotation == item2.DateQuotation &&
                item1.High == item2.High &&
                item1.Low == item2.Low &&
                item1.Close == item2.Close &&
                item1.AdjClose == item2.AdjClose)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
