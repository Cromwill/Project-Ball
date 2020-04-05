using UnityEngine;

public class AroundSound : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _soundOverlap;
    [SerializeField] private int _maxSoundClips;
    private AudioSource[] _currentBallKicksSound;

    private void Start()
    {
        _currentBallKicksSound = new AudioSource[_maxSoundClips];
    }

    public bool IsCanPlay(AudioSource audio)
    {

        for(int i = 0; i < _currentBallKicksSound.Length; i++)
        {
            if(_currentBallKicksSound[i] == null || !_currentBallKicksSound[i].isPlaying)
            {
                _currentBallKicksSound[i] = audio;
                return true;
            }
        }

        return false;
    }
}
