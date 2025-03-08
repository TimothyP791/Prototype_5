using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Variables for ball or bomb mechanics
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explosionParticle;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        //References to ball or bomb's rigidbody
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos(); 

        // Game Manager reference to update score and cause game over
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //checks to see when mouse clicks an object to destroy it and update the score
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
       
    }

    //Checks if objects collide with the sensor which is the only collider with a trigger
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    //Generates a random force to apply to the ball or bomb
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    //Gives random spin effect to each ball or bomb
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    //Generates a random spawn position for the ball or bomb
    Vector3 RandomSpawnPos()
    {
        // Note Doesn't need z coordinate since it's 2D and we are not changing that direction
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
