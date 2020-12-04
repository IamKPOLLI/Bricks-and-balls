using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BombController : MonoBehaviour
{
    [SerializeField] private Text _brickHealthText;
    public float brickHealth;
    public Vector2 size;
    public const int damage = 3;
    public Vector3 shift ;



    private void Awake()
    {
        Messenger.AddListener(GameEvent.Shift_Down, shiftDown);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.Shift_Down, shiftDown);
    }

    void Start()
    {
        
        size = new Vector2(3, 3);
        shift = new Vector3(0, 0.72f, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        _brickHealthText.text = "" + brickHealth;
        if (brickHealth <= 0)
        {
            boom();
            Destroy(this.gameObject);
            Messenger.Broadcast(GameEvent.Dead_Brick);
        }
    }

    public void setHealth(float health)
    {
        brickHealth = health;
    }

    public void takeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            takeDamage(ballController.getDamage());
        }

        if (collision.gameObject.tag == "BallShadow")
        {
            BallShadowController ballController = collision.gameObject.GetComponent<BallShadowController>();
            takeDamage(ballController.getDamage());
        }

        
    }


    

    public void boom()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 360);
        for (int i = 0; i < colliders.Length; i++)
        {
           
            if (colliders[i].gameObject.tag == "Brick")
            {
                colliders[i].GetComponent<BrickHealthManager>().takeDamage(damage);
            }
        }
    }

    public void shiftDown()
    {
        transform.position -= shift;
        if (transform.position.y <= -6.28f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void teleportToPosition(Vector2 pos)
    {
        transform.position = pos;
    }






}
