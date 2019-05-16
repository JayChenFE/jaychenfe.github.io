
using System;

namespace ChineseSalary
{
	/// <summary>
	/// 客户端程序调用
	/// </summary>
	public class Calculator 
	{
		public static void Main(string[] args) 
		{
			ChineseBonus bonus = new ChineseBonus();
			double bonusValue  = bonus.Calculate();
	
			ChineseTax tax = new ChineseTax();
			double taxValue = tax.Calculate();
	
			double salary = 4000 + bonusValue - taxValue; 

			Console.WriteLine("Chinaese Salary is：" + salary);
			Console.ReadLine();
		}
	}
}
