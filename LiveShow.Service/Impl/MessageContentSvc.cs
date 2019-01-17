using System;
using System.Collections.Generic;
using System.Text;
using LiveShow.Domain;

namespace LiveShow.Service.Impl
{
    public class MessageContentSvc : BaseSvc
    {
        public MessageContentSvc(LiveShowDBContext liveShowDB) : base(liveShowDB)
        {
        }
    }
}
