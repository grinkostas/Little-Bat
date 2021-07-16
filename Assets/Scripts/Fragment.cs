using UnityEngine;

public class Fragment : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private uint _spawnRubinChance;
    [SerializeField] private Rubin _rubin;
    [SerializeField] private Transform BuffPoint;

    public Transform EndPoint;    
    public float Speed = 1;

    private void Start()
    {
        int rand = Random.Range(0, 100);
        if (rand < _spawnRubinChance)
        {
            Instantiate(_rubin, BuffPoint);
        }
    }
    private void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

}
