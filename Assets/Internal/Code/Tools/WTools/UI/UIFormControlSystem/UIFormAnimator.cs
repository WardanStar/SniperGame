using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.WTools
{
    public class UIFormAnimator
    {
        public void ChangeAlphaAnimation(UIForm uiForm, float time, bool emergence)
        {
            foreach (Image image in uiForm.Images)
            {
                if (CheckToImageException(uiForm, image))
                    continue;
                
                Color oldColor = image.color;

                image.color = new Color(oldColor.r, oldColor.g, oldColor.b, emergence ? 0 : 1);
                
                image.DOColor(new Color(oldColor.r, oldColor.g, oldColor.b, emergence ? 1 : 0), time);
            }

            foreach (TMP_Text text in uiForm.Texts)
            {
                if (CheckToTextException(uiForm, text))
                    continue;
                
                Color oldColor = text.color;

                text.color = new Color(oldColor.r, oldColor.g, oldColor.b, emergence ? 0 : 1);
                
                text.DOColor(new Color(oldColor.r, oldColor.g, oldColor.b, emergence ? 1 : 0), time);
            }
        }

        private bool CheckToImageException(UIForm uiForm, Image image)
        {
            foreach (var imagesException in uiForm.ImagesException)
            {
                if (image == imagesException)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool CheckToTextException(UIForm uiForm, TMP_Text text)
        {
            foreach (var textException in uiForm.TextsException)
            {
                if (text == textException)
                {
                    return true;
                }
            }

            return false;
        }
    }
}