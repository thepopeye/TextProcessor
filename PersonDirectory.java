package programManager;

import java.util.HashMap;

public class PersonDirectory{

	private static PersonDirectory instance;
	
	private PersonDirectory() {
		// TODO Auto-generated constructor stub
	}
	
	public static PersonDirectory getInstance(){
		if(null==instance) instance = new PersonDirectory();
		return instance;
	}
	
	private HashMap<Integer, Person> persondb;

	public void setDataprovider(PersonDataProvider dataprovider) {
		this.dataprovider = dataprovider;
	}

	private PersonDataProvider dataprovider;
	
	public void init(Object[] params){
			dataprovider.init(params);
	}
	
	public Person getPerson(int id){
		return dataprovider.getPerson(id);
	}

	public int getStudentsWithNoEnrollments(){
		int count = 0;
		for(Person p : dataprovider.getAllPersons()){
			if(p.getRoles().size() > 0 && p.getRoles().get(0) instanceof Student) {

					Student srole = (Student) (p.getRoles().get(0));
					if (null != srole && srole.EnrollmentRecords.size() > 0)
						count++;
			}
		}
		return  count;
	}

	public int getInstructorsWithNoEnrollments(){
		int count = 0;
		for(Person p : dataprovider.getAllPersons()){
			if(p.getRoles().size() > 0 && p.getRoles().get(0) instanceof Faculty) {
				Faculty srole = (Faculty) (p.getRoles().get(0));
				if (null != srole && srole.TeachingRecords.size() > 0)
					count++;
			}
		}
		return  count;
	}
	
	

}
