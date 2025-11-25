namespace Module.Black_Shoals.Service
{
    public static class Methods
    {
        public static double Normalization(double value)
        {
            //coefficient1-5 - коэффициенты полиномиальной аппроксимации
            double coefficient1 = 0.254829592;
            double coefficient2 = -0.284496736;
            double coefficient3 = 1.421413741;
            double coefficient4 = -1.453152027;
            double coefficient5 = 1.061405429;
            //approximationConstant - константа для аппроксимации
            double approximationConstant = 0.3275911;

            //sign - знак исходного значения
            int sign = 1;
            if (value < 0)
                sign = -1;

            //нормированное значение
            value = Math.Abs(value) / Math.Sqrt(2.0);

            double temp = 1.0 / (1.0 + approximationConstant * value);
            //errorFunction - значение функции ошибок
            double errorFunction = 1.0 - (((((coefficient5 * temp + coefficient4) * temp) + coefficient3)
                * temp + coefficient2) * temp + coefficient1) * temp * Math.Exp(-value * value);

            return 0.5 * (1.0 + sign * errorFunction);
        }

        public static double NormalizationDerivative(double value)
        {
            double valueOne = 1 / (Math.Sqrt(2 * Math.PI));
            double valueTwo = Math.Exp(Math.Pow(-value, 2) / 2);
            return valueOne * valueTwo;
        }
    }
}
