
using System;

namespace AbstractFactory
{
	/// <summary>
	/// �ͻ��˳������
	/// </summary>
	public class Calculator 
	{
		public static void Main(string[] args) 
		{
			Bonus bonus = new ChineseBonus();
			double bonusValue  = bonus.Calculate();
	
			Tax tax = new ChineseTax();
			double taxValue = tax.Calculate();
	
			double salary = 4000 + bonusValue - taxValue; 

			Console.WriteLine("Salary is��" + salary);
			Console.ReadLine();
		}
	}
}
