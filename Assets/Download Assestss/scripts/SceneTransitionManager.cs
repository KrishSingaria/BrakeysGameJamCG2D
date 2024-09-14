using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("PlayerX", spawnPositionInNextScene.x);
            PlayerPrefs.SetFloat("PlayerY", spawnPositionInNextScene.y);
            PlayerPrefs.SetFloat("PlayerZ", spawnPositionInNextScene.z);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
