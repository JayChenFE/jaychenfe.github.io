using System;

namespace AmericanSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            AmericanBonus bonus = new AmericanBonus();
            double bonusValue = bonus.Calculate();

            AmericanTax tax = new AmericanTax();
            double taxValue = tax.Calculate();

            double salary = Constant.BaseSalary + bonusValue - taxValue;

            Console.WriteLine("American Salary is：" + salary);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 公用的常量
    /// </summary>
    public class Constant
    {
        public static double BaseSalary = 4000;
    }

    /// <summary>
    /// 计算美国个人所得税
    /// </summary>
    public class AmericanTax
    {
        public double Calculate()
        {
            return Constant.BaseSalary*0.05 + (Constant.BaseSalary * 0.15*0.25);
        }
    }

    /// <summary>
    /// 计算美国个人奖金
    /// </summary>
    public class AmericanBonus
    {
        public double Calculate()
        {
            return Constant.BaseSalary * 0.15;
        }
    }
}
