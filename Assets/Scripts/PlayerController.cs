using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigid2D;
    private Animator _animator;
    private const float JumpForce = 700.0f;
    private const float WalkForce = 30.0f;
    private const float MaxWalkSpeed = 2.0f;
    private const float Threshold = 0.2f;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var key = 0;

#if UNITY_EDITOR
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && _rigid2D.velocity.y == 0)
        {
            _animator.SetTrigger("JumpTrigger");
            _rigid2D.AddForce(transform.up * JumpForce);
        }

        // 左右に移動
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
#else
// ジャンプ
        if (Input.GetMouseButtonDown(0) && _rigid2D.velocity.y == 0)
        {
            _animator.SetTrigger("JumpTrigger");
            _rigid2D.AddForce(transform.up * JumpForce);
        }

        // 左右に移動
        if (Input.acceleration.x > Threshold) key = 1;
        if (Input.acceleration.x < -Threshold) key = -1;
#endif

        // プレイヤーの速度
        var speedx = Mathf.Abs(_rigid2D.velocity.x);

        // スピード制限
        if (speedx < MaxWalkSpeed)
        {
            _rigid2D.AddForce(transform.right * key * WalkForce);
        }

        // 動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // プレイヤーの動きに応じてアニメーション
        if (_rigid2D.velocity.y == 0)
        {
            _animator.speed = speedx / 2.0f;
        }
        else
        {
            _animator.speed = 1.0f;
        }

        // 画面外に出た時の処理
        if (!(transform.position.y < -5)) return;
        transform.position = Vector3.zero;
        _rigid2D.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }
}