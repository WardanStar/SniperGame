using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tools.WTools
{
    public abstract class UIForm : MonoBehaviour
    {
        protected UIFormControlSystem CurrentUIFormControlSystem { get; set; }
        
        public Image[] Images
        {
            get
            {
                if (_images.Length == 0)
                    _images = GetComponentsInChildren<Image>();
                
                return _images;
            }
        }
        
        public TMP_Text[] Texts
        {
            get
            {
                if (_texts.Length == 0)
                    _texts = GetComponentsInChildren<TMP_Text>();
                
                return _texts;
            }
        }

        public Button[] Buttons
        {
            get
            {
                if (_buttons.Length == 0)
                    _buttons = GetComponentsInChildren<Button>();
                
                return _buttons;
            }
        }

        public Image[] ImagesException => _imagesException;
        public TMP_Text[] TextsException => _textsException;
        public Button[] ButtonException => _buttonException;

        public RectTransform RTransform => (RectTransform)transform;
        
        [Header("Selective appierance")]
        [SerializeField] private Image[] _images;
        [SerializeField] private TMP_Text[] _texts;
        [SerializeField] private Button[] _buttons;
        
        [Header("Exceptions")]
        [SerializeField] private Image[] _imagesException;
        [SerializeField] private TMP_Text[] _textsException;
        [SerializeField] private Button[] _buttonException;
        private float _animationDuration;

        [Inject]
        public void Construct(
            UIFormControlSystem uiFormControlSystem,
            UIStorage uiStorage
            )
        {
            CurrentUIFormControlSystem = uiFormControlSystem;
            uiStorage.AddForm(GetType(), this);
            gameObject.SetActive(false);
        }


        public virtual void ActionBeforeShow()
        {
        }
        
        public virtual void ActionAfterShow()
        {
        }
        
        public virtual void ActionBeforeHide()
        {
        }
        
        public virtual void ActionAfterHide()
        {
        }

        protected void Hide<T>(bool isAnimation) where T : UIForm
        {
            CurrentUIFormControlSystem.HideForm<T>(isAnimation, _animationDuration).Forget();
        }

        public void SetAnimationDuration(float animationDuration)
        {
            _animationDuration = animationDuration;
        }
    }
}