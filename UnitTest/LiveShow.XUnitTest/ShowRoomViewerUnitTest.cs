using LiveShow.Domain;
using LiveShow.Domain.Entitis;
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
        public ShowRoomViewerUnitTest()
        {
            _sampleList = new List<ShowRoomViewer>()
            {
                new ShowRoomViewer()
                {
                    ShowRoomId=1,UserId=1
                },
            };
        }

        [Fact]
        public async Task AddTest()
        { }

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
