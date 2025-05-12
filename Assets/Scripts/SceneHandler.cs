using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public string scenename;
    public void changescene()
    {
        // Load the new scene
        SceneManager.LoadScene(scenename);
    }
}
