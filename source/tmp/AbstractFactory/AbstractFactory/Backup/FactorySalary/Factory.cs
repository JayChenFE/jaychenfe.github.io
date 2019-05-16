using System;

namespace FactorySalary
{
	/// <summary>
	/// Factory¿‡
	/// </summary>
	public class Factory
	{
		public Tax CreateTax()
		{
			return new ChineseTax();
		}

		public Bonus CreateBonus()
		{
			return new ChineseBonus();
		}
	}
}
