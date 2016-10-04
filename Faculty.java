package programManager;

import java.util.ArrayList;
import java.util.List;

public class Faculty extends Role {
	
	public CourseSession Session;
	
	public List<TeachingRecord> TeachingRecords;

	public Faculty() {
		// TODO Auto-generated constructor stub
		TeachingRecords = new ArrayList<>();
		
	}

}
