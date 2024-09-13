using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{ 
   public void play()
   {
        SceneManager.LoadScene("Main");
   }

    public void option()
    {
        //option
    }

    public void exit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }




}
