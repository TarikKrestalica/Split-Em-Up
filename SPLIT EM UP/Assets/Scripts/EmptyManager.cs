using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyManager : MonoBehaviour
{
    [Range(-20f, 20f)]
    [SerializeField] private float xBound;

    [Range(0, 100f)]
    [SerializeField] private float panBackSpeed;

    // Update is called once per frame
    void Update()
    {
        // Is player not in the empty range?
        if (GameManager.player.transform.position.x <= xBound)
        {
            return;
        }

        MoveBackToPlayer();
    }

    // Help with camera smoothing: https://youtu.be/MFQhpwc6cKE?si=bDK9HzU-zuIGL2r6
    public void MoveBackToPlayer()
    {
        Vector3 newPosition = new Vector3(GameManager.player.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 target = Vector3.Lerp(this.transform.position, newPosition, panBackSpeed * Time.deltaTime);
        this.transform.position = target;

    }
}
