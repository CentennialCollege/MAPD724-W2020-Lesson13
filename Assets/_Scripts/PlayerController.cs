using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState
{   
    NONE,
    IDLE,
    WALK,
    JUMP
}



public class PlayerController : MonoBehaviour
{

    [SerializeField] 
    public Transform spawnPoint;


    [Header("Animation")]
    [SerializeField] 
    public Animator animController;

    [SerializeField]
    public AnimState animation = AnimState.IDLE;

    [Header("Physics")]
    [SerializeField]
    public Rigidbody2D body;

    [SerializeField] 
    public float horizontalForce;

    [SerializeField]
    public float verticalForce;

    [SerializeField] 
    public float maxHorizontalVelocity;

    [SerializeField]
    public float maxVerticalVelocity;

    [SerializeField] 
    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        horizontalForce = 5.0f;
        verticalForce = 800.0f;
        maxHorizontalVelocity = 10.0f;
        maxVerticalVelocity = 10.0f;
        isGrounded = false;

        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // move right
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            animation = AnimState.WALK;
            animController.SetInteger("AnimState", 1);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            body.AddForce(new Vector2(horizontalForce, 0.0f));

            
            var xVelocity = Mathf.Clamp(body.velocity.x, 0.0f, maxHorizontalVelocity);
            var yVelocity = body.velocity.y;
            body.velocity = new Vector2(xVelocity, yVelocity);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            animation = AnimState.WALK;
            animController.SetInteger("AnimState", 1);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            body.AddForce(new Vector2(-horizontalForce, 0.0f));

            var xVelocity = Mathf.Clamp(body.velocity.x, -maxHorizontalVelocity, 0.0f);
            var yVelocity = body.velocity.y;
            body.velocity = new Vector2(xVelocity, yVelocity);

        }

        if (isGrounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
            {
                animation = AnimState.JUMP;
                animController.SetInteger("AnimState", 2);
                body.AddForce(new Vector2(0.0f, verticalForce));

                var xVelocity = body.velocity.x;
                var yVelocity = Mathf.Clamp(body.velocity.y, 0.0f, maxVerticalVelocity);
                body.velocity = new Vector2(xVelocity, yVelocity);

                isGrounded = false;
            }
        }


        if (isGrounded)
        {
            if ((Input.GetAxis("Horizontal") >= -0.1f) && (Input.GetAxis("Horizontal") <= 0.1f))
            {
                animation = AnimState.IDLE;
                animController.SetInteger("AnimState", 0);
            }
        }



    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
