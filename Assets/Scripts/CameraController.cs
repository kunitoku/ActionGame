using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        var playerPos = _player.transform.position;
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}