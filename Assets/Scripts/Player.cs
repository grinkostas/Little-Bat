using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[System.Serializable] public class ScoreEvent : UnityEvent<int> { }
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [Min(1)] [SerializeField] private int _lifes;

    [HideInInspector] public UnityEvent PlayerDie;
    [HideInInspector] public UnityEvent GameStart;
    [HideInInspector] public ScoreEvent ScoreIncrease;

    private int _direction = 0;


    public void ChangeDirection()
    {
        if (_direction == 0)
        {
            _direction = 1;
            GameStart.Invoke();
        }
        else
        {
            _direction *= -1;
        }
        
    }
    void Update()
    {
        transform.Translate(Vector3.up * _direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Block block))
        {
            _lifes--;
            if (_lifes == 0)
            {
                PlayerDie.Invoke();
            }
            
        }
        if (collision.TryGetComponent(out Score score))
        {
            ScoreIncrease.Invoke(score.Value);
        }
        if (collision.TryGetComponent(out Rubin rubin))
        {
            rubin.Claim();
        }
    }
}


