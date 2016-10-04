package programManager;

/**
 * Created by parijas on 9/28/16.
 */
public class ProgramStats {

    private static ProgramStats instance;

    public static ProgramStats getInstance(){
        if(null == instance)
            instance = new ProgramStats();
        return instance;
    }

    public ProgramStats(){

    }

    public int StudentRecords;

    public int StudentsNoClasses;

    public int InstructorRecords;

    public int InstructorNoClasses;

    public int CourseRecords;

    public int CourseNoStudents;

    public int Records;

    public void writeStats(){
        System.out.println(Records);
        System.out.println(StudentRecords);
        System.out.println(StudentsNoClasses);
        System.out.println(InstructorRecords);
        System.out.println(InstructorNoClasses);
        System.out.println(CourseRecords);
        System.out.println(CourseNoStudents);
        System.out.println(CourseCatalog.getInstance().fallCourses());
        System.out.println(CourseCatalog.getInstance().springCourses());
        System.out.println(CourseCatalog.getInstance().summerCourses());
    }







}
