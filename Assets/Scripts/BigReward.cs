using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigReward : MonoBehaviour
{
    public float movementSpeed;
    

    // private float[] _fixedPositionY = new float[] {-4.5f, 0.0f, 4.5f};
    private float[] _fixedPositionX = new float[] {-3, 3};
    private string startPosition;
    private SpriteRenderer bigReward;
    private Vector3 fallingLeft = new Vector3(-1f,-.25f,0f);
    private Vector3 fallingRight = new Vector3(1f,-.25f,0f);

    // Allows Sprite to have multiple colliders
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    void OnEnable()
    {
       bigReward = GetComponent<SpriteRenderer>();
        int randomPositionX = Random.Range(0, 2); // only range with float is maximally inclusive, int is not.
        float randomPositionY = Random.Range(-4.5f, 4.5f);

        if (_fixedPositionX[randomPositionX] == -3) {
            startPosition = "left";
            bigReward.flipX = true;
            
        } else {
            startPosition = "right";
            bigReward.flipX = false;
            
        }

        transform.position = new Vector3(_fixedPositionX[randomPositionX], randomPositionY, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startPosition == "left") {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            if (transform.position.x >= 3.0f) {
                gameObject.SetActive(false);
            }
        } else {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            if (transform.position.x <= -3.0f) {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Dead Zone")
        {
            gameObject.SetActive(false);             
        }
        else if (collider.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);          
        }
        else if (collider.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
        else if (collider.gameObject.tag == "Projectile")
        {
            gameObject.SetActive(false);
        }
        else if (collider.gameObject.tag == "Reward")
        {
            gameObject.SetActive(false);
        }
    }

    //Collision Animation Marker
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
}

