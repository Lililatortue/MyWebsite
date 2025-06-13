using Microsoft.EntityFrameworkCore;
using wiwi.infrastructure.models;




namespace wiwi.infrastructure.context;

public class UserDbContext: DbContext{
  public DbSet<UserModel> Users;
  //TODO: Manage banned users

  public UserDbContext(DbContextOptions<UserDbContext> options): base(options){}
}



