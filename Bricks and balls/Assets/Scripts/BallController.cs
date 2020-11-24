using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Rigidbody2D _body;
    private Vector2 _mousePosition;
    private const float _startPosotionY = -4.063f;
    private const float _startPosotionX = 0f;
    private float _ballVelocityX;
    private float _ballVelocityY;
    private bool _isCanClick;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2 (_startPosotionX, _startPosotionY);
        _isCanClick = true;
        speed = 4f;
        arrow.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _isCanClick)
        {
            DrawArrow();
        }
        if (Input.GetMouseButtonUp(0) && _isCanClick)
        {
            Shot();
        }


    }

    public void SetMousePosition()
    {
       _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    

    public void DrawArrow()
    {
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX = _startPosotionX - tempMousePosition.x;
        float diffY = _startPosotionY - tempMousePosition.y;
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    public void Shot()
    {
        arrow.SetActive(false);
        SetMousePosition();
        _ballVelocityX = _mousePosition.x - _startPosotionX;
        _ballVelocityY = _mousePosition.y - _startPosotionY;
        Vector2 tempVelocity = new Vector2(_ballVelocityX, _ballVelocityY).normalized;
        _body.velocity = tempVelocity * speed;
        _isCanClick = false;
    }
    


}
