using UnityEngine;

public class HideAtStart : MonoBehaviour
{
    [SerializeField] Player _player;

    private void OnEnable()
    {
        _player.GameStart.AddListener(OnGameStart);
    }
    private void OnDisable()
    {
        _player.GameStart.RemoveListener(OnGameStart);
    }
    private void OnGameStart()
    {
        Destroy(gameObject);
    }
}
