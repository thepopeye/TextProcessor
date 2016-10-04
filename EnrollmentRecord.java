package programManager;

import java.util.List;

public class EnrollmentRecord {

	public int getCourseID() {
		return CourseID;
	}

	public void setCourseID(int courseID) {
		CourseID = courseID;
	}

	public int CourseID;

	public int getInstructorID() {
		return InstructorID;
	}

	public void setInstructorID(int instructorID) {
		InstructorID = instructorID;
	}

	public int InstructorID;

	public String CourseSessionID;
	
	public List<EvaluationItem> EvaluationResponses;
	
	public String LetterGrade;
	
	public String InstructorComments;
	
	public int InstructorRating;
	
	public String FeedbackOnInstructor;
	
	public String getFeedbackOnInstructor() {
		return FeedbackOnInstructor;
	}

	public void setFeedbackOnInstructor(String feedbackOnInstructor) {
		FeedbackOnInstructor = feedbackOnInstructor;
	}

	public int getInstructorRating() {
		return InstructorRating;
	}

	public void setInstructorRating(int instructorRating) {
		InstructorRating = instructorRating;
	}

	public EnrollmentRecord() {
		// TODO Auto-generated constructor stub
	}

	public String getCourseSessionID() {
		return CourseSessionID;
	}

	public void setCourseSessionID(String courseSessionID) {
		CourseSessionID = courseSessionID;
	}

	public List<EvaluationItem> getEvaluationResponses() {
		return EvaluationResponses;
	}

	public void setEvaluationResponses(List<EvaluationItem> evaluationResponses) {
		EvaluationResponses = evaluationResponses;
	}

	public String getLetterGrade() {
		return LetterGrade;
	}

	public void setLetterGrade(String letterGrade) {
		LetterGrade = letterGrade;
	}

	public String getInstructorComments() {
		return InstructorComments;
	}

	public void setInstructorComments(String instructorComments) {
		InstructorComments = instructorComments;
	}

}
