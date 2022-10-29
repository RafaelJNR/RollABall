using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public TextMeshProUGUI countText;
    private int count;
    public float speed;
    private float movementX;
    private float movementY;
    private float movementJump;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        movementJump = 0;
    }

    

    private void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();
        movementX = movement.x;
        movementY = movement.y;
    }

    private void OnJump()
    {
        if(transform.position.y <= 0.7)
        {
            movementJump = 12f;
        }
        
    }

    void SetCountText()
    {

        countText.text = "count: " + count.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementJump , movementY);
        rb.AddForce(movement*speed);
        movementJump = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            if (count == 8)
            {
                Scene ActualScene = SceneManager.GetActiveScene();
                if (ActualScene.name == "SampleScene")
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
               

            }
        }

        if (other.gameObject.CompareTag("AvoidPickUp"))
        {
            count--;
            SetCountText();

        }

    }
}
