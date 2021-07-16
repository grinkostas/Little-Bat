using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    private int _score;
    private int _bestScore;

    private void Awake()
    {
        _score = PlayerPrefs.GetInt(Constants.ScorePref);
        _bestScore = PlayerPrefs.GetInt(Constants.BestScorePref);
    }
    private void Start()
    {
        _scoreText.text = _score.ToString();
        _bestScoreText.text = _bestScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
