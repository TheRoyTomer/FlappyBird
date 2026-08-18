using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    
    [SerializeField] private AudioClip wingSound;
    [SerializeField] private AudioClip pointSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip dieSound;
    
    
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        audioSource = GetComponent<AudioSource>();

    }
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    public void PlayWing()
    {
        PlaySound(wingSound);
    }

    public void PlayPoint()
    {
        PlaySound(pointSound);
    }

    public void PlayHit()
    {
        PlaySound(hitSound);
    }

    public void PlayDie()
    {
        PlaySound(dieSound);
    }
    
    public void PlayDeathSequence()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        PlayHit();

        yield return new WaitForSeconds(1f);

        PlayDie();
    }
}