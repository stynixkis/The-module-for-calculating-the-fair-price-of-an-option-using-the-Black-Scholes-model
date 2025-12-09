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
        /// Грек Гамма, подсчитываемый для опциона Call
        /// </summary>
        public double GammaOptionCall { get; set; }
        /// <summary>
        /// Грек Гамма, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _gammaOptionPut;
        /// <summary>
        /// Грек Гамма, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? GammaOptionPut
        {
            get
            {
                if (_gammaOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _gammaOptionPut.Value;
            }
            set { _gammaOptionPut = value; }
        }
        /// <summary>
        /// Грек Вега, подсчитываемый для опциона Call
        /// </summary>
        public double VegaOptionCall { get; set; }
        /// <summary>
        /// Грек Вега, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _vegaOptionPut;
        /// <summary>
        /// Грек Вега, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? VegaOptionPut
        {
            get
            {
                if (_vegaOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _vegaOptionPut.Value;
            }
            set { _vegaOptionPut = value; }
        }
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Call
        /// </summary>
        public double ThetaOptionCall { get; set; }
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _thetaOptionPut;
        /// <summary>
        /// Грек Тета, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? ThetaOptionPut
        {
            get
            {
                if (_thetaOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _thetaOptionPut.Value;
            }
            set { _thetaOptionPut = value; }
        }

        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Call
        /// </summary>
        public double RhoOptionCall { get; set; }
        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Put (приватное поле)
        /// </summary>
        private double? _rhoOptionPut;
        /// <summary>
        /// Грек Ро, подсчитываемый для опциона Put (свойство)
        /// </summary>
        public double? RhoOptionPut
        {
            get
            {
                if (_rhoOptionPut == null)
                {
                    string errorMessage = "ОШИБКА: Греки для Put опциона не рассчитываются для американских опционов";
                    Console.WriteLine(errorMessage);
                    return null;
                }
                else
                    return _rhoOptionPut.Value;
            }
            set { _rhoOptionPut = value; }
        }
        /// <summary>
        /// Конструктор класса, принимающий параметры
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
            GammaOptionCall = CalculatingGamma(d1, currentPriceOfUnderlyingAsset, volatility, timeToOptioneExpiration);
            GammaOptionPut = GammaOptionCall;
            VegaOptionCall = CalculatingVega(d1, currentPriceOfUnderlyingAsset, timeToOptioneExpiration);
            VegaOptionPut = VegaOptionCall;
            ThetaOptionCall = CalculatingTetaCall(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility, d1, d2);
            ThetaOptionPut = CalculatingTetaPut(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility, d1, d2);
            RhoOptionCall = CalculatingRoCall(strike, riskFreeInterestRate, timeToOptioneExpiration, d2);
            RhoOptionPut = CalculatingRoPut(strike, riskFreeInterestRate, timeToOptioneExpiration, d2);
        }
        /// <summary>
        /// Конструктор класса, принимающий параметры
        /// </summary>
        /// <param name="currentPriceOfUnderlyingAsset">Рыночная цена базового актива</param>
        /// <param name="strike">Цена имполнения (страйк)</param>
        /// <param name="riskFreeInterestRate">Безрисковая процентная ставка</param>
        /// <param name="timeToOptioneExpiration">Время до экспирации</param>
        /// <param name="volatility_call">Волатильность для Call опциона</param>
        /// <param name="volatility_put">Волатильность для Put опциона</param>
        /// <param name="d1_call">Коэффициент d1 для опциона Call</param>
        /// <param name="d2_call">Коэффициент d2 для опциона Call</param>
        /// <param name="d1_put">Коэффициент d1 для опциона Put</param>
        /// <param name="d2_put">Коэффициент d2 для опциона Put</param>
        public CalculatingGreeks(double currentPriceOfUnderlyingAsset, double strike,
            double riskFreeInterestRate, double timeToOptioneExpiration, double volatility_call, double volatility_put,
            double d1_call, double d2_call, double d1_put, double d2_put)
        {
            DeltaOptionCall = CalculatingDeltaCall(d1_call);
            DeltaOptionPut = CalculatingDeltaPut(d1_put);
            GammaOptionCall = CalculatingGamma(d1_call, currentPriceOfUnderlyingAsset, volatility_call, timeToOptioneExpiration);
            GammaOptionPut = CalculatingGamma(d1_put, currentPriceOfUnderlyingAsset, volatility_put, timeToOptioneExpiration);
            VegaOptionCall = CalculatingVega(d1_call, currentPriceOfUnderlyingAsset, timeToOptioneExpiration);
            VegaOptionPut = CalculatingVega(d1_put, currentPriceOfUnderlyingAsset, timeToOptioneExpiration);
            ThetaOptionCall = CalculatingTetaCall(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility_call, d1_call, d2_call);
            ThetaOptionPut = CalculatingTetaPut(currentPriceOfUnderlyingAsset, strike, riskFreeInterestRate,
                timeToOptioneExpiration, volatility_put, d1_put, d2_put);
            RhoOptionCall = CalculatingRoCall(strike, riskFreeInterestRate, timeToOptioneExpiration, d2_call);
            RhoOptionPut = CalculatingRoPut(strike, riskFreeInterestRate, timeToOptioneExpiration, d2_put);
        }

        /// <summary>
        /// Конструктор класса, принимающий экземпляр класса для подсчета европейского опциона
        /// </summary>
        /// <param name="europeanOption">Экземпляр класса CalculatingFairPriceOfEuropeanOption</param>
        public CalculatingGreeks(CalculatingFairPriceOfEuropeanOption europeanOption)
        {
            DeltaOptionCall = CalculatingDeltaCall(europeanOption.D1);
            DeltaOptionPut = CalculatingDeltaPut(europeanOption.D1);
            GammaOptionCall = CalculatingGamma(europeanOption.D1, europeanOption.CurrentPriceOfUnderlyingAsset, europeanOption.Volatility, europeanOption.TimeToOptioneExpiration);
            GammaOptionPut = GammaOptionCall;
            VegaOptionCall = CalculatingVega(europeanOption.D1, europeanOption.CurrentPriceOfUnderlyingAsset, europeanOption.TimeToOptioneExpiration);
            VegaOptionPut = VegaOptionCall;
            ThetaOptionCall = CalculatingTetaCall(europeanOption.CurrentPriceOfUnderlyingAsset, europeanOption.Strike, europeanOption.RiskFreeInterestRate,
                europeanOption.TimeToOptioneExpiration, europeanOption.Volatility, europeanOption.D1, europeanOption.D2);
            ThetaOptionPut = CalculatingTetaPut(europeanOption.CurrentPriceOfUnderlyingAsset, europeanOption.Strike, europeanOption.RiskFreeInterestRate,
                europeanOption.TimeToOptioneExpiration, europeanOption.Volatility, europeanOption.D1, europeanOption.D2);
            RhoOptionCall = CalculatingRoCall(europeanOption.Strike, europeanOption.RiskFreeInterestRate, europeanOption.TimeToOptioneExpiration, europeanOption.D2);
            RhoOptionPut = CalculatingRoPut(europeanOption.Strike, europeanOption.RiskFreeInterestRate, europeanOption.TimeToOptioneExpiration, europeanOption.D2);
        }
        /// <summary>
        /// Конструктор класса, принимающий экземпляр класса для подсчета американского опциона
        /// </summary>
        /// <param name="americanOption">Экземпляр класса CalculatingFairPriceOfAmericanOption</param>
        public CalculatingGreeks(CalculatingFairPriceOfAmericanOption americanOption)
        {
            DeltaOptionCall = CalculatingDeltaCall(americanOption.D1);
            DeltaOptionPut = null;
            GammaOptionCall = CalculatingGamma(americanOption.D1, americanOption.CurrentPriceOfUnderlyingAsset, americanOption.Volatility, americanOption.TimeToOptioneExpiration);
            GammaOptionPut = null;
            VegaOptionCall = CalculatingVega(americanOption.D1, americanOption.CurrentPriceOfUnderlyingAsset, americanOption.TimeToOptioneExpiration);
            VegaOptionPut = null;
            ThetaOptionCall = CalculatingTetaCall(americanOption.CurrentPriceOfUnderlyingAsset, americanOption.Strike, americanOption.RiskFreeInterestRate,
                americanOption.TimeToOptioneExpiration, americanOption.Volatility, americanOption.D1, americanOption.D2);
            ThetaOptionPut = null;
            RhoOptionCall = CalculatingRoCall(americanOption.Strike, americanOption.RiskFreeInterestRate, americanOption.TimeToOptioneExpiration, americanOption.D2);
            RhoOptionPut = null;
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
