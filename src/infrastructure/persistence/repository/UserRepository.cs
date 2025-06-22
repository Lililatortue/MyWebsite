using wiwi.infrastructure.context;
using wiwi.infrastructure.models;


namespace wiwi.infrastructure.repository.user;
//TODO(ENV)-> create env variables
public class UserRepository: IUserRepository{
  private readonly UserDbContext _context;

  public UserRepository(UserDbContext context){
    _context = context;
  }

  public void Create(UserModel user){
    try {
      _context.Users.Add(user);
      _context.SaveChanges();
    } catch (Exception ex) {
      throw new Exception($"repo: error in create description:"+ex.Message);
    } 
  }

  public void Delete(UserModel user){
    try {
      _context.Remove(user);
      _context.SaveChanges();
    } catch (Exception ex){
      throw new Exception("repo: error in delete\n description:\t"+ex.Message);
    } 
  }

  public void Update(UserModel user){
    try {
      _context.Update(user);
      _context.SaveChanges();
    } catch (Exception ex) {
      throw new Exception("repo: error in update\n description:\t"+ex.Message);
    }
  }
  
  public List<UserModel> FetchAll(int min, int max){
    try {
       List<UserModel> listModel = _context.Users.Skip(min).Take(max).ToList();
       return listModel;
    } catch (Exception ex) {
      throw new Exception("repo: error in fetch all\n description:\t"+ex.Message);
    } 
  }
  
  public UserModel? FindByEmail(string email){
    try {
       return _context.Users.FirstOrDefault(u=>u.Email == email);
    } catch (Exception ex) {
      throw new Exception("repo: error in findbyemail\n description:\t"+ex.Message);
    }
  }

  public UserModel? FindByUsername(string username){
    try {
      return _context.Users.FirstOrDefault(u=>u.Username == username);
    } catch (Exception ex) {
      throw new Exception("repo: error findbyusername\n description:\t"+ex.Message);
    }
  }

}
