using UnityEngine;

namespace com
{
    public class PlaySoundBehaviour : MonoBehaviour
    {
        public float vol = 1;
        public string sound;
        public bool playOnEnable = true;
        public bool useAlternativeSound = false;
        public string[] alternativeSound;

        private void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }

        public void Play()
        {
            if (useAlternativeSound)
            {
                PlayAlternativeSound();
                return;
            }

            PlayMonoSound();
        }

        public void PlayMonoSound()
        {
            SoundSystem.instance.Play(sound, vol);
        }

        public void PlayAlternativeSound()
        {
            if (alternativeSound.Length < 1)
            {
                PlayMonoSound();
            }

            int index = Random.Range(0, alternativeSound.Length);
            SoundSystem.instance.Play(alternativeSound[index], vol);
        }
    }
}