using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyManager : MonoBehaviour
{
    [Range(-20f, 20f)]
    [SerializeField] private float xBound;

    // Update is called once per frame
    void Update()
    {
        // Is player not in the empty range?
        if (GameManager.player.transform.position.x <= xBound)
        {
            return;
        }

        Vector3 newPosition = new Vector3(GameManager.player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
        
    }
}
