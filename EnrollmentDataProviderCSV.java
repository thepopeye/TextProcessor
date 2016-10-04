package programManager;

import java.util.List;

/**
 * Created by parijas on 10/3/16.
 */
public class EnrollmentDataProviderCSV implements  EnrollmentDataProvider {


    @Override
    public void addEnrollmentRecord(EnrollmentRecord record) {

    }

    @Override
    public void updateEnrollmentRecord(int studentId, int courseId, String sessionId, EnrollmentRecord record) {

    }

    @Override
    public EnrollmentRecord getEnrollmentRecord(int studentId, int courseId, String sessionId) {
        return null;
    }

    @Override
    public List<EnrollmentRecord> getEnrollmentRecordsForStudent(int studentId) {
        return null;
    }

    @Override
    public List<EnrollmentRecord> getEnrollmentRecordsForCourse(int courseId, String sessionId) {
        return null;
    }
}
