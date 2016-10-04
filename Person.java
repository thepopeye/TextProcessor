package programManager;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class Person {

	private String Name;

	private int ID;
	
	private String HomePhone;
	
	private String CellPhone;
	
	private String Address;
	
	private String Email;
	
	private List<Role> Roles;
	
	public Person() {
		// TODO Auto-generated constructor stub
		Roles = new ArrayList<Role>();
	}
	
	public void addRole(Role role){
		Roles.add(role);
	}
	
	public void removeRole(Role role){
		Roles.remove(role);
	}

	public List<Role> getRoles(){return Roles;}

	public String getName() {
		return Name;
	}

	public void setName(String name) {
		Name = name;
	}

	public int getID() {
		return ID;
	}

	public void setID(int id) {ID = id; }

	public String getHomePhone() {
		return HomePhone;
	}

	public void setHomePhone(String homePhone) {
		HomePhone = homePhone;
	}

	public String getCellPhone() {
		return CellPhone;
	}

	public void setCellPhone(String cellPhone) {
		CellPhone = cellPhone;
	}

	public String getAddress() {
		return Address;
	}

	public void setAddress(String address) {
		Address = address;
	}

	public String getEmail() {
		return Email;
	}

	public void setEmail(String email) {
		Email = email;
	}

}
