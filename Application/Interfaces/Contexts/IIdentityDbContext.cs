using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts
{
    public interface IIdentityDbContext
    { 
        DbSet<User> Users { get; set; }
    }
}