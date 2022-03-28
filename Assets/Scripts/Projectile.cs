using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float movementSpeed;

    // private float[] _fixedPositionY = new float[] {-4.5f, 0.0f, 4.5f}; // is this necessary?
    private float[] _fixedPositionX = new float[] {-3, 3};
    private string startPosition;
    private SpriteRenderer projectile;
    private Vector3 fallingLeft = new Vector3(-1f,-.25f,0f);
    private Vector3 fallingRight = new Vector3(1f,-.25f,0f);

    // Allows Sprite to have multiple colliders
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        projectile = GetComponent<SpriteRenderer>();
        int randomPositionX = Random.Range(0, 2); // only range with float is maximally inclusive, int is not.
        float randomPositionY = Random.Range(-4f, 4.5f);

        if (_fixedPositionX[randomPositionX] == -3) {
            startPosition = "left";
            projectile.flipX = true;
            
        } else {
            startPosition = "right";
            projectile.flipX = false;
            
        }

        transform.position = new Vector3(_fixedPositionX[randomPositionX], randomPositionY, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startPosition == "left") {
            transform.position += fallingRight * movementSpeed * Time.deltaTime;
            if (transform.position.x >= 3.0f) {
                gameObject.SetActive(false);
            }
        } else {
            transform.position += fallingLeft * movementSpeed * Time.deltaTime;
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
    }

    //Collision Animation Marker
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

}