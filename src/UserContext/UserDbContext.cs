using Microsoft.EntityFrameworkCore;
using models;




namespace context.user;

public class UserDbContext: DbContext{
  public DbSet<UserModel> Users;
  //TODO: Manage banned users

  public UserDbContext(DbContextOptions<UserDbContext> options): base(options){}
}



