using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public void PlayGame()
    {
        print("hit");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
