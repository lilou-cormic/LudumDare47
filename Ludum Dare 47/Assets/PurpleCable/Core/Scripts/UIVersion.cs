using TMPro;
using UnityEngine;

namespace PurpleCable
{
    public class UIVersion : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI VersionText = null;

        private void Awake()
        {
            VersionText.text = Application.version;
        }
    }
}
