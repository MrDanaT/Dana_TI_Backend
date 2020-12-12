using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.UI
{
    public static class Validations
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
