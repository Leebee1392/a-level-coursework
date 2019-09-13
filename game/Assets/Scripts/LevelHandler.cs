using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Monkey monkey;
    private GridManager gridManager;

    public Sprite food;

    private void Start()
    {
        Debug.Log("Start");
        gridManager = new GridManager(20, 20, food);

        // this is giving the gridmanager the monkey object and the monkey the gridmanager object
        monkey.Setup(gridManager);
        gridManager.SetUp(monkey);
    }
}
