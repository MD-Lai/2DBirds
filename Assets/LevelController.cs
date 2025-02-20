﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Enemy[] _enemies;
    private static int _nextLevelIndex = 1;

    private void OnEnable() {
        _enemies = FindObjectsOfType<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in _enemies){ 
            if (enemy != null) {
                return;
            }
        }

        Debug.Log("Enemies are gone!");

        _nextLevelIndex += 1;

        string nextLevelID = "Level" + _nextLevelIndex;

        SceneManager.LoadScene(nextLevelID);

    }
}
