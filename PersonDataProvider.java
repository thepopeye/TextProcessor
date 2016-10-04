package programManager;

import java.util.List;

public interface PersonDataProvider {

	public void init(Object[] params);
	
	public Person getPerson(int id);
	
	public int getCount();

	public List<Person> getAllPersons();
}
