using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace BuzzCurrency.Library.Enums
{
    public enum UserType
    {
        [Description("Guest")]
        Unconfirmed,

        [Description("General User")]
        Confirmed,

        [Description("Genuine User")]
        Genuine,

        [Description("VIP")]
        VIP,

        [Description("VVIP")]
        VVIP,

        [Description("Banned")]
        Banned
    }
}


