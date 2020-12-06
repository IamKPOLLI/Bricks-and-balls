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
        numberOfShadows = 3;
        _readyToShot = false;
    }



    void Update()
    {
        if (_currentDestroyied == numberOfShadows && _readyToShot)
        {
            _currentDestroyied = 0;
            Messenger.Broadcast(GameEvent.Shift_Down);
            _ball.SetCurrentState(BallController.BallState.aim);
            _readyToShot = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("Ball"))
        {
            Messenger.Broadcast(GameEvent.Ball_Stop);
            _readyToShot = true;
        }

        if (collision.gameObject.CompareTag("BallShadow"))
        {
            Destroy(collision.gameObject);
            _currentDestroyied++;
        }

        

    }


}
