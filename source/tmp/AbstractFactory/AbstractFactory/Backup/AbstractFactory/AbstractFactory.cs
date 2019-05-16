using System;
using System.Reflection;

namespace AbstractFactory
{
	/// <summary>
	/// Factory¿‡
	/// </summary>
	public abstract class AbstractFactory
	{
//		public AbstractFactory GetInstance()
//		{
//			string factoryName = Constant.STR_FACTORYNAME.ToString();
//
//			AbstractFactory instance;
//
//			if(factoryName == "ChineseFactory")
//				instance = new ChineseFactory();
//			else if(factoryName == "AmericanFactory")
//				instance = new AmericanFactory();
//			else
//				instance = null;
//
//			return instance;
//		}

		public AbstractFactory GetInstance()
		{
			string factoryName = Constant.STR_FACTORYNAME.ToString();

			AbstractFactory instance;

			if(factoryName != "")
				instance = (AbstractFactory)Assembly.Load(factoryName).CreateInstance(factoryName);
			else
				instance = null;

			return instance;
		}


		public abstract Tax CreateTax();

		public abstract Bonus CreateBonus();
	}
}
