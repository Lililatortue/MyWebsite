using wiwi.domain.valueobjects.credentials;
using wiwi.domain.valueobjects.privilege;

namespace wiwi.domain.entities;
/*
 * Responsible for maintaining invariants, 
 * User to use my plateform must be at the very least 16 years old
 * have a valid email 1 unique per user
 * by default everyone is a 'guest'
 *
 * */
public class User {
  private string      _username;
  private Credentials _creds;
  private Privilege   _privilege;
  private bool        _bannedFlag; 

  internal User(string username,Credentials creds, Privilege privilege, bool banned = false){
    
    if(string.IsNullOrWhiteSpace(username))
      throw new ArgumentException("username can't be null");

    _username= username;
    _creds = creds; 
    _privilege = privilege;
    _bannedFlag = banned;
  }
   

  //GETTER / SETTER

  public string Username => _username;
  
  public string Email => _creds.Email;

  public string Password =>_creds.Password;

  public PrivilegeStatus Privilege => _privilege.PrivilegeStatus; 

  public void SetBannedFlag(bool flag) => _bannedFlag = flag;
   
  public bool BannedFlag => _bannedFlag;
  
  public bool verifyCredentials(String email, String password) {
    return email == _creds.Email && _creds.verify(password);
  }
}
