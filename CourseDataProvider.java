package programManager;

import java.util.List;

/**
 * Created by parijas on 9/28/16.
 */
public interface CourseDataProvider {
    void addCourse(Course course);

    void removeCourse(Course course);

    void updateCourse(Course course);

    Course getCourse(int id);

    List<Course> getCourses();

    void init(Object[] params);


}
