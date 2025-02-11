using UnityEngine;

public class MusicLoopManager : MonoBehaviour {
    private AudioSource audioSource;

    [SerializeField] private float loopStartTime = 1.41f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        //if (audioSource.isPlaying) {
        //    return;
        //}
        //audioSource.Play();
        //audioSource.time = loopStartTime;
    }
}
