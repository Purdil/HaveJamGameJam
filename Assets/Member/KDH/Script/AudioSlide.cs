using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlide : MonoBehaviour
{
    [SerializeField] private AudioType audiomixer;

    private Slider slider;

    private void Awake()
    {
        audiomixer = GetComponent<AudioType>();
    }

}
