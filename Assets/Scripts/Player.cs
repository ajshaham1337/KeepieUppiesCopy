using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ScoreController score;
    public AudioSource playOnTrigger;

    public static GameObject playerinstance;

    public float[] lanes = new float[] { -3f, 0f, 3f }; // x position of each lane. note: it is exceeding world bounds in order to work properly?
    private int targetLane = 1; // middle lane

    // public float superJumpMultiplier;
    private float gravity = 0.5f;
    private float jumpHeight = 1.5f;
    private float targetJump = 0f;

    private float laneSwapSpeed = 5f;
    private float jumpSpeed = 5f;

    private bool isSwappingLanes = false;
    private bool isJumping = false;
    private bool isFalling = false;

    // private float skyZone = 4f;
    // private float deadZone = -4f;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public GameObject GameOverScene;
    public GameObject ScoreUI;
    public GameObject PauseButtonUI;

    // Allows Sprite to have multiple colliders
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameOverScene.SetActive(false);
        PauseButtonUI.SetActive(true);
        ScoreUI.SetActive(true);

        targetJump = transform.position.y + jumpHeight; // Initialize tartgetJump position.

        dragDistance = Screen.width * 5 / 100; //dragDistance is 5% width of the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && isJumping == false && isSwappingLanes == false) // user is touching the screen with a single touch and not doing an action currently.
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position; //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 10% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance) //It's a drag
                {
                    Debug.Log("Drag registered");

                    //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   
                        //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x)) // If the movement was to the right
                        {   
                            //Right swipe
                            Debug.Log("Right Swipe");
                            if (isSwappingLanes == false && targetLane < lanes.Length - 1.0)
                            {
                                targetLane++;
                                isSwappingLanes = true;
                                AudioManager.me.playPlayerMoveSFX();
                            }
                        }
                        else
                        {   
                            //Left swipe
                            Debug.Log("Left Swipe");
                            if (isSwappingLanes == false && targetLane > 0)
                            {
                                targetLane--;
                                isSwappingLanes = true;
                                AudioManager.me.playPlayerMoveSFX();
                            }
                        }
                    }
                    else //the vertical movement is greater than the horizontal movement
                    {   
                        if (lp.y > fp.y) //If the movement was up
                        {   
                            //Up swipe
                            Debug.Log("Up Swipe");
                            targetJump = transform.position.y + jumpHeight; //set jump result location
                            isJumping = true; //do the jump
                            AudioManager.me.playPlayerMoveSFX(); // play the jump sound

                        }
                        else
                        {   
                            //Down swipe
                            Debug.Log("Down Swipe");
                            targetJump = transform.position.y - jumpHeight;
                            isJumping = true;
                            isFalling = true;
                            AudioManager.me.playPlayerMoveSFX(); // play the jump sound
                        }
                    }
                }
            }
        }

        /* World rules */
        // Gravity affecting player
        if (isJumping == false)
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }

        /* Vertical movement */
        // Move player towards the target jump's position
        if (isJumping == true && Mathf.Abs(transform.position.y - targetJump) > 0.05f)
        {
            if (isFalling == true)
            {
                transform.position += Vector3.down * jumpSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            }
        }
        // When close enough to the final vertical position, snap to it
        else if (isJumping == true && Mathf.Abs(transform.position.y - targetJump) <= 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lanes[targetLane], targetJump, 0), Time.deltaTime);
            isJumping = false;
            isFalling = false;
        }

        /* Horizontal movement */
        // Move player towards the target lane's position
        if (isSwappingLanes == true && Mathf.Abs(transform.position.x - lanes[targetLane]) > 0.05f)
        {
            if (transform.position.x < lanes[targetLane])
            {
                transform.position += Vector3.right * laneSwapSpeed * Time.deltaTime;
            }
            else if (transform.position.x > lanes[targetLane])
            {
                transform.position += Vector3.left * laneSwapSpeed * Time.deltaTime;
            }
        }
        // When close enough to the final horizontal position, snap to it
        else if (isSwappingLanes == true && Mathf.Abs(transform.position.x - lanes[targetLane]) <= 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lanes[targetLane], transform.position.y, 0), Time.deltaTime); // causes a slight player movement downward cause its on position y not targetjump...
            isSwappingLanes = false;
        }

        // No lives remaining
        if (score.food == 0) {
            EndGame();
        }
    }

    /* Player collision */
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player collisions with world bounds
        if (collider.gameObject.tag == "Sky Zone")
        {
            isJumping = false;
        }
        else if (collider.gameObject.tag == "Dead Zone")
        {
            AudioManager.me.playObstacleHitSFX();
            EndGame();
        }

        // Player collisions with objects
        if (collider.gameObject.tag == "Obstacle")
        {
            AudioManager.me.playObstacleHitSFX();
            EndGame();
        }
        if (collider.gameObject.tag == "Projectile")
        {
            AudioManager.me.playObstacleHitSFX();
            score.MinusHealth();
            score.ResetMultiplier();
        }
        else if (collider.gameObject.tag == "Reward")
        {
            Debug.Log("Food Collected!");
            AudioManager.me.playRewardSFX();
            score.AddHealth();
        }
        else if (collider.gameObject.tag == "Big Reward")
        {
            Debug.Log("Big Reward Collected!");
            AudioManager.me.playRewardSFX();
            score.AddMultiplier();
        }
    }
    
    /* Resolve game over */
    void EndGame()
    {
        Debug.Log("Game Over!");
        score.SetHighScore();
        Time.timeScale = 0f;
        GameOverScene.SetActive(true);
        PauseButtonUI.SetActive(false);
        ScoreUI.SetActive(false);
        AudioManager.me.pauseGameMusic();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Collision Animation Marker
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
}
