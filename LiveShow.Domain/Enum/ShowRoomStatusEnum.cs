using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LiveShow.Domain.Enum
{
    public enum ShowRoomStatusEnum
    {
        [Description("Default")]
        Default,

        [Description("Activate")]
        Activate,

        [Description("Disable")]
        Disable,

        [Description("Ban")]
        Ban
    }
}
