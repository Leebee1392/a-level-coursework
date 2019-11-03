using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    private Vector2Int currentLocation;
    public Vector2Int direction;
    public float timeSinceLastMoved;
    public readonly float frequencyOfMovement = 1f;
    public GridManager gridManager;
    private int snakeBodySize;
    private List<Vector2Int> previousLocations;
    private List<Transform> snakeBodyTransformList;
    public Sprite body;


    public void Setup(GridManager gridManager, Sprite body)
    {
        this.gridManager = gridManager;
        this.body = body;
    }


    private void Awake()
    {
        currentLocation = new Vector2Int(10, 10);
        timeSinceLastMoved = frequencyOfMovement;
        previousLocations = new List<Vector2Int>();
        snakeBodySize = 0;
        snakeBodyTransformList = new List<Transform>();
    }

    public void Update()
    {
        HandleInput();

        timeSinceLastMoved = timeSinceLastMoved + Time.deltaTime;
        if (timeSinceLastMoved >= frequencyOfMovement)
        {
            // adds the position to the list
            previousLocations.Insert(0, currentLocation);

            currentLocation = currentLocation + direction;
            timeSinceLastMoved = timeSinceLastMoved - frequencyOfMovement;

            bool snakeAteFood = gridManager.SnakeMovedAndEaten(currentLocation);
            if (snakeAteFood)
            {
                // Monkey ate food so grow body
                snakeBodySize++;
                CreateSnakeBody();
                Debug.Log("Current Snake Size" + snakeBodySize);
            }

            // check how many elements are in the list
            // if its bigger than the body size, - 1 from the end of the snake
            if (previousLocations.Count >= snakeBodySize + 1)
            {
                previousLocations.RemoveAt(previousLocations.Count - 1);
            }

            transform.position = new Vector3(currentLocation.x, currentLocation.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleVector(direction) - 90);

        }      
    }

    private void CreateSnakeBody()
    {
        GameObject snakeBodyGameObject = new GameObject("body", typeof(SpriteRenderer));
        snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = body;
        snakeBodyTransformList.Add(snakeBodyGameObject.transform);
    }

    private void HandleInput()
    {
        if (HasUserPressedUp())
        {
            SetDirectionUp();
        }
        else if (HasUserPressedDown())
        {
            SetDirectionDown();
        }
        else if (HasUserPressedLeft())
        {
            SetDirectionLeft();
        }
        else if (HasUserPressedRight())
        {
            SetDirectionRight();
        }
    }

    private bool HasUserPressedUp()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    private bool HasUserPressedDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }

    private bool HasUserPressedLeft()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
    }

    private bool HasUserPressedRight()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }

    private void SetDirectionUp()
    {
        // you can only go up if you are not currently going down
        if (direction.y != -1)
        {
            direction.x = 0;
            direction.y = 1;
        }
    }

    private void SetDirectionDown()
    {
        // you can only go down if you are not currently going up
        if (direction.y != 1)
        {
            direction.x = 0;
            direction.y = -1;
        }
    }

    private void SetDirectionLeft()
    {
        // you can only go left if you are not currently going right
        if (direction.x != 1)
        {
            direction.x = -1;
            direction.y = 0;
        }
    }

    private void SetDirectionRight()
    {
        // you can only go right if you are not currently going left
        if (direction.x != -1)
        {
            direction.x = 1;
            direction.y = 0;
        }
    }

    private float GetAngleVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n > 0)
        {
            n = n + 360;
        }
        return n;
    }

    public List<Vector2Int> GetSnakeLocations()
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        locations.Add(currentLocation);
        foreach(Vector2Int previousLocation in previousLocations)
        {
            locations.Add(previousLocation);
        }

        // Debug Logging
        string positions = "";
        foreach(Vector2Int location in locations)
        {
            positions += location.ToString() + ", ";
        }
        Debug.Log("Current Snake Locations :" + positions);

        return locations;
    }
}


