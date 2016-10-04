package programManager;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by parijas on 9/28/16.
 */
public class CourseDataProviderCSV implements CourseDataProvider {

    private HashMap<Integer, Course> courseCatalog;

    public CourseDataProviderCSV(){
        courseCatalog = new HashMap<Integer, Course>();
    }

    @Override
    public void addCourse(Course course) {

    }

    @Override
    public void removeCourse(Course course) {

    }

    @Override
    public void updateCourse(Course course) {

    }

    @Override
    public Course getCourse(int id) {
        if(courseCatalog.containsKey(id))
            return courseCatalog.get(id);
        else return  null;
    }

    @Override
    public void init(Object[] params) {
        readCSV(params[0].toString());
    }

    @Override
    public List<Course> getCourses(){
        return new ArrayList<Course>(courseCatalog.values());
    }

    private void readCSV(String filename){
        Path file = Paths.get(filename);
        List<String> fileArray;
        try {
            fileArray = Files.readAllLines(file);
            ProgramStats.getInstance().CourseRecords = fileArray.size();
            for(String s : fileArray){
                String[] parts = s.split(",");
                int id = Integer.parseInt(parts[0]);
                if(!courseCatalog.containsKey(id)){
                    Course c = new Course();
                    c.setID(Integer.parseInt(parts[0]));
                    c.setTitle(parts[1]);
                    for(int i=2;i<=4;i++){
                        if(parts.length>i){
                            c = setCourseSession(parts[i], c);
                        }
                    }
                    courseCatalog.put(id, c);
                }
            }
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    private Course setCourseSession(String session, Course course){
        switch (session.toLowerCase()){
            case "fall":
                course.setOfferedInFall(true);
                break;
            case "summer":
                course.setOfferedInSummer(true);
                break;
            case "spring":
                course.setOfferedInSpring(true);
                break;
            default:
                break;
        }
        return  course;
    }
}
