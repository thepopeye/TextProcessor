package programManager;

import java.util.Date;

public class Course {

	public int getID() {
		return ID;
	}

	public void setID(int ID) {
		this.ID = ID;
	}

	public int ID;
	
	public String CourseID;
	
	public String Title;
	
	public String Description;
	
	public Date CourseActivationDate;
	
	public Date CourseDeactivationDate;
	
	public boolean OfferedInFall;
	
	public boolean OfferedInSpring;
	
	public boolean OfferedInSummer;

	public Course() {
		// TODO Auto-generated constructor stub
		
	}
	
	public String getCourseID() {
		return CourseID;
	}

	public void setCourseID(String courseID) {
		CourseID = courseID;
	}

	public String getTitle() {
		return Title;
	}

	public void setTitle(String title) {
		Title = title;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	public Date getCourseActivationDate() {
		return CourseActivationDate;
	}

	public void setCourseActivationDate(Date courseActivationDate) {
		CourseActivationDate = courseActivationDate;
	}

	public Date getCourseDeactivationDate() {
		return CourseDeactivationDate;
	}

	public void setCourseDeactivationDate(Date courseDeactivationDate) {
		CourseDeactivationDate = courseDeactivationDate;
	}

	public boolean isOfferedInFall() {
		return OfferedInFall;
	}

	public void setOfferedInFall(boolean offeredInFall) {
		OfferedInFall = offeredInFall;
	}

	public boolean isOfferedInSpring() {
		return OfferedInSpring;
	}

	public void setOfferedInSpring(boolean offeredInSpring) {
		OfferedInSpring = offeredInSpring;
	}

	public boolean isOfferedInSummer() {
		return OfferedInSummer;
	}

	public void setOfferedInSummer(boolean offeredInSummer) {
		OfferedInSummer = offeredInSummer;
	}

	public boolean IsCourseActive(){
		return true;
	}

}
