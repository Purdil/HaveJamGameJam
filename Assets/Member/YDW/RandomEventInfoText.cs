using System.Collections;
using Member.YDW.EventChannels;
using TMPro;
using UnityEngine;

namespace Member.YDW
{
    public class RandomEventInfoText : MonoBehaviour
    {
        [SerializeField] private RandomEventInfoEvent _eventInfoEvent;

        [SerializeField] private TextMeshProUGUI _description;
        private void Awake()
        {
            gameObject.SetActive(false);
            _eventInfoEvent.OnEvent += HandleOnInfo;
        }

        private void HandleOnInfo(string obj)
        {
            _description.text = obj;
            gameObject.SetActive(true);
            StartCoroutine(Delete());
        }

        private IEnumerator Delete()
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _eventInfoEvent.OnEvent -= HandleOnInfo;
        }
    }
}