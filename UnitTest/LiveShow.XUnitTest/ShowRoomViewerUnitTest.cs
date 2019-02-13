using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Domain.Enum;
using LiveShow.Service.Dto;
using LiveShow.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoqEFCoreExtension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiveShow.XUnitTest
{
    public class ShowRoomViewerUnitTest
    {
        private List<ShowRoomViewer> _sampleList;
        private List<ShowRoom> _sampleRoomList;
        public ShowRoomViewerUnitTest()
        {
            _sampleList = new List<ShowRoomViewer>()
            {
                new ShowRoomViewer()
                {
                    ShowRoomId=1,UserId=1
                },
            };
            _sampleRoomList = new List<ShowRoom>()
            {
                new ShowRoom() { Id=1,Name="Test1",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Default.GetHashCode(),Title="Test1Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=1,ShowRoomId=1,CreateTime=DateTime.Now} } },
                new ShowRoom() { Id=2,Name="Test2",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Default.GetHashCode(),Title="Test2Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=2,ShowRoomId=2,CreateTime=DateTime.Now} }  },
                new ShowRoom() { Id=3,Name="Test3",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Activate.GetHashCode(),Title="Test3Title",ShowRoomVlewers=new List<ShowRoomViewer>(){ new ShowRoomViewer() { UserId=3,ShowRoomId=3,CreateTime=DateTime.Now} }  },
                new ShowRoom() { Id=4,Name="Test4",IsDeleted=false,CreateTime=DateTime.Now,LastActivateTime=DateTime.Now,Status=ShowRoomStatusEnum.Disable.GetHashCode(),Title="Test4Title"/*,ShowRoomVlewers=new List<ShowRoomVlewer>(){ new ShowRoomVlewer() { UserId=4,ShowRoomId=4,CreateTime=DateTime.Now} } */ }
            };
        }

        [Fact]
        public async Task AddTest()
        {
            var mockSet = new Mock<DbSet<ShowRoomViewer>>().SetupList(_sampleList);
            var mockRoomSet = new Mock<DbSet<ShowRoom>>().SetupList(_sampleRoomList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoomViewer).Returns(mockSet.Object);
            mockContext.Setup(x => x.ShowRoom).Returns(mockRoomSet.Object);
            var mockSvc = new ShowRoomViewerSvc(mockContext.Object);

            var dto = new ShowRoomViewerDto() { UserId = 2, ShowRoomId = 2 };
            await mockSvc.Add(dto);

            mockSet.Verify(x => x.Add(It.IsAny<ShowRoomViewer>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task RemoveTest()
        {
            var mockSet = new Mock<DbSet<ShowRoomViewer>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.ShowRoomViewer).Returns(mockSet.Object);
            var mockSvc = new ShowRoomViewerSvc(mockContext.Object);

            await mockSvc.Remove(new ShowRoomViewerDto() { UserId = 1, ShowRoomId = 1 });

            mockSet.Verify(x => x.Remove(It.IsAny<ShowRoomViewer>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
