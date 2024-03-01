using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models;

public class DataContext : IdentityDbContext<AppUser , AppRole , string> 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}   