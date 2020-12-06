using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    [SerializeField] private Text _brickHealthText;
    public float brickHealth;
    public Vector2 size;
    public const int damage = 3;
    public Vector3 shift;



    private void Awake()
    {
        Messenger.AddListener(GameEvent.Shift_Down, ShiftDown);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.Shift_Down, ShiftDown);
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
            Boom();
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





    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            takeDamage(ballController.GetDamage());
        }

        if (collision.gameObject.CompareTag("BallShadow"))
        {
            BallShadowController ballController = collision.gameObject.GetComponent<BallShadowController>();
            takeDamage(ballController.GetDamage());
        }
    }




    public void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 360);
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject.CompareTag("Brick"))
            {
                colliders[i].GetComponent<BrickController>().TakeDamage(damage);
            }
        }
    }

    public void ShiftDown()
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
