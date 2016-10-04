package programManager;

public class EvaluationItem {
	
	public Object Questions;
	
	public Object Answers;
	
	public String Comments;
	
	public Object getQuestions() {
		return Questions;
	}

	public void setQuestions(Object questions) {
		Questions = questions;
	}

	public Object getAnswers() {
		return Answers;
	}

	public void setAnswers(Object answers) {
		Answers = answers;
	}

	public String getComments() {
		return Comments;
	}

	public void setComments(String comments) {
		Comments = comments;
	}

	public double getFullScore() {
		return FullScore;
	}

	public void setFullScore(double fullScore) {
		FullScore = fullScore;
	}

	public double getObtainedScore() {
		return ObtainedScore;
	}

	public void setObtainedScore(double obtainedScore) {
		ObtainedScore = obtainedScore;
	}

	public double getScaledScore() {
		return ScaledScore;
	}

	public void setScaledScore(double scaledScore) {
		ScaledScore = scaledScore;
	}

	public double getWeight() {
		return Weight;
	}

	public void setWeight(double weight) {
		Weight = weight;
	}

	public double FullScore;
	
	public double ObtainedScore;
	
	public double ScaledScore;
	
	public double Weight;

	public EvaluationItem() {
		// TODO Auto-generated constructor stub
	}

}
