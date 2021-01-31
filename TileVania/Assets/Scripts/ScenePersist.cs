using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int sceneIndex;
    private void Awake()
    {
        int numberOfScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numberOfScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (sceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            Destroy(gameObject);
        }
    }
}
