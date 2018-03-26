using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigid2D;
    private const float JumpForce = 700.0f;
    private const float WalkForce = 30.0f;
    private const float MaxWalkSpeed = 2.0f;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigid2D.AddForce(transform.up * JumpForce);
        }

        // 左右に移動
        var key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // プレイヤーの速度
        var speedx = Mathf.Abs(_rigid2D.velocity.x);

        // スピード制限
        if (speedx < MaxWalkSpeed) {
            _rigid2D.AddForce(transform.right * key * WalkForce);
        }

        // 動く方向に応じて反転
        if (key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }
    }
}