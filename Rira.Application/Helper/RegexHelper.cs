using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Application.Helper;

public static class RegexHelper
{
    // National code: exactly 10 digits
    public const string NationalCodeRegex = @"^\d{10}$";

    // First name / Last name: only letters, max 100 characters
    // You can adjust \p{L} to include letters in all languages
    public const string NameRegex = @"^[\p{L}]{1,100}$";
}
