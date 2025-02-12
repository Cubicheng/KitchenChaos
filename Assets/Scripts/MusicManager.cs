using UnityEngine;

public class MusicManager : MonoBehaviour {
    private AudioSource audioSource;

    public static MusicManager Instance { get; private set; }

    private void Awake() {

        if (!PlayerPrefs.HasKey(SoundManager.MUSIC_VOLUME)) {
            PlayerPrefs.SetFloat(SoundManager.MUSIC_VOLUME, 0.5f);
        }

        if (!PlayerPrefs.HasKey(SoundManager.SFX_VOLUME)) {
            PlayerPrefs.SetFloat(SoundManager.SFX_VOLUME, 0.5f);
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();
        SetMusicVolume();
    }

    public void SetMusicVolume() {
        audioSource.volume = PlayerPrefs.GetFloat(SoundManager.MUSIC_VOLUME);
    }
}
