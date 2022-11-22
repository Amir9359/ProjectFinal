using System;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace UnitTest.Builders
{
    public class DataBaseBuilder
    {
        internal DataBaseContext GetDbContext()
        {
            var opt = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DataBaseContext(opt);
        }
    }
}