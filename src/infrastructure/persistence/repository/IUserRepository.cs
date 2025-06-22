using wiwi.infrastructure.models;

namespace wiwi.infrastructure.repository.user;

public interface IUserRepository{
  public void Create(UserModel model);
  public void Delete(UserModel model);
  public void Update(UserModel model);
  public List<UserModel> FetchAll(int min, int max);
  public UserModel? FindByEmail(string email);
  public UserModel? FindByUsername(string username);
}
