using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevels : MonoBehaviour
{
    private int indexNextLevel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indexNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(indexNextLevel);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indexNextLevel = SceneManager.GetActiveScene().buildIndex - 1;
            SceneManager.LoadScene(indexNextLevel);
        }
    }
}