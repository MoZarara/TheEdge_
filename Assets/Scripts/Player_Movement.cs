using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    
    private CharacterController m_char;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -22.81f;

    private Animator anim;
    private bool inJump;
    private bool inHit;

    [HideInInspector]
    public bool m_Jump;
    [HideInInspector]
    public float hori_Input;
    [HideInInspector]
    public float ver_Input;

    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;

    [Header ("Explosion Prefab")]
    public GameObject fireExplosion;


    public Controller controller;

    /*[HideInInspector]
    public bool playerDied;*/

    [Header("")]
    public AudioClip walkAudio;
    public AudioClip jumpAudio;
    public AudioClip dieAudio;
    public AudioSource playerAudioSourceWalk;
    public AudioSource playerAudioSourceJump;

    public bool jumpNow = false;
    
    private void Start()
    {
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }


        m_char = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        //playerDied = false;
        //playerAudioSource = GetComponent<AudioSource>();
        playerAudioSourceWalk.mute = true;
        playerAudioSourceJump.mute = true;
        //playerAudioSource.mute = true;
    }

    void Update()
    {
        if (!controller.playerStop)
        {


            groundedPlayer = m_char.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;

            }

            //float xHorizontal = /*Input.GetAxis("Horizontal")*/ CrossPlatformInputManager.GetAxis("Horizontal") ;
            //float zVertical = /*Input.GetAxis("Vertical")*/ CrossPlatformInputManager.GetAxis("Vertical") ;


            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = ver_Input * m_CamForward + hori_Input * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = ver_Input * Vector3.forward + hori_Input * Vector3.right;
            }



            //Vector3 move = new Vector3(/*xHorizontal*/ hori_Input, 0, /*zVertical*/ ver_Input);
            m_char.Move(/*move*/ m_Move * Time.deltaTime * playerSpeed);


            if (m_Move != Vector3.zero)
            {
                gameObject.transform.forward = m_Move;

                if (!inJump && groundedPlayer && !inHit)
                {
                    anim.Play("walk");
                    playerAudioSourceJump.mute = true;
                    playerAudioSourceWalk.mute = false;
                    //playerAudioSource.loop = true;
                    //playerAudioSource.clip = walkAudio;                                       
                    
                    
                    //anim.Play("Berserk_townwalk_01_hu");
                }
            }
            else
            {
                if (!inJump && groundedPlayer && !inHit)
                {
                    anim.Play("idle");
                    //playerAudioSource.mute = true;
                    playerAudioSourceJump.mute = true;
                    playerAudioSourceWalk.mute = true;
                    
                    //anim.Play("Berserk_lobbyidle_01_hu");

                }
            }

            // Changes the height position of the player..

            /*if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }*/

            /*if (controller.returnAfterDied) {

                playerVelocity.y = 0;

            }*/

            


            if (!jumpNow && m_Jump && groundedPlayer)
            {
               

                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                    anim.Play("Jump");

                    playerAudioSourceJump.mute = false;
                    playerAudioSourceWalk.mute = true;

                    playerAudioSourceJump.Play();

                    inJump = true;
               
            }

            
            

            if (groundedPlayer)
            {
                if (m_Jump)
                {
                    jumpNow = true;
                }
                else {
                    jumpNow = false;
                }


                inJump = false;
            }
            else
            {
                anim.Play("Jump");
                playerAudioSourceWalk.mute = true;
                /*playerAudioSource.loop = false;
                playerAudioSource.clip = jumpAudio;
                playerAudioSource.mute = false;*/
            }





            playerVelocity.y += gravityValue * Time.deltaTime;
            m_char.Move(playerVelocity * Time.deltaTime);

        }
        else {
            if (m_char.isGrounded) {
                anim.Play("idle");
            }
            
            playerAudioSourceJump.mute = true;
            playerAudioSourceWalk.mute = true;
        }
    }


   


    private void LateUpdate()
    {
        
            if (transform.position.y <= -20.0f /*&& !playerDied*/) {
                StartCoroutine(controller.FinallyGame("Game Over", 0.0f));               
                //Debug.Log("kkkk");
               // playerDied = true;
            //controller.OnPause(false);
            }
       
    }


    /*
    public void PlayerHit() {
        //anim.Play("Berserk_attack_01_hu");
        anim.Play("attack_1");
        StartCoroutine(HitTime());
        
    }

    IEnumerator HitTime() {
        inHit = true;
        yield return new WaitForSeconds(1.0f);
        inHit = false;
    }
    */










    /////////////////////////////////////////////////////////
    /*
     private bool moveLeft, moveRight, moveUp, moveDown;
     private CharacterController m_char;
     private Animator anim;
     float x, y;
     public float speedMoveSide;
     public float jumpPower = 7f;
     public bool inJump, inRoll;
     public float fWdSpeed = 7f;


     private void Start()
     {
         m_char = GetComponent<CharacterController>();
         anim = GetComponent<Animator>();
     }

     void Update()
     {
         moveLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ;
         moveRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ;



         moveUp = Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump");
         moveDown = Input.GetKeyDown(KeyCode.LeftShift) || CrossPlatformInputManager.GetButtonDown("Roll");

         float rightLeft =  CrossPlatformInputManager.GetAxis("Horizontal");
         float backwardAndForward =  CrossPlatformInputManager.GetAxis("Vertical");



         Vector3 moveVector = new Vector3(
             (rightLeft * speedMoveSide) * Time.deltaTime,
             y * Time.deltaTime, 
             (fWdSpeed * backwardAndForward) * Time.deltaTime);


         if (backwardAndForward != 0)
         {
             if (!inJump)
                 anim.Play("walk");

         }
         else {
             if (!inJump)
                 anim.Play("idle");

         }

         if (y != 0) {
             inJump = false;
         }

         m_char.Move(moveVector);


         Jump();
         Roll();

     }




     private void Jump()
     {
         if (m_char.isGrounded)
         {

             if (moveUp)
             {
                 y = jumpPower;
                 inJump = true;
             }


         }
         else
         {
             y -= jumpPower * 2f * Time.deltaTime;
             anim.Play("Jump");

         }

     }



         private void Roll()
         {
             y -= Time.deltaTime;




             if (moveDown)
             {

                 //y = 0.5f;
                 y -= 10f;

                 //m_char.center = new Vector3(0, colCenterY / 2f, 0);
                 //m_char.height = colHeight / 2f;

                 inRoll = true;
                 inJump = false;
             }

         }
     */


   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thorns")
        {
            ExplosionPlayer();
            StartCoroutine( controller.FinallyGame("Game Over", 0.3f));
        }

       
    }*/


    public void ExplosionPlayer() {
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        gameObject.SetActive(false);
        GameObject explosion = Instantiate(fireExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);
    }

   

}
