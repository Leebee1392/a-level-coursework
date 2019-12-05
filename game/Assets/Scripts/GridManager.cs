using UnityEngine;

public class GridManager
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Sprite foodSprite;
    private Monkey monkey;

    public GridManager(int width, int height, Sprite foodSprite)
    {
        this.width = width;
        this.height = height;
        this.foodSprite = foodSprite;
    }

     public void SetUp(Monkey monkey)
     {
         this.monkey = monkey;

        SpawnFood();
    }

    private void SpawnFood()
    {
        // makes sure the food doesn't land on the snake
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width-1), Random.Range(0, height-1));
        } while (monkey.GetSnakeLocations().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

   public bool SnakeMovedAndEaten(Vector2Int snakePosition)
    {
        if (snakePosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Debug.Log("Snake ate food");
            return true;
        }
        else
        {
            return false;
        }
    }

}
