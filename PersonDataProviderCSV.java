package programManager;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class PersonDataProviderCSV implements PersonDataProvider {

	private HashMap<Integer, Person> persondb;
	
	public PersonDataProviderCSV() {
		// TODO Auto-generated constructor stub
		persondb = new HashMap<>();
	}

	@Override
	public void init(Object[] params) {
		// TODO Auto-generated method stub
		String studentsfile = params[0].toString();
		String instructorsfile = params[1].toString();
		readCSV(studentsfile, true);
		readCSV(instructorsfile, false);

	}

	@Override
	public Person getPerson(int id) {
		// TODO Auto-generated method stub
		return  persondb.get(id);
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return persondb.size();
	}

	@Override
	public List<Person> getAllPersons(){
		return new ArrayList<Person>(persondb.values());
	}


	
	private void readCSV(String filename, boolean student){
		Path file = Paths.get(filename);
		List<String> fileArray;
		try {
			fileArray = Files.readAllLines(file);
			if(student) ProgramStats.getInstance().StudentRecords = fileArray.size();
			else ProgramStats.getInstance().InstructorRecords = fileArray.size();
			for(String s : fileArray){
				String[] parts = s.split(",");
				int id = Integer.parseInt(parts[0]);
				if(!persondb.containsKey(id) && parts.length == 4){
					Person p = new Person();
					p.setID(Integer.parseInt(parts[0]));
					p.setName(parts[1]);
					p.setAddress(parts[2]);
					p.setHomePhone(parts[3]);
					persondb.put(id, p);
				}

			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
