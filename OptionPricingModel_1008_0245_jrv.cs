// 代码生成时间: 2025-10-08 02:45:29
 * Author: [Your Name]
 * Date: [Today's Date]
 */

using System;

namespace OptionPricingApp
{
    public class OptionPricingModel
    {
        // Converts the year fraction to the appropriate time value for the Black-Scholes model.
        private const double RiskFreeRate = 0.05; // Annual risk-free interest rate.
        private const double Volatility = 0.20; // Annual volatility.

        /// <summary>
        /// Calculates the price of a European call option using the Black-Scholes model.
        /// </summary>
        /// <param name="S">The current stock price.</param>
        /// <param name="K">The strike price of the option.</param>
        /// <param name="T">The time to expiration in years.</param>
        /// <param name="r">The risk-free interest rate.</param>
        /// <param name="sigma">The volatility of the stock.</param>
        /// <returns>The price of the call option.</returns>
        public double CalculateCallOptionPrice(double S, double K, double T, double r = RiskFreeRate, double sigma = Volatility)
        {
            if (S < 0 || K < 0 || T < 0 || r < 0 || sigma <= 0)
            {
                throw new ArgumentException("Input values must be positive.");
            }

            double sqrtT = Math.Sqrt(T);
            double d1 = (Math.Log(S / K) + (r + (sigma * sigma) / 2) * T) / (sigma * sqrtT);
            double d2 = d1 - sigma * sqrtT;

            double callOptionPrice = S * BlackScholesCumulativeNormal(d1) - K * Math.Exp(-r * T) * BlackScholesCumulativeNormal(d2);

            return callOptionPrice;
        }

        /// <summary>
        /// Calculates the price of a European put option using the Black-Scholes model.
        /// </summary>
        /// <param name="S">The current stock price.</param>
        /// <param name="K">The strike price of the option.</param>
        /// <param name name="T">The time to expiration in years.</param>
        /// <param name name="r">The risk-free interest rate.</param>
        /// <param name name="sigma">The volatility of the stock.</param>
        /// <returns>The price of the put option.</returns>
        public double CalculatePutOptionPrice(double S, double K, double T, double r = RiskFreeRate, double sigma = Volatility)
        {
            if (S < 0 || K < 0 || T < 0 || r < 0 || sigma <= 0)
            {
                throw new ArgumentException("Input values must be positive.");
            }

            double sqrtT = Math.Sqrt(T);
            double d1 = (Math.Log(S / K) + (r + (sigma * sigma) / 2) * T) / (sigma * sqrtT);
            double d2 = d1 - sigma * sqrtT;

            double putOptionPrice = K * Math.Exp(-r * T) * BlackScholesCumulativeNormal(-d2) - S * BlackScholesCumulativeNormal(-d1);

            return putOptionPrice;
        }

        // Approximation of the cumulative distribution function of the standard normal distribution.
        private double BlackScholesCumulativeNormal(double x)
        {
            double a1 = 0.31938152;
            double a2 = -0.356563782;
            double a3 = 1.781477937;
            double a4 = -1.821255978;
            double a5 = 1.330274429;
            double p = 0.231639776;
            double t = 1.0 / (1.0 + p * Math.Abs(x));
            double b1 = a1 * t;
            double b2 = a2 * t * t;
            double b3 = a3 * t * t * t;
            double b4 = a4 * t * t * t * t;
            double b5 = a5 * t * t * t * t * t;
            double cumulative = (1.0 - Math.Pow(t, 15) * (b5 + b4 + b3 + b2 + b1)) / 2.0;
            return cumulative;
        }
    }
}
