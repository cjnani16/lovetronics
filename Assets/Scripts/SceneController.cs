﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public string scenePath;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
    }
}
