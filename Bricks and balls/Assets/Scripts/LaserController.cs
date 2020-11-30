using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserController : MonoBehaviour
{


    [SerializeField] private Text _brickHealthText;
    public int brickHealth;
    public Vector2 size;
    public const int damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        setHealth(5);
        size = new Vector2(0.2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        _brickHealthText.text = "" + brickHealth;
        if (brickHealth <= 0)
        {
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
            boom();
            takeDamage(ballController.getDamage());
        }

        if (collision.gameObject.tag == "BallShadow")
        {
            BallShadowController ballController = collision.gameObject.GetComponent<BallShadowController>();
            boom();
            takeDamage(ballController.getDamage());
        }
    }


    public void boom()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
        drawLines(transform.position, size);
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject.tag == "Brick")
            {
                colliders[i].GetComponent<BrickHealthManager>().takeDamage(damage);
            }
        }
        
    }

    public void drawLines(Vector2 start, Vector2 size)
    {
        GameObject line = new GameObject();
        Vector3 startPos = new Vector3(start.x, start.y - size.y / 2,-0.05f);
        Vector3 stopPos = new Vector3(start.x, start.y + size.y / 2,-0.05f);
        line.transform.position = startPos;
        line.AddComponent<LineRenderer>();
        LineRenderer ln = line.GetComponent<LineRenderer>();
        ln.startColor = Color.white;
        ln.endColor = Color.white;
        ln.startWidth = 0.2f;
        ln.endWidth = 0.2f;
        ln.SetPosition(0, startPos);
        ln.SetPosition(1, stopPos);
        GameObject.Destroy(line, 0.2f);
    }

}
