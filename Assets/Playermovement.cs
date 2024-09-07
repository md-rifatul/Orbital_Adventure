using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

   [SerializeField] private Vector3 resp;
   [SerializeField] private AudioSource deathSoundEffect;
   //private int life=3;
   [SerializeField] private AudioSource jumpsoundeffect;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;

     public static float dirx = 0f;
     private float horizontalMove;
     private bool moveLeft; 

    private bool moveRight;
      [SerializeField] private float moveSpeed=7f;
     [SerializeField] private float JumpForce=10f;
     private enum MovementState {idle,running,jump,falling};
     //private MovementState state=MovementState.idle;




    // Start is called before the first frame update
   
    private void Start()
    {
        moveLeft = false; 
        moveRight = false;
        rb=GetComponent<Rigidbody2D>();
        coll=GetComponent<BoxCollider2D>();
        anim=GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
       resp=transform.position;

    }
    //I am pressing the left button 

public void PointerDownLeft() 

{ 

moveLeft = true; 

} 

 

//I am not pressing the left button 

public void PointerUpLeft() 

{ 

moveLeft = false; 

} 

 

//Same thing with the right button 

public void PointerDownRight() 

{ 

moveRight = true; 

} 

 

public void PointerUpRight() 

{ 

moveRight = false; 

}


    
    // Update is called once per frame
    private void MovementPlayer() 

{ 

//If I press the left button 

if (moveLeft) 

{ 
dirx=-1f;
horizontalMove = -moveSpeed; 

} 

 

//if i press the right button 

else if (moveRight) 

{ 
dirx=1f;
horizontalMove = moveSpeed; 

} 

 

//if I am not pressing any button 

else 

{ 
dirx=0f;
horizontalMove = 0; 

} 

}
    private void Update()
    {
        MovementPlayer();
        //dirx= Input.GetAxisRaw("Horizontal");

        rb.velocity=new Vector2(horizontalMove,rb.velocity.y);

        if(Input.GetButtonDown("Jump")&& IsGround()){
            jumpsoundeffect.Play();
            rb.velocity= new Vector2(rb.velocity.x,JumpForce);
            
        }

       UpdateAnimation();
      
        
    }
  
    public void jumpButton() 
{ 

if(IsGround()) 

{ 


rb.velocity = Vector2.up * JumpForce; 



} 



} 

    private void UpdateAnimation()
    {
        MovementState state;
        if(dirx > 0f){
           state=MovementState.running;
             sprite.flipX=false;
           // transform.Rotate(0f,180f,0f);
        }
        else if(dirx < 0f){
           state=MovementState.running;
            sprite.flipX=true;
          // transform.Rotate(0f,0f,0f);
        }
        else{
            state=MovementState.idle;
        }
        if(rb.velocity.y>.1f){
            state=MovementState.jump;
        }
        else if(rb.velocity.y<-.1f){
            state=MovementState.falling;
        }
        anim.SetInteger("state",(int)state);
    }
    private bool IsGround(){
       return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpableGround);
    }


 private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
           // Die();
           
            Health_Manager.health--;
               if( Health_Manager.health==0){
                
                 deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     
    
               }
               else{
                transform.position=resp;
               }
        }
        
    }
      private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("CheckPoint")){

           /* if(Health_Manager.health==0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
            else{
                 
            }*/
        //        anim.SetTrigger("Check");
              
              resp=transform.position;
                
     }
  
}
 private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     
    }
}