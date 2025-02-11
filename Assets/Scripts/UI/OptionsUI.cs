using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI instance { get; private set; }

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button backButton;

    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button CutButton;
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadCutButton;

    [SerializeField] private TextMeshProUGUI MoveUpButtonText;
    [SerializeField] private TextMeshProUGUI MoveDownButtonText;
    [SerializeField] private TextMeshProUGUI MoveLeftButtonText;
    [SerializeField] private TextMeshProUGUI MoveRightButtonText;
    [SerializeField] private TextMeshProUGUI InteractButtonText;
    [SerializeField] private TextMeshProUGUI CutButtonText;
    [SerializeField] private TextMeshProUGUI GamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI GamepadCutButtonText;

    [SerializeField] private Transform pressToRebind;

    private string MUSIC_VOLUME;
    private string SFX_VOLUME;

    private void Awake() {
        instance = this;
        MUSIC_VOLUME = SoundManager.MUSIC_VOLUME;
        SFX_VOLUME = SoundManager.SFX_VOLUME;

        backButton.onClick.AddListener(() => {
            Hide();
            GamePauseUI.instance.Show();
        });

        MoveUpButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.MoveUp);
        });

        MoveDownButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.MoveDown);
        });

        MoveLeftButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.MoveLeft);
        });

        MoveRightButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.MoveRight);
        });

        InteractButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.Interact);
        });

        CutButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.Cut);
        });

        GamepadInteractButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.GamePadInteract);
        });

        GamepadCutButton.onClick.AddListener(() => {
            Rebind(GameInput.Binding.GamePadCut);
        });
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e) {
        Hide();
    }

    private void UPdateVisual() {
        MoveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        MoveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        MoveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        MoveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        InteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        CutButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Cut);
        GamepadCutButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadCut);
        GamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteract);
    }

    private void Start() {
        Hide();
        HidePressToRebind();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME);
        UPdateVisual();
    }

    private void Update() {
        if (musicSlider.value != PlayerPrefs.GetFloat(MUSIC_VOLUME)) {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, musicSlider.value);
            MusicManager.Instance.SetMusicVolume();
        }
        if (sfxSlider.value != PlayerPrefs.GetFloat(SFX_VOLUME)) {
            PlayerPrefs.SetFloat(SFX_VOLUME, sfxSlider.value);
        }
    }

    private void Rebind(GameInput.Binding binding) {
        ShowPressToRebind();
        GameInput.Instance.Rebinding(binding, ()=> {
            HidePressToRebind();
            UPdateVisual();
        });
    }

    public void Show() {
        if (Gamepad.current != null) {
            musicSlider.Select();
        }
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebind() {
        pressToRebind.gameObject.SetActive(true);
    }
    private void HidePressToRebind() {
        pressToRebind.gameObject.SetActive(false);
    }

}
