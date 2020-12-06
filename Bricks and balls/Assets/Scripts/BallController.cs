using UnityEngine;

public class BallController : MonoBehaviour
{


    public enum BallState
    {
        aim,
        fire,
        wait,
        endShot
    }

    public BallState currentBallState;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.Ball_Stop, CollisionWithStart);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.Ball_Stop, CollisionWithStart);
    }

    [SerializeField] private GameObject _arrow;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private NumberBallController numContr;
    private Vector2 _mousePosition;
    public Vector2 currentPosBeforeShot;
    private Vector2 _startPosition = new Vector2(0, -6.45f);
    private float _ballVelocityX;
    private float _ballVelocityY;
    public Vector2 ballVelocity;
    public float speed;
    private int _damage;

    // Start is called before the first frame update
    void Start()
    {
        // TeleportToStart();
        currentPosBeforeShot = _startPosition;
        currentBallState = BallState.aim;
        speed = 8f;
        _arrow.SetActive(false);
        _damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBallState)
        {
            case BallState.aim:
                if (Input.GetMouseButton(0))
                {
                    DrawArrow();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Shot();
                }
                break;
            case BallState.fire:
                break;
            case BallState.wait:
                break;
            case BallState.endShot:
                break;
        }




    }

    public void SetMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    public void DrawArrow()
    {
        _arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX = transform.position.x - tempMousePosition.x;
        float diffY = transform.position.y - tempMousePosition.y;
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        _arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    public void Shot()
    {
        _arrow.SetActive(false);
        SetMousePosition();
        _ballVelocityX = _mousePosition.x - transform.position.x;
        _ballVelocityY = _mousePosition.y - transform.position.y;
        ballVelocity = new Vector2(_ballVelocityX, _ballVelocityY).normalized;
        _body.velocity = ballVelocity * speed;
        SetCurrentState(BallState.fire);
        numContr.SetState(NumberBallController.NumControllerState.work);
    }


    public void TeleportToStart()
    {
        transform.position = _startPosition;
    }

    public void SetCurrentState(BallState state)
    {
        currentBallState = state;
    }

    public int GetDamage()
    {
        return _damage;
    }



    public void CollisionWithStart()
    {
        _body.velocity = Vector2.zero;
        currentPosBeforeShot = transform.position;
        //   TeleportToStart();
        SetCurrentState(BallState.wait);
    }
}
