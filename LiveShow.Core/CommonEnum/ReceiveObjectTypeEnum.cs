using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LiveShow.Core.CommonEnum
{
    public enum ReceiveObjectTypeEnum
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default,
        /// <summary>
        /// Group(ShowRoom)
        /// </summary>
        [Description("Group")]
        Group,
        /// <summary>
        /// Individual(SingleUser)
        /// </summary>
        [Description("Individual")]
        Individual
    }
}
