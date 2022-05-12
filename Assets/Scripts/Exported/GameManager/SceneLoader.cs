using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadArea(int areaNumberToLoad)
    {
        SceneManager.LoadScene(areaNumberToLoad);
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene(0);
    }
}
