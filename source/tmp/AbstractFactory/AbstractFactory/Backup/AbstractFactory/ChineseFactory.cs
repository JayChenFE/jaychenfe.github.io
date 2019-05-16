using System;

namespace AbstractFactory
{
	/// <summary>
	/// ChineseFactory¿‡
	/// </summary>
	public class ChineseFactory:AbstractFactory
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
