using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void onClick(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
