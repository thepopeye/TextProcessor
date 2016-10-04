package programManager;

import javax.swing.plaf.basic.BasicInternalFrameTitlePane;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class Program {

	private static final String studentfile = "students.csv";
	private static final String instructorfile = "instructors.csv";
	private static final String coursefile = "courses.csv";
	private static final String recordsfile = "records.csv";


	public Program() {
		// TODO Auto-generated constructor stub
	}

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		if(args.length == 0) {
			System.out.println("Please specify the directory for data files.");
			return;
		}

		Path directory = Paths.get(args[0]);
		PersonDirectory.getInstance().setDataprovider(new PersonDataProviderCSV());
		PersonDirectory.getInstance().init(new Object[]{directory.resolve(studentfile).toString(), directory.resolve(instructorfile).toString()});
		CourseCatalog.getInstance().setCourseDataProvider(new CourseDataProviderCSV());
		CourseCatalog.getInstance().init(new Object[]{directory.resolve(coursefile).toString()});
		processEnrollmentRecords(directory.resolve(recordsfile).toString());
		ProgramStats.getInstance().StudentsNoClasses = PersonDirectory.getInstance().getStudentsWithNoEnrollments();
		ProgramStats.getInstance().InstructorNoClasses = PersonDirectory.getInstance().getInstructorsWithNoEnrollments();
		ProgramStats.getInstance().writeStats();
	}

	private static int processEnrollmentRecords(String filename){
		Path file = Paths.get(filename);
		List<String> fileArray = new ArrayList<>();
		try {
			fileArray = Files.readAllLines(file);
			ProgramStats.getInstance().Records = fileArray.size();
			for(String s : fileArray){
				String[] parts = s.split(",");
				int id = Integer.parseInt(parts[0]);
				Person student = initStudent(parts[0]);
				Person instructor = initInstructor(parts[2]);
				if(null!=student && null!=instructor && null!=CourseCatalog.getInstance().getCourse(Integer.parseInt(parts[1]))){
					EnrollmentRecord record = new EnrollmentRecord();
					record.setCourseID(Integer.parseInt(parts[1]));
					record.setInstructorID(Integer.parseInt(parts[2]));
					record.setInstructorComments(parts[3]);
					record.setLetterGrade(parts[4]);
					Student srole = (Student)(student.getRoles().get(0));
					srole.EnrollmentRecords.add(record);
					TeachingRecord trecord = new TeachingRecord();
					Faculty frole = (Faculty)(instructor.getRoles().get(0));
					frole.TeachingRecords.add(trecord);
				}
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return  fileArray.size();
	}

	private static Person initStudent(String sid){
		int id = Integer.parseInt(sid);
		Person person = PersonDirectory.getInstance().getPerson(id);
		if(null!=person) {
			if (person.getRoles().size() == 0) {
				Student srole = new Student();
				person.addRole(srole);
			}
		}
		else System.out.println("Incorrect student ID. Record cannot be processed.");
		return person;
	}

	private static Person initInstructor(String sid){
		int id = Integer.parseInt(sid);
		Person person = PersonDirectory.getInstance().getPerson(id);
		if(null!=person) {
			if (person.getRoles().size() == 0) {
				Faculty frole = new Faculty();
				person.addRole(frole);
			}
		}
		else System.out.println("Incorrect instructor ID. Record cannot be processed.");
		return person;
	}

}
