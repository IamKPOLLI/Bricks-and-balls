using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{


    public Vector2[] map = new Vector2[] { new Vector2(1, 14), new Vector2(1, 5), new Vector2(1, 14), new Vector2(1, 10), new Vector2(1, 8), new Vector2(1, 8), new Vector2(1, 5), new Vector2(3, 8), new Vector2(0, 0), new Vector2(0, 0), new Vector2(3, 3), new Vector2(2, 6), new Vector2(2, 6) };
    public Vector2 firstBlockPosition;

    public int numberOfBricks = 0;

    [SerializeField] private BrickController _brickPrefab;
    [SerializeField] private BombController _bombPrefab;
    [SerializeField] private LaserController _laserPrefab;

    public enum GameState
    {
        generate,
        play
    }

    public GameState currentGameState;





    private void Awake()
    {
        Messenger.AddListener(GameEvent.Dead_Brick, DeadBriks);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.Dead_Brick, DeadBriks);
    }

    // Start is called before the first frame update
    void Start()
    {
        firstBlockPosition = new Vector2(-4.5f, 8.36f);
        currentGameState = GameState.generate;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentGameState == GameState.generate)
        {
            GenerateMap(map);
            currentGameState = GameState.play;
        }


    }


    public void GenerateMap(Vector2[] map)
    {

        Vector2 currentBlockPosition = new Vector2(firstBlockPosition.x, firstBlockPosition.y);
        int X = 0;


        Debug.Log(map.Length);
        for (int i = 0; i < map.Length; i++)
        {

            switch (map[i].x)
            {
                case 0:
                    currentBlockPosition.x += 1.3f;
                    X++;
                    if (X == 8)
                    {
                        X = 0;
                        currentBlockPosition.x = firstBlockPosition.x;

                    }
                    break;
                case 1:
                    BrickController brick;
                    brick = Instantiate(_brickPrefab) as BrickController;
                    brick.TeleportToPosition(currentBlockPosition);
                    brick.SetHealth(map[i].y);
                    currentBlockPosition.x += 1.3f;
                    numberOfBricks++;
                    X++;
                    if (X == 8)
                    {
                        X = 0;
                        currentBlockPosition.x = firstBlockPosition.x;
                        currentBlockPosition.y -= 1.3f;

                    }
                    break;

                case 2:
                    BombController bomb;
                    bomb = Instantiate(_bombPrefab) as BombController;
                    bomb.teleportToPosition(currentBlockPosition);
                    bomb.setHealth(map[i].y);
                    currentBlockPosition.x += 1.3f;
                    numberOfBricks++;
                    X++;
                    if (X == 8)
                    {
                        X = 0;
                        currentBlockPosition.x = firstBlockPosition.x;
                        currentBlockPosition.y = currentBlockPosition.y - 1.3f;

                    }
                    break;

                case 3:
                    LaserController laser;
                    laser = Instantiate(_laserPrefab) as LaserController;
                    laser.TeleportToPosition(currentBlockPosition);
                    laser.SetHealth(map[i].y);
                    currentBlockPosition.x += 1.3f;
                    numberOfBricks++;
                    X++;
                    if (X == 8)
                    {
                        X = 0;
                        currentBlockPosition.x = firstBlockPosition.x;
                        currentBlockPosition.y = currentBlockPosition.y - 1.3f;

                    }
                    break;




            }



        }



    }

    public void DeadBriks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            Restart();
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
