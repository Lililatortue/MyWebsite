using wiwi.infrastructure.context;
using wiwi.infrastructure.models;


namespace wiwi.infrastructure.repository;

//TODO(ENV)-> create env variables
public class UserRepository{
  private readonly UserDbContext _context;
  private List<UserModel> _repo;

  public UserRepository(){
    _context = new UserDbContext();
    _repo = new List<UserModel>();
  }

  public void Create(UserModel user){
    try {
      _context.Add(user);
    } catch (Exception ex) {
      throw new Exception("repo: error in create\n description:\t"+ex.Message);
    } finally {
      _context.SaveChanges();
    }
  }

  public void Delete(UserModel user){
    try {
      _context.Remove(user);
    } catch (Exception ex){
      throw new Exception("repo: error in delete\n description:\t"+ex.Message);
    } finally {
      _context.SaveChanges();
    }
  }

  public void Update(UserModel user){
    try {
      _context.Update(user);
    } catch (Exception ex) {
      throw new Exception("repo: error in update\n description:\t"+ex.Message);
    } finally {
      _context.SaveChanges();
    }
  }
  
  public List<UserModel> FetchAll(int min, int max){
    try {
      return _context.Users.Skip(min).Take(max).ToList();
    } catch (Exception ex) {
      throw new Exception("repo: error in fetch all\n description:\t"+ex.Message);
    } 
  }
  
  public UserModel? FindByEmail(String email){
    try {
       return _context.Users.FirstOrDefault(u=>u.Email == email);
    } catch (Exception ex) {
      throw new Exception("repo: error in findbyemail\n description:\t"+ex.Message);
    }
  }

  public UserModel? FindByUsername(String username){
    try {
      return _context.Users.FirstOrDefault(u=>u.username == username);
    } catch (Exception ex) {
      throw new Exception("repo: error findbyusername\n description:\t"+ex.Message);
    }
  }

}
