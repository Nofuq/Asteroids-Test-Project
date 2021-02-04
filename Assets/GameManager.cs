using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;

    public int numberOfAsteroids;
    public int level;

    public GameObject asteroidLarge;

    void Start()
    {
        
        level = 0;
        StartNewLevel();
    }


    public void UpdateNumberOfAsteroids(int change)
    {
        numberOfAsteroids += change;
        if (numberOfAsteroids <= 0)
        {
            Invoke("StartNewLevel", 3f);
        }

    }

    void StartNewLevel()
    {
        level++;
        numberOfAsteroids = 0;
        for (int i = 0; i < level * 2; i++)
        {
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        Vector2 asteroidPos;
        var fixedValue = Random.value;
        if (fixedValue < 0.25f)
        {
            asteroidPos.x = screenLeft;
            asteroidPos.y = Random.Range(screenBottom, screenTop);
        }
        else if (fixedValue < 0.5f)
        {
            asteroidPos.x = Random.Range(screenLeft, screenRight);
            asteroidPos.y = screenTop;
        }
        else if (fixedValue < 0.75f)
        {
            asteroidPos.x = screenRight;
            asteroidPos.y = Random.Range(screenBottom, screenTop);
        }
        else
        {
            asteroidPos.x = Random.Range(screenLeft, screenRight);
            asteroidPos.y = screenBottom;
        }

        numberOfAsteroids++;
        GameObject asteroid1 = Instantiate(asteroidLarge, asteroidPos, Quaternion.identity);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
