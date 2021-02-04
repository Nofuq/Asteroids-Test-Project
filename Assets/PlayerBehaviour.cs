using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float thrustSpeed = 5f;
    public float bulletForce = 20f;
    public float forceToKill;


    public Rigidbody2D rb;
    float thrust;
    float rotation;

    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;

    private int score = 0;
    public int lives = 3;

    public Text scoreText;
    public Text livesText;


    public Transform firePoint;
    public GameObject bullet;

    public GameObject gameOverPanel;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        score = 0;

        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        thrust = Input.GetAxisRaw("Vertical");
        rotation = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 7f);
        }

        //ScreenWraping

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

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * thrust * thrustSpeed);

        rb.AddTorque(-rotation);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > forceToKill)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                player.SetActive(false);
                gameOverPanel.SetActive(true);
            }
            Debug.Log($"Dead: {other.relativeVelocity.magnitude}");
        }
    }

    void ScorePoints(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
