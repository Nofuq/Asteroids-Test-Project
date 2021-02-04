using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public float maxThrustSpeed;
    public float maxTorqueSpeed;
    public Rigidbody2D rb;

    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;

    public int size;

    public int points;

    public GameObject player;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public GameManager gm;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gm = GameObject.FindObjectOfType<GameManager>();

        Vector2 thrust = new Vector2(Random.Range(-maxThrustSpeed, maxThrustSpeed), Random.Range(-maxThrustSpeed, maxThrustSpeed));
        float torque = Random.Range(-maxTorqueSpeed, maxTorqueSpeed);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);


            if (size == 3)
            {
                GameObject asteroid1 = Instantiate(asteroidMedium, transform.position, transform.rotation);
                GameObject asteroid2 = Instantiate(asteroidMedium, transform.position, transform.rotation);
                gm.UpdateNumberOfAsteroids(1);
            }
            else if (size == 2)
            {
                GameObject asteroid1 = Instantiate(asteroidSmall, transform.position, transform.rotation);
                GameObject asteroid2 = Instantiate(asteroidSmall, transform.position, transform.rotation);
                gm.UpdateNumberOfAsteroids(1);
            }
            else if (size == 1)
            {
                gm.UpdateNumberOfAsteroids(-1);
            }

            player.SendMessage("ScorePoints", points);

            Destroy(gameObject);
        }
        
    }
}
