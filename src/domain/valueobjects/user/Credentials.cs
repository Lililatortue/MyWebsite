using System.Security.Cryptography;
using System.Net.Mail;


namespace wiwi.domain.valueobjects.credentials;

using cred = wiwi.domain.valueobjects.credentials;


public class Credentials{
    private string _email;
    private Password _password;
    
    //constructor

    private Credentials(string email, Password password){
        if(string.IsNullOrWhiteSpace(email))
          throw new ArgumentException("email can't be empty"); 

        var addr = new MailAddress(email); //throws
        _email = addr.Address;
        _password = password; 
    }

    //to create credentials
    public static Credentials CreateCredentials(string email, string password){
      return new Credentials(email,cred.Password.CreateFromString(password));
    }

    //to get credentials from db
    public static Credentials GetCredentials(string email,string password){
      return new Credentials(email, cred.Password.GetHash(password));
    }

    public string Email => _email;
    public string Password => _password.HashPsw;    
    public bool verify(string input) => _password.Matches(input);
}


/*
 * Creation of password 
 * Recommande usage: make sur that as soon as this password goes out of scope it 
 * gets dispose
 * 
 * */
internal class Password{  
  //variables:

  private const int HASH_SIZE = 32;
  private const int SALT_SIZE = 16;

  private readonly byte[] _psw;
  
  //constructor:

  private Password(byte[] hash) {
    _psw = hash;
  }
  
  /* 
   * GetSaltStored in password
   *
   */
  public byte[] GetSalt() {
    byte[] salt = new byte[16];
    Array.Copy(_psw, 0,salt, 0, 16);
    return salt;
  }

  /*
   * Generates a password with appropritate hash
   *
   * */
  public static Password CreateFromString(string password){
    byte[]salt = new byte[SALT_SIZE];

    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(salt); 

    var pkbf2 = new Rfc2898DeriveBytes(password,salt,100000,HashAlgorithmName.SHA256); //password hashing
    byte[]hash = pkbf2.GetBytes(HASH_SIZE);
    
    byte[]hashpsw =new byte[SALT_SIZE + HASH_SIZE];

    Array.Copy(salt, 0, hashpsw, 0, SALT_SIZE);
    Array.Copy(hash, 0, hashpsw, SALT_SIZE, HASH_SIZE);

    return new Password(hashpsw); //stringify
  }

  /*
   * From db string to password
   *
   * */
  public static Password GetHash(string password){
    byte[] hash = Convert.FromBase64String(password);
    return new Password(hash);
  }



  /*
   * compares input to the password.
   *
   * */
  public bool Matches(string input) {
    var pkbf2 = 
      new Rfc2898DeriveBytes(input, GetSalt(), 100000, HashAlgorithmName.SHA256); //password hashing
    byte[]hash = pkbf2.GetBytes(HASH_SIZE);
    
    for(int i = 0; i < HASH_SIZE; i++) {
     if(hash[i] != _psw[i]) return false;
    }
    return true;
  }

 
  /*
   * Indexer to go through each byte of safely
   * 
   */
  private byte this[int x] {
    get { 
    if (x < 0 || x >= _psw.Length - SALT_SIZE) throw new IndexOutOfRangeException();
    return _psw[SALT_SIZE + x]; }
  }
  //getter
  public string HashPsw => Convert.ToBase64String(_psw);
}
