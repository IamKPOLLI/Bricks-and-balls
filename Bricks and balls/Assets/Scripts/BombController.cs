using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    [SerializeField] private Text _brickHealthText;
    public int brickHealth;
    public Vector2 size;
    public const int damage = 3;
    
    void Start()
    {
        setHealth(5);
        size = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        _brickHealthText.text = "" + brickHealth;
        if (brickHealth <= 0)
        {
            boom();
            Destroy(this.gameObject);
        }
    }

    public void setHealth(int health)
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


    




}
