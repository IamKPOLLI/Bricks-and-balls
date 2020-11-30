using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{


    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot
    }

    public ballState currentBallState;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.Ball_Stop, collisionWithStart);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.Ball_Stop, collisionWithStart);
    }

    [SerializeField] private GameObject _arrow;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private NumberBallController numContr;
    private Vector2 _mousePosition;
    private const float _startPosotionY = -6.63f;
    private const float _startPosotionX = 0f;
    private float _ballVelocityX;
    private float _ballVelocityY;
    public Vector2 ballVelocity;
    public float speed;
    private int _damage;

    // Start is called before the first frame update
    void Start()
    {
        // TeleportToStart();
        currentBallState = ballState.aim;
        speed = 8f;
        _arrow.SetActive(false);
        _damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBallState)
        {
            case ballState.aim:
                if (Input.GetMouseButton(0))
                {
                    DrawArrow();
                }
                if (Input.GetMouseButtonUp(0) )
                {
                    Shot();
                }
                break;
            case ballState.fire:
                break;
            case ballState.wait:
                break;
            case ballState.endShot:
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
        float diffX = _startPosotionX - tempMousePosition.x;
        float diffY = _startPosotionY - tempMousePosition.y;
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        _arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    public void Shot()
    {
        _arrow.SetActive(false);
        SetMousePosition();
        _ballVelocityX = _mousePosition.x - _startPosotionX;
        _ballVelocityY = _mousePosition.y - _startPosotionY;
         ballVelocity = new Vector2(_ballVelocityX, _ballVelocityY).normalized;
        _body.velocity = ballVelocity * speed;
        setCurrentState(ballState.fire);
        numContr.setState(NumberBallController.NumControllerState.work);
    }
    

    public void TeleportToStart()
    {
        transform.position = new Vector2(_startPosotionX, _startPosotionY);
    }

    public void setCurrentState(ballState state)
    {
        currentBallState = state;
    }

    public int getDamage()
    {
        return _damage;
    }



    public void collisionWithStart()
    {
        _body.velocity = Vector2.zero;
        TeleportToStart();
        setCurrentState(ballState.wait);
    }
}
