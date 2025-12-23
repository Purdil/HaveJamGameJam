using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioType audioType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        float value = PlayerPrefs.GetFloat(audioType.ToString(), 1f);

        slider.SetValueWithoutNotify(value);

        if (AudioSetting.Instance != null)
        {
            AudioSetting.Instance.SetVolume(audioType, value);
        }
    }
}
