using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // Total time for the countdown in seconds (5 minutes)
    public float timeRemaining = 300f;

    // Flag to control if the timer is active
    public bool timerIsRunning = false;

    // Reference to the TextMeshPro component to display the time
    public TextMeshProUGUI timeText;

    // Name of the scene to load when the timer reaches zero
    public string gameOverSceneName = "GameOver";

    // Start is called before the first frame update
    private void Start()
    {
        // Start the timer when the game begins
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer is running
        if (timerIsRunning)
        {
            // If there is time remaining, decrement the timer
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                // Update the displayed time
                DisplayTime(timeRemaining);
            }
            else
            {
                // When time runs out, set time to zero and stop the timer
                timeRemaining = 0;
                timerIsRunning = false;

                // Trigger the Game Over sequence
                GameOver();
            }
        }
    }

    // Method to format and display the remaining time on the screen
    void DisplayTime(float timeToDisplay)
    {
        // Add one second for display formatting
        timeToDisplay += 1;

        // Calculate minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Display the time in "MM:SS" format
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to handle the Game Over state by loading the Game Over scene
    void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverSceneName);
    }
}
