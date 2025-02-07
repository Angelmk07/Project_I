using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputs : MonoBehaviour
{
    [SerializeField] private KeyCode buttonInfo = KeyCode.I;
    [SerializeField] private KeyCode buttonReturn = KeyCode.Backspace;
    [SerializeField] private GameObject inputInfo;
    private void Update()
    {
        if (Input.GetKeyDown(buttonInfo))
        {
            inputInfo.SetActive(!inputInfo.activeSelf);
        }
        if (Input.GetKeyDown(buttonReturn))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
