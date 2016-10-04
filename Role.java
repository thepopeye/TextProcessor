package programManager;

public abstract class Role {
	
	//Specific information about the role, e.g. faculty could be a lecturer, a professor, 
	//student can be undergrad, grad, doctoral, etc. Role is only relevant to enrollment in a course
	public String RoleClassification;
	
	public CourseSession Session;

	public Role() {
		// TODO Auto-generated constructor stub
	}

}
