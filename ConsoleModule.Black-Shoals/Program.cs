using Module.Black_Shoals.Classes;

//CalculatingFairPriceOfEuropeanOption newOption = new CalculatingFairPriceOfEuropeanOption(38.6459, 40, 0.1, 0.5, 0.3);
//PrintEurop(newOption);
CalculatingFairPriceOfAmericanOption newOption = new CalculatingFairPriceOfAmericanOption(40, 40, 0.1, 0.5, 0.3, [0.7, 0.7], [(2.0 / 12), (5.0 / 12)]);
PrintAmerica(newOption);

void PrintEurop(CalculatingFairPriceOfEuropeanOption newOption)
{
    Console.WriteLine("\nS = " + newOption.CurrentPriceOfUnderlyingAsset);
    Console.WriteLine("K = " + newOption.Strike);
    Console.WriteLine("r = " + newOption.RiskFreeInterestRate);
    Console.WriteLine("T = " + newOption.TimeToOptioneExpiration);
    Console.WriteLine("o` = " + newOption.Volatility);

    Console.WriteLine("\nC = " + newOption.PriceOptionCall);
    Console.WriteLine("P = " + newOption.PriceOptionPut);

    Console.WriteLine("\nDelta C = " + newOption.GreeksValue.DeltaOptionCall);
    Console.WriteLine("Delta P = " + newOption.GreeksValue.DeltaOptionPut);
    Console.WriteLine("Gamma = " + newOption.GreeksValue.Gamma);
    Console.WriteLine("Vega = " + newOption.GreeksValue.Vega);
    Console.WriteLine("Teta C = " + newOption.GreeksValue.TetaOptionCall);
    Console.WriteLine("Teta P = " + newOption.GreeksValue.TetaOptionPut);
    Console.WriteLine("Ro C = " + newOption.GreeksValue.RoOptionCall);
    Console.WriteLine("Ro P = " + newOption.GreeksValue.RoOptionPut);
}

void PrintAmerica(CalculatingFairPriceOfAmericanOption newOption)
{
    Console.WriteLine("\nS = " + newOption.CurrentPriceOfUnderlyingAsset);
    Console.WriteLine("K = " + newOption.Strike);
    Console.WriteLine("r = " + newOption.RiskFreeInterestRate);
    Console.WriteLine("T = " + newOption.TimeToOptioneExpiration);
    Console.WriteLine("o` = " + newOption.Volatility);

    Console.WriteLine("\nРазмеры дивидендов = [" + string.Join(", ", newOption.Dividends) + "]");
    Console.WriteLine("Сроки до выплаты = [" + string.Join(", ", newOption.DividendTimes) + "]");

    Console.WriteLine("\nC = " + newOption.PriceOptionCall);

    Console.WriteLine("\nDelta C = " + newOption.GreeksValue.DeltaOptionCall);
    Console.WriteLine("Gamma = " + newOption.GreeksValue.Gamma);
    Console.WriteLine("Vega = " + newOption.GreeksValue.Vega);
    Console.WriteLine("Teta C = " + newOption.GreeksValue.TetaOptionCall);
    Console.WriteLine("Ro C = " + newOption.GreeksValue.RoOptionCall);
}