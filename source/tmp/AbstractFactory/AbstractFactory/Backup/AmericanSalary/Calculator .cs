
using System;

namespace AmericanSalary
{
	/// <summary>
	/// 客户端程序调用
	/// </summary>
	public class Calculator 
	{
		public static void Main(string[] args) 
		{
			AmericanBonus bonus = new AmericanBonus();
			double bonusValue  = bonus.Calculate();
	
			AmericanTax tax = new AmericanTax();
			double taxValue = tax.Calculate();
	
			double salary = 4000 + bonusValue - taxValue; 

			Console.WriteLine("American Salary is：" + salary);
			Console.ReadLine();
		}
	}
}
