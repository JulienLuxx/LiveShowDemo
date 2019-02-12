using AutoMapper;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Service.Dto;
using LiveShow.Service.Impl;
using LiveShow.Service.Infrastructure;
using LiveShow.Service.QueryModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoqEFCoreExtension;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LiveShow.XUnitTest
{
    public class RoleUnitTest
    {
        private List<Role> _sampleList;
        private RoleDto _sampleDto; 
        public RoleUnitTest()
        {
            _sampleList= new List<Role>()
            {
                new Role(){ Id=1,Name="Test1",CreateTime=DateTime.Now,IsDeleted=false,Status=0 },
                new Role(){ Id=2,Name="Test2",CreateTime=DateTime.Now,IsDeleted=false,Status=0 }
            };
            //Mapper.Initialize(x => x.AddProfile<CustomizeProfile>());
        }

        [Fact]
        public void AddTest()
        {
            Mapper.Initialize(x => x.AddProfile<CustomizeProfile>());
            var mockSet = new Mock<DbSet<Role>>();
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.Role).Returns(mockSet.Object);
            var mockSvc = new RoleSvc(mockContext.Object);

            var dto = new RoleDto() { Name = "Test", IsDeleted = false, CreateTime = DateTime.Now, Status = 1 };
            mockSvc.Add(dto);

            mockContext.Verify(x => x.Add(It.IsAny<Role>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task EditTest()
        {
            var mockSet = new Mock<DbSet<Role>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.Role).Returns(mockSet.Object);
            var mockSvc = new RoleSvc(mockContext.Object);
            var dto = new RoleDto() { Id = 1, Name = "Test1.5", CreateTime = DateTime.Now, IsDeleted = false, Status = 1 };
            await mockSvc.Edit(dto);

            mockSet.Verify(x => x.Update(It.IsAny<Role>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task GetPageDataTest()
        {
            var mockSet = new Mock<DbSet<Role>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.Role).Returns(mockSet.Object);
            var mockSvc = new RoleSvc(mockContext.Object);
            var result = await mockSvc.GetPageDataAsync(new RoleQueryModel());
            Assert.Equal(2, result.List.Count());
        }

        [Fact]
        public async Task GetSingleDataTest()
        {
            Mapper.Initialize(x => x.AddProfile<CustomizeProfile>());
            var mockSet = new Mock<DbSet<Role>>().SetupList(_sampleList);
            var mockContext = new Mock<LiveShowDBContext>();
            mockContext.Setup(x => x.Role).Returns(mockSet.Object);
            var mockSvc = new RoleSvc(mockContext.Object);
            var result = await mockSvc.GetSingleDataAsync(1);
            Assert.Equal(1, result.Data.Id);
        }
    }
}
