using System;

namespace InterfaceSalary
{
    /// <summary>
    /// �����й���������˰
    /// </summary>
    public class ChineseTax : Tax
    {
        public override double Calculate()
        {
            return (Constant.BaseSalary + (Constant.BaseSalary * 0.1)) * 0.4;
        }
    }
}
