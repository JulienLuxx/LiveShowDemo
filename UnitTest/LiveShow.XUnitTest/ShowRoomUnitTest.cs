using AutoMapper;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Domain.Enum;
using LiveShow.Service.Dto;
using LiveShow.Service.Impl;
using LiveShow.Service.Infrastructure;
using LiveShow.Service.QueryModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoqEFCoreExtension;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new ShowRoom() { Id=1,Name="Test1",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Default.GetHashCode(),Title="Test1Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=1,ShowRoomId=1,CreateTime=DateTime.Now} } },
                new ShowRoom() { Id=2,Name="Test2",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Default.GetHashCode(),Title="Test2Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=2,ShowRoomId=2,CreateTime=DateTime.Now} }  },
                new ShowRoom() { Id=3,Name="Test3",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Activate.GetHashCode(),Title="Test3Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=3,ShowRoomId=3,CreateTime=DateTime.Now} }  },
                new ShowRoom() { Id=4,Name="Test4",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Disable.GetHashCode(),Title="Test4Title"/*,ShowRoomVlewers=new List<ShowRoomVlewer>(){ new ShowRoomVlewer() { UserId=4,ShowRoomId=4,CreateTime=DateTime.Now} } */ }
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
        public async Task ActivateTest()
        {
            var mockSet = new Mock<DbSet<ShowRoom>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoom).Returns(mockSet.Object);
            var mockSvc = new ShowRoomSvc(mockContext.Object);
            await mockSvc.Activate(4);

            var data = mockContext.Object.ShowRoom.Where(x => x.Id == 4).FirstOrDefault();
            Assert.Equal(ShowRoomStatusEnum.Activate.GetHashCode(), data.Status);
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task GetPageDataTest()
        {
            var mockSet = new Mock<DbSet<ShowRoom>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoom).Returns(mockSet.Object);
            var mockSvc = new ShowRoomSvc(mockContext.Object);
            var result = await mockSvc.GetPageDataAsync(new ShowRoomQueryModel());
            Assert.Equal(4, result.List.Count());
        }

        [Fact]
        public async Task GetSingleDataByUserIdTest()
        {
            Mapper.Initialize(x => x.AddProfile<CustomizeProfile>());
            var mockSet = new Mock<DbSet<ShowRoom>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoom).Returns(mockSet.Object);
            var mockSvc = new ShowRoomSvc(mockContext.Object);
            var result = await mockSvc.GetSingleDataByUserIdAsync(1);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task ShutdownTest()
        {
            var mockSet = new Mock<DbSet<ShowRoom>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoom).Returns(mockSet.Object);
            var mockSvc = new ShowRoomSvc(mockContext.Object);
            await mockSvc.Shutdown(3);

            var data = mockContext.Object.ShowRoom.Where(x => x.Id == 3).FirstOrDefault();
            Assert.Equal(ShowRoomStatusEnum.Disable.GetHashCode(), data.Status);
            Assert.True(data.ShowRoomVlewers.Count() == 0);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
