using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;  

public class SceneLoader : MonoBehaviour
{
    // References to TextMeshProUGUI elements for displaying scores
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve and display the high score from PlayerPrefs
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

        // Retrieve and display the current score from PlayerPrefs
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
    }

    // Method to load a new scene based on the provided scene name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  // Load the specified scene
    }
}
