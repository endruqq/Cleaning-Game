using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantSceneManagment : MonoBehaviour
{

    private static PersistantSceneManagment instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
