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
        string key = audioType.ToString();

        if (PlayerPrefs.HasKey(key))
        {
            float save = PlayerPrefs.GetFloat(key, 1f);
            slider.SetValueWithoutNotify(save);
        }
    }
}
