using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class monkey : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    // for floats add a f on the end of a number
    public float moveSpeed = 1f;

    public void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Fixed Update can be run once, zero or serveral times a frame
    // depending on how many physics frames per seconds are set in the time settings
    // and how fast or slow the frame rate is 
    private void FixedUpdate()
    {
        if (playerRigidbody != null)
        {
            ApplyInput();
        }
        else
        {
            Debug.LogWarning("Rigidbody is not attached to player " + gameObject.name);
        }
    }

    public void ApplyInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float xforce = xInput * moveSpeed * Time.deltaTime;

        Vector2 force = new Vector2(xforce, 0);

        playerRigidbody.AddForce(force);
    }
}
