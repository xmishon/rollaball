using UnityEngine;

namespace mzmeevskiy
{
    public class SoundController
    {
        [SerializeField] private AudioSource _audioSource;


        public SoundController(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlayBonusPickupSound(int _bonusCount)
        {
            _audioSource.Play();
        }
    }
}