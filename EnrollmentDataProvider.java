package programManager;

import java.util.List;

/**
 * Created by parijas on 10/3/16.
 */
public interface EnrollmentDataProvider {

    void addEnrollmentRecord(EnrollmentRecord record);

    void updateEnrollmentRecord(int studentId, int courseId, String sessionId, EnrollmentRecord record);

    EnrollmentRecord getEnrollmentRecord(int studentId, int courseId, String sessionId);

    List<EnrollmentRecord> getEnrollmentRecordsForStudent(int studentId);

    List<EnrollmentRecord> getEnrollmentRecordsForCourse(int courseId, String sessionId);

}
