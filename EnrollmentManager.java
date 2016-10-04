package programManager;

import java.util.List;

/**
 * Created by parijas on 10/3/16.
 */
public class EnrollmentManager {

    private EnrollmentDataProvider dataprovider;

    public void setDataprovider(EnrollmentDataProvider dataprovider) {
        this.dataprovider = dataprovider;
    }

    private static EnrollmentManager instance;

    private EnrollmentManager(){}

    public EnrollmentManager getInstance(){
        if(null == instance)
            instance = new EnrollmentManager();
        return  instance;
    }

    public void addEnrollmentRecord(EnrollmentRecord record) {
        dataprovider.addEnrollmentRecord(record);
    }

    public void updateEnrollmentRecord(int studentId, int courseId, String sessionId, EnrollmentRecord record) {
        dataprovider.updateEnrollmentRecord(studentId, courseId, sessionId, record);
    }

    public EnrollmentRecord getEnrollmentRecord(int studentId, int courseId, String sessionId) {
        return  dataprovider.getEnrollmentRecord(studentId, courseId, sessionId);
    }

    public List<EnrollmentRecord> getEnrollmentRecordsForStudent(int studentId) {
        return  dataprovider.getEnrollmentRecordsForStudent(studentId);
    }

    public List<EnrollmentRecord> getEnrollmentRecordsForCourse(int courseId, String sessionId) {
        return  dataprovider.getEnrollmentRecordsForCourse(courseId, sessionId);
    }

}
