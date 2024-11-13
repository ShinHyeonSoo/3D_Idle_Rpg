using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public void OnClickLoadSceneButton(int index)
    {
        switch(index)
        {
            case 0:
                SceneManager.LoadScene("Normal");
                break;
            case 1:
                SceneManager.LoadScene("PerlinNoise");
                break;
        }
    }
}
