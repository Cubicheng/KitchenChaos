using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const float MAX_VOLUME = 0.6f;

    public const string MUSIC_VOLUME = "MusicVolume";
    public const string SFX_VOLUME = "SFXVolume";

    public static SoundManager instance { get; private set; }
    private void Awake() {
        instance = this;
    }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private void Start() {
        DeliveryManager.instance.OnRecipeAccepted += DeliveryManager_OnRecipeAccepted;
        DeliveryManager.instance.OnRecipeRejected += DeliveryManager_OnRecipeRejected;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickup += Player_OnPickup;
        BaseCounter.OnDrop += BaseCounter_OnDrop;
        TrashCounter.OnTranshed += TrashCounter_OnTranshed;
    }

    public void PlayFootstepSound() {
        PlaySound(audioClipRefsSO.footstep);
    }

    private void TrashCounter_OnTranshed(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.trash);
    }

    private void BaseCounter_OnDrop(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectDrop);
    }

    private void Player_OnPickup(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.chop);
    }

    private void DeliveryManager_OnRecipeAccepted(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.deliveryAccepted);
    }

    private void DeliveryManager_OnRecipeRejected(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.deliveryRejected);
    }

    private void PlaySound(AudioClip[] audioClips, float volume = MAX_VOLUME) {
        Vector3 position = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume * PlayerPrefs.GetFloat(SFX_VOLUME));
    }

    private void PlaySound(AudioClip audioClip, float volume = MAX_VOLUME) {
        Vector3 position = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioClip, position, volume * PlayerPrefs.GetFloat(SFX_VOLUME));
    }
    
}
