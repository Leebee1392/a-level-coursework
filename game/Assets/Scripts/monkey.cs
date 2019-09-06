using UnityEngine;


public class monkey : MonoBehaviour
{
    private Vector2Int position;
    private Vector2Int direction;
    private float timeSinceLastMoved;
    private readonly float frequencyOfMovement = 1f;

    private void Awake()
    {
        position = new Vector2Int(10,10);
        timeSinceLastMoved = frequencyOfMovement;
    }

    private void Update()
    {
        HandleInput();

        timeSinceLastMoved = timeSinceLastMoved + Time.deltaTime;
        if (timeSinceLastMoved >= frequencyOfMovement)
        {
            position = position + direction;
            timeSinceLastMoved = timeSinceLastMoved - frequencyOfMovement;
        }

        transform.position = new Vector3(position.x, position.y);
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

}
