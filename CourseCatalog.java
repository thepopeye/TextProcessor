package programManager;

import java.util.ArrayList;
import java.util.List;

public class CourseCatalog {
	
	private List<Course> catalog;

	public void setCourseDataProvider(CourseDataProvider courseDataProvider) {
		this.courseDataProvider = courseDataProvider;
	}

	private CourseDataProvider courseDataProvider;
	
	private static CourseCatalog instance;

	private CourseCatalog() {
		// TODO Auto-generated constructor stub
		catalog = new ArrayList<Course>();
	}
	
	public static CourseCatalog getInstance(){
		if(null==instance) instance = new CourseCatalog();
		return instance;
	}

	public void init(Object[] params){
		courseDataProvider.init(params);
	}
	
	
	public boolean addCourse(Course course){
		catalog.add(course);
		return true;
	}
	
	public boolean removeCourse(Course course){
		return true;
	}
	
	public boolean updateCourse(Course course){
		return true;
	}

	public Course getCourse(int id){
		return  courseDataProvider.getCourse(id);
	}

	public List<Course> getCourses(){
		return courseDataProvider.getCourses();
	}

	public CourseOffering createCourseOffering(Course course, CourseSession session){
		CourseOffering offering = new CourseOffering();
		offering.setBaseCourse(course);
		offering.setSession(session);
		return offering;
	}

	public int fallCourses(){
		int count = 0;
		for(Course c : getCourses()){
			if(c.isOfferedInFall()) count++;
		}
		return  count;
	}

	public int springCourses(){
		int count = 0;
		for(Course c : getCourses()){
			if(c.isOfferedInSpring()) count++;
		}
		return  count;
	}

	public int summerCourses(){
		int count = 0;
		for(Course c : getCourses()){
			if(c.isOfferedInSummer()) count++;
		}
		return  count;
	}
	
	

}
