using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaserController : MonoBehaviour
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



    // Start is called before the first frame update
    void Start()
    {

        size = new Vector2(0.2f, 3f);
        shift = new Vector3(0, 0.72f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _brickHealthText.text = "" + brickHealth;
        if (brickHealth <= 0)
        {
            Destroy(this.gameObject);
            Messenger.Broadcast(GameEvent.Dead_Brick);
        }
    }

    public void SetHealth(float health)
    {
        brickHealth = health;
    }

    public void TakeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }






    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            Boom();
            TakeDamage(ballController.GetDamage());
        }

        if (collision.gameObject.CompareTag("BallShadow"))
        {
            BallShadowController ballController = collision.gameObject.GetComponent<BallShadowController>();
            Boom();
            TakeDamage(ballController.GetDamage());
        }
    }


    public void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
        
        DrawLines(transform.position, size);
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject.CompareTag("Brick"))
            {
                colliders[i].GetComponent<BrickController>().TakeDamage(damage);
            }
        }

        


    }

    public void DrawLines(Vector2 start, Vector2 size)
    {
        GameObject line = new GameObject();
        Vector3 startPos = new Vector3(start.x, start.y - size.y / 2, -0.05f);
        Vector3 stopPos = new Vector3(start.x, start.y + size.y / 2, -0.05f);
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

    public void ShiftDown()
    {
        transform.position -= shift;
        if (transform.position.y <= -6.28f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void TeleportToPosition(Vector2 pos)
    {
        transform.position = pos;
    }

}
