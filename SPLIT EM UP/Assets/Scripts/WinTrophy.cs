using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrophy : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
          void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Collided");
                SceneManager.LoadScene("WinScreen");
            }
        }

}

    // Update is called once per frame
}
