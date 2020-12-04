using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NumberBallController : MonoBehaviour
{

    public enum NumControllerState
    {
        work,
        not_work
    }


    public Vector2 startPositions;
    [SerializeField] private BallShadowController _ballPrefab;
    [SerializeField] private BallController mainBall;
    

    private int _numberOfBalls;
    private int _currentNumberBalls;
    private NumControllerState _state;

    // Start is called before the first frame update
    void Start()
    {
        _numberOfBalls = 5;
        _currentNumberBalls = 0;
        _state = NumControllerState.not_work;
        startPositions = new Vector2(0, -6.45f);
         
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == NumControllerState.work)
        {
            InvokeRepeating("AddBall", 0.1f, 0.1f);
            setState(NumControllerState.not_work);
        }
        if(_currentNumberBalls == _numberOfBalls)
        {
            CancelInvoke();
            _currentNumberBalls = 0;
        }
    }

    void AddBall()
    {
        BallShadowController ball;
        ball = Instantiate(_ballPrefab) as BallShadowController;
        ball.TeleportToPosition(startPositions);
        ball.AddMovement(mainBall.ballVelocity * mainBall.speed);
        _currentNumberBalls++;   
    }

    public void setState(NumberBallController.NumControllerState state)
    {
        _state = state;
    }

}
