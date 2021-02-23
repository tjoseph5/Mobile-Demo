using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{

    GameObject playerBall;

    // Start is called before the first frame update
    void Start()
    {
        playerBall = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBall.GetComponent<DragShoot>().canMove && playerBall.GetComponent<DragShoot>().isShoot && playerBall.GetComponent<DragShoot>().ballVelocity == 0)
        {
            StartCoroutine(GameRestart());
        }
    }

    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        Destroy(playerBall);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Test Scene");

    }
}
