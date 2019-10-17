using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAtividadeEntrevista.Utils.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CpfValidation : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var cpf = (String)value;
            bool result = true;
            if (cpf != null)
            {
                string only_number = String.Join("", Regex.Split(cpf, @"[^\d]"));
                bool equal_numbers = Regex.IsMatch(only_number, @"^(\d)\1{10}$");
                if (equal_numbers)
                {
                    result = false;
                } else
                {
                    for (var t = 9; t < 11; t++)
                    {
                        var d = 0;
                        var c = 0;
                        while (c < t)
                        {
                            d += int.Parse(only_number[c].ToString()) * ((t + 1) - c);
                            c++;
                        }
                        d = ((10 * d) % 11) % 10;
                        if (int.Parse(only_number[c].ToString()) != d)
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
    }
}