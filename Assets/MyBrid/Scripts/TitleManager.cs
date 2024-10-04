using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string playScene = "PlayScene";

    void Update()
    {
        ResetData();
    }

    private void ResetData()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

}
