using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float movementSpeed = 3;

    private float[] _fixedPositionX = new float[] {-1.5f, -1.0f, 0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f};
    private Vector3 fallingDownLeft = new Vector3 (-.25f,-1f,0f);

    // Allows Sprite to have multiple colliders
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        int randomPositionX = Random.Range(0, 10);
        transform.position = new Vector3(_fixedPositionX[randomPositionX], 6.5f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += fallingDownLeft * movementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Dead Zone")
        {
            gameObject.SetActive(false);             
        }
    }

    //Collision Animation Marker
    public void SetColliderForSprite( int spriteNum )
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

}
