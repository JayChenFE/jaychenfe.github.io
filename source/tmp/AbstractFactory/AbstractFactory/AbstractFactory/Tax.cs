using System;

namespace AbstractFactory
{
	/// <summary>
	/// 个人所得税抽象类
	/// </summary>
	public abstract class Tax
	{
		public abstract double Calculate();
	}
}
