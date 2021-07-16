using System.Collections.Generic;
using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Fragments Prefabs to Spawn")]
    [SerializeField] private Fragment[] _fragments;

    [Tooltip("Fragment to spawn at Start without obstacles")]
    [SerializeField] private Fragment _emptyFragment;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Player _player;

    [Header("Values")]
    [Min(1)] [SerializeField] private uint _countOfEmptyFragments;
    [SerializeField] private float _speed;
    

    private List<Fragment> _spawnedFragments;
    private bool _isGameStarted = false;

    private void OnEnable()
    {
        _player.GameStart.AddListener(OnGameStart);
    }

    private void OnDisable()
    {
        _player.GameStart.RemoveListener(OnGameStart);
    }

    private void Start()
    {
        _spawnedFragments = new List<Fragment>();
        SpawnStartFragments();
    }

    private void SpawnStartFragments()
    {
        for (uint i = 0; i < _countOfEmptyFragments; i++)
        {
            Spawn(_emptyFragment);
        }
    }

    
    private void Spawn(Fragment newFragment)
    {                
        Vector3 spawnPosition = NextSpawnPostion();
        newFragment.Speed = GetSpeed();
        _spawnedFragments.Add(Instantiate(newFragment, transform));
        _spawnedFragments[_spawnedFragments.Count - 1].transform.position = spawnPosition;
    }

    private float GetSpeed()
    {
        float result;
        if (_isGameStarted)
        {
            result = _speed;
        }
        else
        {
            result = 0;
        }
        return result;

    }
    private Vector3 NextSpawnPostion()
    {
        Vector3 spawnPosition = _startPoint.position;
        if (_spawnedFragments.Count > 0)
        {
            spawnPosition = _spawnedFragments[_spawnedFragments.Count - 1].EndPoint.position;
        }
        return spawnPosition;
    }
    private void OnGameStart()
    {
        _isGameStarted = true;
        foreach(var item in _spawnedFragments)
        {
            item.Speed = _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Block>(out Block block))
        {
            Fragment fragment = collision.GetComponentInParent<Fragment>();
            _spawnedFragments.Remove(fragment);
            Destroy(fragment.gameObject);

            Fragment newFragment = _fragments[Random.Range(0, _fragments.Length)];
            Spawn(newFragment);
        }
        
    }

}
