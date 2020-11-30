using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickHealthManager : MonoBehaviour
{

    [SerializeField] private Text _brickHealthText;
    public int brickHealth;
    // Start is called before the first frame update
    void Start()
    {
        setHealth(40);
    }

    // Update is called once per frame
    void Update()
    {
        _brickHealthText.text = "" + brickHealth;
        if(brickHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void takeDamage(int damageToTake)
    {
        brickHealth  -= damageToTake;
    }

    public void setHealth(int health)
    {
        brickHealth = health;
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

}
