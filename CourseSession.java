package programManager;

import java.util.Date;

public class CourseSession {
	
	//Name is similar to Fall2016, Spring2017 etc.
	public String Name;
	
	public String getName() {
		return Name;
	}

	public void setName(String name) {
		Name = name;
	}

	public Date getStartDate() {
		return StartDate;
	}

	public void setStartDate(Date startDate) {
		StartDate = startDate;
	}

	public Date getEndDate() {
		return EndDate;
	}

	public void setEndDate(Date endDate) {
		EndDate = endDate;
	}

	public Date StartDate;
	
	public Date EndDate;

	public CourseSession() {
		// TODO Auto-generated constructor stub
	}

}
