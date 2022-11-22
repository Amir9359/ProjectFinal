using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Infrastructure.MappingProfile;
using UnitTest.Builders;
using Xunit;

namespace UnitTest.Core.Application.CatalogTypeTest
{
    public class GetListTest
    {
        [Fact]
        public void GetList_of_CatalogType()
        {
            //Arrange
            var databaseBuilder = new DataBaseBuilder();
            var dbContext = databaseBuilder.GetDbContext();

            var MockMapp = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new CatalogMappingProfile());
            });
            var mapper = MockMapp.CreateMapper();
            var service = new CatalogTypeService(dbContext, mapper);

            service.Add(new CatalogTypeDto()
            {
                Id = 1,
                Type = "T1"
            });
            service.Add(new CatalogTypeDto()
            {
                Id = 2,
                Type = "T2"
            });

            var list = service.GetList(null, 1, 20);

            // Assert
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);    
        }
    }
}