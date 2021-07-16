using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private Player _player;
    private TMP_Text _text;

    public int Score;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        Score = 0;
        _text.text = Score.ToString();
    }
    private void OnEnable()
    {
        _player.ScoreIncrease.AddListener(OnScoreChange);
        _player.PlayerDie.AddListener(OnPlayerDie);
    }
    private void OnDisable()
    {
        _player.ScoreIncrease.RemoveListener(OnScoreChange);
        _player.PlayerDie.RemoveListener(OnPlayerDie);
    }

    private void OnScoreChange(int value)
    {
        Score += value;
        _text.text = Score.ToString();
    }

    private void OnPlayerDie()
    {
        PlayerPrefs.SetInt(Constants.ScorePref, Score);
        int bestScore = PlayerPrefs.GetInt(Constants.BestScorePref);
        if (Score > bestScore)
        {
            PlayerPrefs.SetInt(Constants.BestScorePref, Score);
        }
    }

   

}
