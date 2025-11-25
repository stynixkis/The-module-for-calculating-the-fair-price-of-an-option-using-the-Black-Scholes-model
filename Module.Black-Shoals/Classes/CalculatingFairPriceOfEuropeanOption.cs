using Module.Black_Shoals.Service;

namespace Module.Black_Shoals.Classes
{
    /// <summary>
    /// Класс для подсчета справедливой цены европейского опциона
    /// </summary>
    public class CalculatingFairPriceOfEuropeanOption
    {
        /// <summary>
        /// Рыночная цена базового актива
        /// </summary>
        public double CurrentPriceOfUnderlyingAsset { get; set; }
        /// <summary>
        /// Цена исполнения (страйк)
        /// </summary>
        public double Strike { get; set; }
        /// <summary>
        /// Безрисковая процентная ставка
        /// </summary>
        public double RiskFreeInterestRate { get; set; }
        /// <summary>
        /// Время до экспирации
        /// </summary>
        public double TimeToOptioneExpiration { get; set; }
        /// <summary>
        /// Волатильность
        /// </summary>
        public double Volatility { get; set; }
        /// <summary>
        /// Коэффициент d1
        /// </summary>
        protected double _d1 { get; set; }
        /// <summary>
        /// Коэффициент d2
        /// </summary>
        protected double _d2 { get; set; }
        /// <summary>
        /// Стоимость опциона Call
        /// </summary>
        public double PriceOptionCall { get; set; }
        /// <summary>
        /// Стоимость опциона Put
        /// </summary>
        public double PriceOptionPut { get; private set; }
        /// <summary>
        /// Экземпляр класса, содержащий значения греков
        /// </summary>
        public CalculatingGreeks GreeksValue { get; set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="volatility">Волатильность</param>
        public CalculatingFairPriceOfEuropeanOption(double currentPriceOfUnderlyingAsset, double strike,
            double riskFreeInterestRate, double timeToOptioneExpiration, double volatility)
        {
            CurrentPriceOfUnderlyingAsset = currentPriceOfUnderlyingAsset;
            Strike = strike;
            RiskFreeInterestRate = riskFreeInterestRate;
            TimeToOptioneExpiration = timeToOptioneExpiration;
            Volatility = volatility;

            _d1 = Calculating_d1();
            _d2 = Calculating_d2();

            PriceOptionCall = CalculatingPriceOption_Call();
            PriceOptionPut = CalculatingPriceOption_Put();

            GreeksValue = new CalculatingGreeks(CurrentPriceOfUnderlyingAsset, Strike, RiskFreeInterestRate,
                TimeToOptioneExpiration, Volatility, _d1, _d2);
        }
        /// <summary>
        /// Метод подсчета коэффициента d1
        /// </summary>
        /// <returns></returns>
        protected double Calculating_d1()
        {
            double valueOne = Math.Log(CurrentPriceOfUnderlyingAsset / Strike);
            double valueTwo = RiskFreeInterestRate + (Math.Pow(Volatility, 2) / 2);
            double valueNumerator = valueOne + valueTwo * TimeToOptioneExpiration;
            double valueDenominator = Volatility * Math.Sqrt(TimeToOptioneExpiration);
            return valueNumerator / valueDenominator;
        }
        /// <summary>
        /// Метод подсчета коэффициента d2
        /// </summary>
        /// <returns></returns>
        protected double Calculating_d2()
        {
            return _d1 - Volatility * Math.Sqrt(TimeToOptioneExpiration);
        }
        /// <summary>
        /// Метод подсчета цены опциона Call
        /// </summary>
        /// <returns></returns>
        protected double CalculatingPriceOption_Call()
        {
            double valueOne = CurrentPriceOfUnderlyingAsset * Methods.StandardNormalCDF(_d1);
            double valueTwo = Strike * Math.Exp(-RiskFreeInterestRate * TimeToOptioneExpiration) * Methods.StandardNormalCDF(_d2);
            return Math.Round((valueOne - valueTwo), 2);
        }
        /// <summary>
        /// Метод подсчета цены опциона Put
        /// </summary>
        /// <returns></returns>
        private double CalculatingPriceOption_Put()
        {
            double valueOne = CurrentPriceOfUnderlyingAsset * Methods.StandardNormalCDF(-_d1);
            double valueTwo = Strike * Math.Exp(-RiskFreeInterestRate * TimeToOptioneExpiration) * Methods.StandardNormalCDF(-_d2);
            return Math.Round((valueTwo - valueOne), 2);
        }
    }
}
