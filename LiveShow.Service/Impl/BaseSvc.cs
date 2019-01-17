using AutoMapper;
using LiveShow.Core.Dto;
using LiveShow.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Impl
{
    public abstract class BaseSvc
    {
        protected LiveShowDBContext _liveShowDB { get; set; }

        protected BaseSvc(LiveShowDBContext liveShowDB)
        {
            _liveShowDB = liveShowDB;
        }

    }
}
