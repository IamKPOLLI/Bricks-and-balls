using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{

    [SerializeField] private Text _brickHealthText;
    public float brickHealth;
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

    public void TakeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }

    public void SetHealth(float health)
    {
        brickHealth = health;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            TakeDamage(ballController.GetDamage());
        }

        if (collision.gameObject.CompareTag("BallShadow"))
        {
            BallShadowController ballController = collision.gameObject.GetComponent<BallShadowController>();
            TakeDamage(ballController.GetDamage());
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

    public void TeleportToPosition(Vector2 pos)
    {
        transform.position = pos;
    }

}
