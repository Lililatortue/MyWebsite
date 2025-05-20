using System.Security.Cryptography;


namespace logic;
/*
 * Creation of password 
 * Recommande usage: make sur that as soon as this password goes out of scope it 
 * gets dispose
 * 
 * */
public class Password: IDisposable{
 
  public Password(string psw) {
    this._psw = Convert.FromBase64String(psw);
  }
  
  /*GetSaltStored in password
   */
  public byte[] GetSalt() {
    byte[] salt = new byte[16];
    Array.Copy(_psw, 0,salt, 0, 16);
    return salt;
  }
  
  /*
   * compares input to the password.
   * */
  public bool CheckPassword(string input) {
    var pkbf2 = 
      new Rfc2898DeriveBytes(input, GetSalt(), 100000, HashAlgorithmName.SHA256); //password hashing
    byte[]hash = pkbf2.GetBytes(HASH_SIZE);
    
    for(int i = 0; i < HASH_SIZE; i++) {
     if(hash[i] != _psw[i]) return false;
    }
    return true;
  }


  /*
   * Generates a password with appropritate hash
   *
   * */
  public static string CreatePassword(string password){
    byte[]salt = new byte[16];

    using(var rng = RandomNumberGenerator.Create()){
      rng.GetBytes(salt);
    } // get salt

    var pkbf2 = new Rfc2898DeriveBytes(password,salt,100000,HashAlgorithmName.SHA256); //password hashing
    byte[]hash = pkbf2.GetBytes(HASH_SIZE);
    
    byte[]hashpsw =new byte[48];
    Array.Copy(salt, 0, hashpsw, 0, 16);
    Array.Copy(hash, 0, hashpsw, 16, 32);

    return Convert.ToBase64String(hashpsw); //stringify
  }


  public void Dispose() {
    if(_psw != null){
      Array.Clear(_psw, 0, _psw.Length);
    }
  }


  //private:
  ////variables:

  private const int HASH_SIZE = 32;
  private readonly byte[] _psw;
  
  /*Indexer to go through each byte of 
   */
  private byte this[int x] {
    get { 
    if (x < 0 || x >= _psw.Length - 16) throw new IndexOutOfRangeException();
    return _psw[16+x]; }
  }




}
