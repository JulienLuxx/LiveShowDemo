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
        protected readonly IMapper _mapper;
        protected LiveShowDBContext _liveShowDB { get; set; }

        protected BaseSvc(IMapper mapper, LiveShowDBContext liveShowDB)
        {
            _mapper = mapper;
            _liveShowDB = liveShowDB;
        }

    }
}
