using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatCameraManager : MonoBehaviour
{
    private RawImage m_rawImage;

    [SerializeField] List<Texture> currentTextures;
    [SerializeField] List<Texture> defaulttextures;
    [SerializeField] List<Texture> hitByEnemytextures;
    [SerializeField] List<Texture> hitEnemytextures;

    [Range(0, 4f)]
    [SerializeField] float timeLapse;

    // Start is called before the first frame update
    void Awake()
    {
        m_rawImage = GetComponent<RawImage>();
        if (!m_rawImage)
        {
            Debug.LogError("Need image component to function properly!");
            return;
        }
    }

    IEnumerator Play()
    {
        WaitForSeconds wait = new WaitForSeconds(timeLapse);
        while (true)
        {
            for (int i = 0; i < currentTextures.Count; i++)
            {
                m_rawImage.texture = currentTextures[i];
                yield return wait;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTextures = defaulttextures;
        StartCoroutine(Play());
    }

    public void PlayHitEnemyTextures()
    {
        StopAllCoroutines();
        currentTextures = hitEnemytextures;
        GameManager.player.SetPlayerState(PlayerState.HitEnemy);
        StartCoroutine(Play());
    }

    public void PlayHitByEnemyTextures()
    {
        StopAllCoroutines();
        currentTextures = hitByEnemytextures;
        GameManager.player.SetPlayerState(PlayerState.HitByEnemy);
        StartCoroutine(Play());
    }

    public void PlayDefaultTextures()
    {
        StopAllCoroutines();
        currentTextures = defaulttextures;
        GameManager.player.SetPlayerState(PlayerState.Default);
        StartCoroutine(Play());
    }
}
