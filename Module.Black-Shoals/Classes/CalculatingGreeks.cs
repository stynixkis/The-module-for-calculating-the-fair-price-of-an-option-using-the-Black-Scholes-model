using Module.Black_Shoals.Service;

namespace Module.Black_Shoals.Classes
{
    /// <summary>
    /// Класс для подсчета греков
    /// </summary>
    public class CalculatingGreeks
    {
        /// <summary>
        /// Грек Дельта, подсчитываемый для опциона Call
        /// </summary>
        public double DeltaOptionCall { get; set; }
        /// <summary>
        /// Грек Дельта, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _deltaOptionPut;
        /// <summary>
        /// Грек Дельта, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? DeltaOptionPut
        {
            get
            {
                if (_deltaOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _deltaOptionPut.Value;
            }
            set { _deltaOptionPut = value; }
        }
        /// <summary>
        /// Грек Гамма, подсчитываемый для опциона Call и Put
        /// </summary>
        public double Gamma { get; set; }
        /// <summary>
        /// Грек Вега, подсчитываемый для опциона Call и Put
        /// </summary>
        public double Vega { get; set; }
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Call
        /// </summary>
        public double TetaOptionCall { get; set; }
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _tetaOptionPut;
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? TetaOptionPut
        {
            get
            {
                if (_tetaOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _tetaOptionPut.Value;
            }
            set { _tetaOptionPut = value; }
        }

        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Call
        /// </summary>
        public double RoOptionCall { get; set; }
        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _roOptionPut;
        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? RoOptionPut
        {
            get
            {
                if (_roOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _roOptionPut.Value;
            }
            set { _roOptionPut = value; }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="volatility">Волатильность</param>
        /// <param name="d1">Коэффициент d1</param>
        /// <param name="d2">Коэффициент d2</param>
        public CalculatingGreeks(double currentPriceOfUnderlyingAsset, double strike,
            double riskFreeInterestRate, double timeToOptioneExpiration, double volatility,
            double d1, double d2)
        {
            DeltaOptionCall = CalculatingDeltaCall(d1);
            DeltaOptionPut = CalculatingDeltaPut(d1);
            Gamma = CalculatingGamma(d1, currentPriceOfUnderlyingAsset, volatility, timeToOptioneExpiration);
            Vega = CalculatingVega(d1, currentPriceOfUnderlyingAsset, timeToOptioneExpiration);
            TetaOptionCall = CalculatingTetaCall(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility, d1, d2);
            TetaOptionPut = CalculatingTetaPut(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility, d1, d2);
            RoOptionCall = CalculatingRoCall(strike, riskFreeInterestRate, timeToOptioneExpiration, d2);
            RoOptionPut = CalculatingRoPut(strike, riskFreeInterestRate, timeToOptioneExpiration, d2);
        }
        /// <summary>
        /// Метод подсчета грека Дельта для опциона Call
        /// </summary>
        /// <param name="value">Коэффициент d1</param>
        /// <returns></returns>
        private double CalculatingDeltaCall(double value)
        {
            return Math.Round(Methods.StandardNormalCDF(value), 4);
        }
        /// <summary>
        /// Метод подсчета грека Дельта для опциона Put
        /// </summary>
        /// <param name="value">Коэффициент d1</param>
        /// <returns></returns>
        private double CalculatingDeltaPut(double value)
        {
            return Math.Round((Methods.StandardNormalCDF(value) - 1), 4);
        }
        /// <summary>
        /// Метод подсчета грека Гамма
        /// </summary>
        /// <param name="d1">Коэффициент d1</param>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="volatility">Волатильность</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <returns></returns>
        private double CalculatingGamma(double d1, double currentPriceOfUnderlyingAsset,
            double volatility, double timeToOptioneExpiration)
        {
            double valueOne = Methods.StandardNormalCDFDerivative(d1);
            double valueTwo = currentPriceOfUnderlyingAsset * volatility * Math.Sqrt(timeToOptioneExpiration);
            return Math.Round((valueOne / valueTwo), 4);
        }
        /// <summary>
        /// Метод подсчета грека Вега
        /// </summary>
        /// <param name="d1">Коэффициент d1</param>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <returns></returns>
        private double CalculatingVega(double d1, double currentPriceOfUnderlyingAsset,
            double timeToOptioneExpiration)
        {
            return Math.Round((currentPriceOfUnderlyingAsset * Methods.StandardNormalCDFDerivative(d1) * Math.Sqrt(timeToOptioneExpiration)), 4);
        }
        /// <summary>
        /// Метод подсчета грека Тета для опциона Call
        /// </summary>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="volatility">Волатильность</param>
        /// <param name="d1">Коэффициент d1</param>
        /// <param name="d2">Коэффициент d2</param>
        /// <returns></returns>
        private double CalculatingTetaCall(double currentPriceOfUnderlyingAsset, double strike,
            double riskFreeInterestRate, double timeToOptioneExpiration, double volatility,
            double d1, double d2)
        {
            double valueOne = currentPriceOfUnderlyingAsset * Methods.StandardNormalCDFDerivative(d1) * volatility;
            double valueTwo = valueOne / (2 * Math.Sqrt(timeToOptioneExpiration));
            double valueThree = riskFreeInterestRate * strike * Math.Exp(-riskFreeInterestRate * timeToOptioneExpiration) * Methods.StandardNormalCDF(d2);
            return Math.Round((-valueTwo - valueThree), 4);
        }
        /// <summary>
        /// Метод подсчета грека Тета для опциона Put
        /// </summary>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="volatility">Волатильность</param>
        /// <param name="d1">Коэффициент d1</param>
        /// <param name="d2">Коэффициент d2</param>
        /// <returns></returns>
        private double CalculatingTetaPut(double currentPriceOfUnderlyingAsset, double strike,
            double riskFreeInterestRate, double timeToOptioneExpiration, double volatility,
            double d1, double d2)
        {
            double valueOne = currentPriceOfUnderlyingAsset * Methods.StandardNormalCDFDerivative(d1) * volatility;
            double valueTwo = valueOne / (2 * Math.Sqrt(timeToOptioneExpiration));
            double valueThree = riskFreeInterestRate * strike * Math.Exp(-riskFreeInterestRate * timeToOptioneExpiration) * Methods.StandardNormalCDF(-d2);
            return Math.Round((-valueTwo + valueThree), 4);
        }
        /// <summary>
        /// Метод подсчета грека Ро для опциона Call
        /// </summary>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="d2">Коэффициент d2</param>
        /// <returns></returns>
        private double CalculatingRoCall(double strike, double riskFreeInterestRate, double timeToOptioneExpiration, double d2)
        {
            return Math.Round((strike * timeToOptioneExpiration * Math.Exp(-riskFreeInterestRate * timeToOptioneExpiration) * Methods.StandardNormalCDF(d2)), 4);
        }
        /// <summary>
        /// Метод подсчета грека Ро для опциона Put
        /// </summary>
        /// <param name="strike">Цена исполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="d2">Коэффициент d2</param>
        /// <returns></returns>
        private double CalculatingRoPut(double strike, double riskFreeInterestRate, double timeToOptioneExpiration, double d2)
        {
            return Math.Round((-strike * timeToOptioneExpiration * Math.Exp(-riskFreeInterestRate * timeToOptioneExpiration) * Methods.StandardNormalCDF(-d2)), 4);
        }
    }
}
