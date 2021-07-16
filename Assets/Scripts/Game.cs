using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ScoreView _score;
    [SerializeField] private Menu _menu;
    

    [Header("Light")]
    [SerializeField] private Light _light;
    [SerializeField] private Color _endLightColor;

    [Header("Levels Setting")]
    [SerializeField] private int _scoreToNextLevel;    
    [SerializeField] private float _rangeDecreasePerLevel;
    [SerializeField] private int _maxLevelsForLight;

    [Header("Sounds")]
    [SerializeField] private AudioSource _dieSound;
    [SerializeField] private AudioSource _wingsSound;

    private int _level = 1;
    private bool _isAlive = true;
    private bool _isPaused = false;

    private void OnEnable()
    {       
        _player.PlayerDie.AddListener(OnPlayerDie);
    }
    private void OnDisble()
    {
        _player.PlayerDie.RemoveListener(OnPlayerDie);
    }
    private void Start()
    {
        _wingsSound.Play();
        Time.timeScale = 1;
        
    }
    private void OnPlayerDie()
    {
        Time.timeScale = 0;
        _dieSound.Play();
        _wingsSound.Stop();
        _isAlive = false;
        Instantiate(_menu.gameObject, _canvas.transform);
    }
    private void FixedUpdate()
    {
        if (_score.Score > _scoreToNextLevel * _level)
        {
            if (_level < _maxLevelsForLight)
            {
                ChangeLight();
            }            
            _level++;
        }
    }
    private void ChangeLight()
    {
        _light.range = _light.range - _rangeDecreasePerLevel;
        _light.color = _light.color + (_endLightColor / (_maxLevelsForLight - _level));
    }

    private void Resume()
    {
        Time.timeScale = 1;
        _wingsSound.Play();
        _isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _wingsSound.Stop();
        _isPaused = true;
    }

    public void ClickOnScreen()
    {
        if (_isAlive && !_isPaused)
        {
            _player.ChangeDirection();
        }

        if (_isPaused)
        {
            Resume();
        }
    }

}
