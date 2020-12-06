using UnityEngine;

public class BallShadowController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    private int _damage;
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetDamage()
    {
        return _damage;
    }
    public void TeleportToPosition(Vector2 pos)
    {
        transform.position = new Vector2(pos.x, pos.y);
    }

    public void AddMovement(Vector2 velocity)
    {
        _body.velocity = velocity;
    }
}
