using TMPro;
using Tools.WTools;
using UnityEngine;

namespace UI.Forms
{
    public class PreShotReportForm : UIForm
    {
        [SerializeField] private TMP_Text _preShotReportText;

        public void SetText(string text) =>
            _preShotReportText.text = text;
    }
}