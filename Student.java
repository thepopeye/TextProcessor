package programManager;

import java.util.ArrayList;

public class Student extends Role {
	
	public Person Advisor;
	
	public CourseSession Session;
	
	public ArrayList<EnrollmentRecord> EnrollmentRecords;
	
	public Student() {
		// TODO Auto-generated constructor stub
		EnrollmentRecords = new ArrayList<>();
		
	}

}
