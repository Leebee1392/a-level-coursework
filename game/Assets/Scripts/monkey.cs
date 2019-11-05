using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    // enums - shortens code, easier to read
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }
    private Vector2Int currentLocation;
    private Direction direction;
    public float timeSinceLastMoved;
    public readonly float frequencyOfMovement = 1f;
    public GridManager gridManager;
    private int snakeBodySize;
    private List<SnakeMovePosition> previousLocations;
    private List<SnakeBodyPart> snakeBodyPartList;
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
        previousLocations = new List<SnakeMovePosition>();
        direction = Direction.Right;
        snakeBodySize = 0;
        snakeBodyPartList = new List<SnakeBodyPart>();
    }

    public void Update()
    {
        HandleInput();

        timeSinceLastMoved = timeSinceLastMoved + Time.deltaTime;

        if (timeSinceLastMoved >= frequencyOfMovement)
        {
            SnakeMovePosition previousSnakeMovePosition = null;

            // checks if we have at least one position in the list and make sure we don't
            // pick the previous location
            if (previousLocations.Count > 0)
            {
                previousSnakeMovePosition = previousLocations[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition,currentLocation, direction);
            // adds the position to the list
            previousLocations.Insert(0, snakeMovePosition);

            timeSinceLastMoved = timeSinceLastMoved - frequencyOfMovement;

            Vector2Int directionVector;
            switch (direction)
            {
                default:
                case Direction.Right:
                    directionVector = new Vector2Int(1, 0);
                    break;

                case Direction.Left:
                    directionVector = new Vector2Int(-1, 0);
                    break;

                case Direction.Up:
                    directionVector = new Vector2Int(0, 1);
                    break;

                case Direction.Down:
                    directionVector = new Vector2Int(0, -1);
                    break;
            }

            currentLocation = currentLocation + directionVector;

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
            transform.eulerAngles = new Vector3(0, 0, GetAngleVector(directionVector) - 90);


            UpdateSnakeBodyParts();

        }      
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count, body));
    }

    private void UpdateSnakeBodyParts()
    {
        // working out where the snake is
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(previousLocations[i]);
        }
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
        if (direction != Direction.Down)
        {
            direction = Direction.Up;
        }
    }

    private void SetDirectionDown()
    {
        // you can only go down if you are not currently going up
        if (direction != Direction.Up)
        {
            direction = Direction.Down;
        }
    }

    private void SetDirectionLeft()
    {
        // you can only go left if you are not currently going right
        if (direction != Direction.Right)
        {
            direction = Direction.Left;
        }
    }

    private void SetDirectionRight()
    {
        // you can only go right if you are not currently going left
        if (direction != Direction.Left)
        {
            direction = Direction.Right;
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
        foreach(SnakeMovePosition snakeMovePosition in previousLocations)
        {
            locations.Add(snakeMovePosition.GetCurrentLocation());
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


    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex, Sprite body)
        {
            GameObject snakeBodyGameObject = new GameObject("body", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = body;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetCurrentLocation().x, snakeMovePosition.GetCurrentLocation().y);

            // to make the monkey face the right way
            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                // currently going up
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        // previously was going left
                        case Direction.Left:
                            angle = 0 + 45;
                            break;
                        // previously was going right
                        case Direction.Right:
                            angle = 0 - 45;
                            break;
                    }
                    break;

                        // currently going down
                        case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        // previously was going left
                        case Direction.Left:
                            angle = 180 + 45;
                            break;
                        // previously was going right
                        case Direction.Right:
                            angle = 180 - 45;
                            break;
                    }
                    break;

                // currently going left
                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        // previously was going down
                        case Direction.Down:
                            angle = -45;
                            break;
                        // previously was going up
                        case Direction.Up:
                            angle = 45;
                            break;
                    }
                    break;

                // currently going to the right
                case Direction.Right:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        // previously was going down
                        case Direction.Down:
                            angle = 45;
                            break;
                        // previously was going up
                        case Direction.Up:
                            angle = -45;
                            break;
                    }
                    break;

            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

   
    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int currentLocation;
        private Direction direction;

        //constructor
        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int currentLocation, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.currentLocation = currentLocation;
            this.direction = direction;
        }

        public Vector2Int GetCurrentLocation()
        {
            return currentLocation;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }

    }

}


