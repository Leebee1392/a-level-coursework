using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Monkey monkey;
    private GridManager gridManager;

    public Sprite food;
    public Sprite body;
    public Sprite powerUp;

    private void Start()
    {
        Debug.Log("Start");
        gridManager = new GridManager(20, 20, food, powerUp);

        // this is giving the gridmanager the monkey object and the monkey the gridmanager object
        monkey.Setup(gridManager, body, powerUp);
        gridManager.SetUp(monkey);
    }
}
