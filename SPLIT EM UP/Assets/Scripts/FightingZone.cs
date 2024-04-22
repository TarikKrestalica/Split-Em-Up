using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FightingZone : MonoBehaviour
{
    [SerializeField] GameObject barriers;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.player.SetAtFightingZone(true);
            barriers.SetActive(true);
        }
    }

    public void SetBarriersActive(bool toggle)
    {
        barriers.SetActive(toggle);
    }
}