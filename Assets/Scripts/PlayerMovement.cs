using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myPlayer;
    private Animator anim;
    private SpriteRenderer sr;
    private float movementX;
    private float movementY;

    private float moveForce=10f;
    private float jumpForce=11f;
    public GameObject snake_body;
    public Transform location;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        movementX=Input.GetAxisRaw("Horizontal");
        movementY=Input.GetAxisRaw("Vertical");
        transform.position+=new Vector3(movementX*moveForce,movementY*4,0f)*Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag=="fruit")
        {
            Destroy(other.gameObject);
            Instantiate(snake_body,new Vector3(location.position.x,location.position.y,0f),Quaternion.identity);
        }
    }

}
