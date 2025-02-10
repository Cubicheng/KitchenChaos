using UnityEngine;

public class MusicManager : MonoBehaviour {
    private AudioSource audioSource;

    public static MusicManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        SetMusicVolume();
    }

    public void SetMusicVolume() {
        audioSource.volume = PlayerPrefs.GetFloat(SoundManager.MUSIC_VOLUME);
    }
}
