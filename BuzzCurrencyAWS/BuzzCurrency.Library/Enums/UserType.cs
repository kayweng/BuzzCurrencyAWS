using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace BuzzCurrency.Library.Enums
{
    public enum UserType
    {
        [Description("Unconfirmed User")]
        Unconfirmed,

        [Description("Confirmed User")]
        Confirmed,

        [Description("Genuine User")]
        Genuine,

        [Description("VIP User")]
        VIP,

        [Description("VVIP User")]
        VVIP,

        [Description("Banned User")]
        Banned
    }
}


