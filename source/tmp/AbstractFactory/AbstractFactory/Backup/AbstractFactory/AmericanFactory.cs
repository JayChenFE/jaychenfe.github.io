using System;

namespace AbstractFactory
{
	/// <summary>
	/// AmericanFactory¿‡
	/// </summary>
	public class AmericanFactory:AbstractFactory
	{
		public override Tax CreateTax()
		{
			return new ChineseTax();
		}

		public override Bonus CreateBonus()
		{
			return new ChineseBonus();
		}
	}
}
