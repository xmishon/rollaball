using UnityEngine;

namespace mzmeevskiy
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlayBonusPickupSound(int _bonusCount)
        {
            _audioSource.Play();
        }
    }
}