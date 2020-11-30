using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{


    [SerializeField] private BallController _ball;
    public int numberOfShadows;
    private int _currentDestroyied;
    private bool _readyToShot;



    void Start()
    {
        _currentDestroyied = 0;
        numberOfShadows = 10;
        _readyToShot = false;
    }



    void Update()
    {
     if(_currentDestroyied == numberOfShadows && _readyToShot)
        {
            _currentDestroyied = 0;
            _ball.setCurrentState(BallController.ballState.aim);
            _readyToShot = false;
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Messenger.Broadcast(GameEvent.Ball_Stop);
            _readyToShot = true;    
        }

        if (collision.gameObject.tag == "BallShadow")
        {
            Destroy(collision.gameObject);
            _currentDestroyied++;
        }
    }

    void setNumberOfShadow(int number)
    {
        numberOfShadows = number;
    }

}
