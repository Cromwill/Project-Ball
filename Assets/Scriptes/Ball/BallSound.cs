using UnityEngine;

public class BallSound : MonoBehaviour
{
    public AroundSound _aroundSound;
    [SerializeField] private AudioClip[] _audioClips;

    private AudioSource _selfAudioSourse;

    private void Start()
    {
        _selfAudioSourse = GetComponent<AudioSource>();
        _aroundSound = FindObjectOfType<AroundSound>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_selfAudioSourse.isPlaying)
        {
            int index = Random.Range(0, _audioClips.Length);
            if (_aroundSound.IsCanPlay(_selfAudioSourse))
            {
                _selfAudioSourse.PlayOneShot(_audioClips[index]);
            }
        }
    }
}
