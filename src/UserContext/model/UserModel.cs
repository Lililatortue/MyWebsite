using System.Net.Mail; //verifying email





namespace models;
/*
 * Responsible for maintaining invariants, 
 * User to use my plateform must be at the very least 16 years old
 * have a valid email 1 unique per user
 * by default everyone is 'enduser'
 *
 * */
public class UserModel {
  public string username  {get; private set;}
  public string email     {get; private set;}
  public string password  {get; private set;}
  public string role      {get; private set;}
  public bool   IsBanned; 

  public UserModel(string username, string email, string psw, string role){
    SetUsername(username);
    SetEmail(email);
    SetPassword(psw);
    this.role = role;
    IsBanned = false;
  }

  public void SetUsername(string str){
    if(string.IsNullOrEmpty(str)){

      this.username = str;
    } else {
      throw new ArgumentException("Invalid username");
    }
  }

  public void SetEmail(string str){
    try{
      this.email = new MailAddress(str).ToString();
    } catch(Exception ex) {
      throw new ArgumentException("Invalid email "+ex.Message);
    }
  }

  public void SetPassword(string str){
    if(string.IsNullOrEmpty(str)){

      this.password = str;
    } else {
      throw new ArgumentException("Invalid password");
    }
  }
  public void SetBannedFlag(bool flag){
    IsBanned = flag;
  }
}
