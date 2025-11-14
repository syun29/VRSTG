using System.Collections;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    private AudioSource m_source;

    private void Awake()
    {
        m_source = GetComponent<AudioSource>();
    }

    private IEnumerator Co_Destroy()
    {
        yield return new WaitWhile(() => !m_source.isPlaying);
        yield return new WaitWhile(() => m_source.isPlaying);
        Destroy(gameObject);
    }
}
