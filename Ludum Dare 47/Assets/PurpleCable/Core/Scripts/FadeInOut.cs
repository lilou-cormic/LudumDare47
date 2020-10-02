using UnityEngine;

namespace PurpleCable
{
    public class FadeInOut : MonoBehaviour
    {
        #region Instance

        public static FadeInOut _instance;

        #endregion

        #region Editor

        [SerializeField] CanvasGroup CanvasGroup = null;

        [SerializeField] GameObject Panel = null;

        [SerializeField] float FadeInTime = 0.2f;

        [SerializeField] float FadeOutTime = 0.2f;

        [SerializeField] bool IsScale = false;

        #endregion

        public static bool IsFading => _instance._isFadingIn || _instance._isFadingOut;

        private float _timeElapsed = 0f;

        private bool _isFadingIn = false;

        private bool _isFadingOut = false;

        #region Unity callbacks

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_isFadingIn)
            {
                if (_timeElapsed >= FadeInTime)
                {
                    _timeElapsed = 0f;
                    _isFadingIn = false;
                    gameObject.SetActive(false);
                    Cursor.visible = true;
                    return;
                }

                _timeElapsed += Time.deltaTime;

                if (IsScale)
                    Panel.transform.localScale = Vector3.Lerp(Vector3.one * 2, Vector3.zero, _timeElapsed / FadeInTime);
                else
                    CanvasGroup.alpha = Mathf.Lerp(1f, 0f, _timeElapsed / FadeInTime);
            }

            if (_isFadingOut)
            {
                if (_timeElapsed >= FadeOutTime)
                {
                    _timeElapsed = 0f;
                    _isFadingOut = false;
                    return;
                }

                _timeElapsed += Time.deltaTime;

                if (IsScale)
                    Panel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 2, _timeElapsed / FadeOutTime);
                else
                    CanvasGroup.alpha = _timeElapsed / FadeOutTime;
            }
        }

        #endregion

        #region Methods

        public static void FadeIn()
            => _instance.FadeInInternal();

        private void FadeInInternal()
        {
            Cursor.visible = false;
            _timeElapsed = 0f;
            _isFadingIn = true;
            gameObject.SetActive(true);

            if (IsScale)
                Panel.transform.localScale = Vector3.one * 2;
            else
                CanvasGroup.alpha = 1f;
        }

        public static void FadeOut()
            => _instance.FadeOutInternal();

        private void FadeOutInternal()
        {
            Cursor.visible = false;
            _timeElapsed = 0f;
            _isFadingOut = true;
            gameObject.SetActive(true);

            if (IsScale)
                Panel.transform.localScale = Vector3.zero;
            else
                CanvasGroup.alpha = 0f;
        }

        #endregion
    }
}
