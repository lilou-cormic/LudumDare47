using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace PurpleCable
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        private Button Button;

        [SerializeField]
        private AudioClip ClickSound = null;

        [SerializeField]
        private AudioClip SelectedSound = null;

#if UNITY_EDITOR
        [SerializeField]
        private TextMeshProUGUI LabelText = null;

        [SerializeField]
        private string Label = string.Empty;

        private void OnValidate()
        {
            if (LabelText == null)
                return;

            if (string.IsNullOrEmpty(Label) || Label == "Button")
                Label = LabelText.text;
            else if (!string.IsNullOrEmpty(Label))
                LabelText.text = Label;
        }
#endif

        private void Awake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(() => PlayClickSound());
        }

        public void PlayClickSound()
        {
            ClickSound.Play();
        }

        public void PlaySelectedSound()
        {
            SelectedSound.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
            PlaySelectedSound();
        }
    }
}
