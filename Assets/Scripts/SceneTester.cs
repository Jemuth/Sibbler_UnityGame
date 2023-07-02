using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    public string sceneName;
    private void Update()
    {
       
        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
