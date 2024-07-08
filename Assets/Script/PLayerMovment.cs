
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed; //for working from unity setting the speed numbers

    private Rigidbody2D body;

    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        //Grab references from objects
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        //flip player when moving left right jump
        if (horizontalInput >0.01f) 
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();
        //keycode is an enumeration that contains all buttons


        if (Input.GetKey(KeyCode.UpArrow))
            Jump();



        //Set animator parameters
        anim.SetBool("run", horizontalInput!=0); //if arrows keys is pressed run
        anim.SetBool("grounded",grounded);

    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");

        grounded = false;


    }

    private void OnCollisionEnter2D(Collision2D collision) //touching another righbody2d do the thing
    {
        if(collision.gameObject.tag== "Ground")
            grounded = true;
    }
}
