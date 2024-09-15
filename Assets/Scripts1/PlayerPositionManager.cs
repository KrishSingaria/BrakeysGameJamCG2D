using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public GameObject player; 

    void Start()
    {

        if (PlayerPrefs.HasKey("PlayerX"))
        {

            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");


            player.transform.position = new Vector3(x, y, z);
        }
    }
}
