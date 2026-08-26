using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float duration = 60f; // Duration in seconds
    public TMPro.TMP_Text countdownText; // Reference to the UI Text component

    private Coroutine countdownCoroutine;

    void Start()
    {
        // Start the countdown coroutine
        this.StartOrRestartCoroutine(ref countdownCoroutine, CountdownCoroutine(duration));
    }

    private IEnumerator CountdownCoroutine(float time)
    {
        float remainingTime = time;

        while (remainingTime > Mathf.Epsilon)
        {
            // Update the countdown text
            UpdateTimerDisplay(remainingTime);
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
            // Decrease the remaining time
            remainingTime -= 1f;
        }
        UpdateTimerDisplay(remainingTime);
        Gamemanager.StopAllCoroutinesOnGameOver();
    }

    private void UpdateTimerDisplay(float time)
    {
        // Update the countdown text
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        countdownText.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }
}
