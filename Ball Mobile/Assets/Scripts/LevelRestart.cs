using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{

    DragShoot player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<DragShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.canMove && player.isShoot && player.ballVelocity == 0)
        {
            StartCoroutine(GameRestart());
        }
    }

    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Test Scene");

    }
}
