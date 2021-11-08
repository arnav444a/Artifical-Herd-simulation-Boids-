using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObstacleActive : MonoBehaviour
{
    public Animator fadeAni;
    public GameObject[] obstacleObjs;
    int k = 0;
    public void ButtonPress()
    {
        Debug.Log("Working!");
        k++;
        if (k >= obstacleObjs.Length)
        {
            k = 0;
        }
        for (int i = 0; i < obstacleObjs.Length; i++)
        {
            obstacleObjs[k].SetActive(true);
            if (obstacleObjs[i] != obstacleObjs[k])
            {
                obstacleObjs[i].SetActive(false);
            }
        }
    }
    public void Reset()
    {
        fadeAni.SetTrigger("FadeIn");
        AsyncOperation sceneActivator = SceneManager.LoadSceneAsync(0);
        sceneActivator.allowSceneActivation = false;
        StartCoroutine(ActivateNewScene(sceneActivator));
    }
    private IEnumerator ActivateNewScene(AsyncOperation sceneActivation)
    {
        yield return new WaitForSeconds(0.6f);
        sceneActivation.allowSceneActivation = true;
    }
}
