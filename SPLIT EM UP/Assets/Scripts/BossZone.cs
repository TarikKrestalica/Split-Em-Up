using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handling all boss battle events here!
public class BossZone : MonoBehaviour
{
    [SerializeField] GameObject barriers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            barriers.SetActive(true);
        }
    }
}
