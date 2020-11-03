using UnityEngine;

namespace mzmeevskiy
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlayBonusPickupSound()
        {
            _audioSource.Play();
        }
    }
}