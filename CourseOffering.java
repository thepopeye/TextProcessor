package programManager;

import java.util.ArrayList;
import java.util.List;

public class CourseOffering {

	public String UID;
	
	public Course BaseCourse;
	
	public CourseSession Session;
	
	//used to determine specific campus (or online) 
	public String CampusDesignation;
	
	public Object Syllabus;
	
	public String RecommendedReading;
	
	public List<EvaluationItem> EvaluationMaterial;
	
	public String getUID() {
		return UID;
	}

	public void setUID(String uID) {
		UID = uID;
	}

	public Course getBaseCourse() {
		return BaseCourse;
	}

	public void setBaseCourse(Course baseCourse) {
		BaseCourse = baseCourse;
	}

	public CourseSession getSession() {
		return Session;
	}

	public void setSession(CourseSession session) {
		Session = session;
	}

	public Object getSyllabus() {
		return Syllabus;
	}

	public void setSyllabus(Object syllabus) {
		Syllabus = syllabus;
	}

	public String getRecommendedReading() {
		return RecommendedReading;
	}

	public void setRecommendedReading(String recommendedReading) {
		RecommendedReading = recommendedReading;
	}

	public List<EvaluationItem> getEvaluationMaterial() {
		return EvaluationMaterial;
	}

	public void setEvaluationMaterial(List<EvaluationItem> evaluationMaterial) {
		EvaluationMaterial = evaluationMaterial;
	}

	public Person getInstructor() {
		return instructor;
	}

	public void setInstructor(Person instructor) {
		instructor.addRole(new Faculty());
		this.instructor = instructor;
	}

	public Person getTeachingAssistant() {
		return teachingAssistant;
	}

	public void setTeachingAssistant(Person teachingAssistant) {
		this.teachingAssistant = teachingAssistant;
	}

	private List<Person> students;
	
	private Person instructor;
	
	private Person teachingAssistant;
	
	public CourseOffering() {
		// TODO Auto-generated constructor stub
		students = new ArrayList<Person>();
	}
	
	public boolean registerStudent(Person person){
		Student studentrole = new Student();
		EnrollmentRecord erecord = new EnrollmentRecord();
		erecord.setEvaluationResponses(EvaluationMaterial);
		erecord.setCourseSessionID(UID);
		person.addRole(studentrole);
		students.add(person);
		return true;
	}
	
	public boolean removeStudent(Person person){
		return true;
	}
}
