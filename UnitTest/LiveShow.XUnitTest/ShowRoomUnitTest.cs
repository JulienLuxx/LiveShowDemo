using AutoMapper;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Domain.Enum;
using LiveShow.Service.Dto;
using LiveShow.Service.Impl;
using LiveShow.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiveShow.XUnitTest
{
    public class ShowRoomUnitTest
    {
        private List<ShowRoom> _sampleList;
        public ShowRoomUnitTest()
        {
            _sampleList = new List<ShowRoom>()
            {
            };
        }
        [Fact]
        public void AddTest()
        {
            Mapper.Initialize(x => x.AddProfile<CustomizeProfile>());
            var mockSet = new Mock<DbSet<ShowRoom>>();
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoom).Returns(mockSet.Object);
            var mockSvc = new ShowRoomSvc(mockContext.Object);

            var dto = new ShowRoomDto() { Name = "Test", IsDeleted = false, Status = ShowRoomStatusEnum.Disable.GetHashCode(), CreateTime = DateTime.Now, LastActivateTime = DateTime.Now, Title = "TestTitle" };
            mockSvc.Add(dto);

            mockContext.Verify(x => x.Add(It.IsAny<ShowRoom>()), Times.Once);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task GetPageDataTest()
        {
        }
    }
}
