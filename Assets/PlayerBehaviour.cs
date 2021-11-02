using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float horizontalForce;
    [SerializeField] private float verticalForce;

    private bool isGrounded;
    private Rigidbody2D rigidbody;

    public Transform ground;
    public float rad;
    public LayerMask groundLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        movement();
        checkIfGrounded();

    }

    private void movement()
    {
        if (isGrounded)
        {
            float deltaTime = Time.deltaTime;

            //keyboard input 
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float jump = Input.GetAxisRaw("Jump");

            //check for flip 
            if(x != 0)
            {
                x = flipAnimation(x);
            }

            //touch input 
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce ;
            float jumpMoveForce = jump * verticalForce ;

            rigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce));

            rigidbody.velocity *= 0.99f;
        }
    }

    private void checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(ground.position, rad, Vector2.down, rad, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    private float flipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ground.position, rad);

            
    }
}
