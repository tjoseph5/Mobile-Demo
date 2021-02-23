using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void UndoSlowmotion()
    {
        /*
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Time.timeScale == 1.0f)
        {
            Time.fixedDeltaTime = Time.deltaTime;
        }
        */

        ///*
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime += (0.01f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
        //*/
    }

    private void Update()
    {
        UndoSlowmotion();
    }
}
