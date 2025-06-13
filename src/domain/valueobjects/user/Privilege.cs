

namespace wiwi.domain.valueobjects.privilege;

public class Privilege {
  private readonly PrivilegeStatus _privileges;

  public Privilege(PrivilegeStatus privileges){
    _privileges = privileges;
  }
  
  public PrivilegeStatus PrivilegeStatus => _privileges;  
  
  public bool match(PrivilegeStatus status) => _privileges == status;
}


public enum PrivilegeStatus {
  Guest = 0,
  Admin = 1,
  Root  = 2 
}
