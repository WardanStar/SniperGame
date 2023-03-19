using System;
using System.Collections.Generic;
using Additional;
using Game.Entities;
using Game.Systems;
using ProjectSystems;
using TMPro;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Forms
{
    public class GameForm : UIForm
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private RectTransform _ammunitionRoot;

        private readonly List<UIAmmunitionIconMono> _ammunitionIcons = new();
        private IArm _arm;


        [Inject]
        public void Construct(
            IArm arm,
            ScoreCounter scoreCounter,
            WeaponInfo weaponInfo
        )
        {
            _arm = arm;
            scoreCounter.Score.Subscribe(value => _scoreText.text = $"Score : {Math.Round(value, 1)}").AddTo(this);
            weaponInfo.Ammunition.Subscribe(ChangeAmmunition).AddTo(this);
        }

        private void ChangeAmmunition(int value)
        {
            if (value < _ammunitionIcons.Count)
            {
                UIAmmunitionIconMono icon = GetAmmunitionIcon();

                if (ReferenceEquals(icon, null))
                    return;
                
                icon.ReturnToPool();
                _ammunitionIcons.Remove(icon);
                return;
            }

            for (int i = _ammunitionIcons.Count; i < value; i++)
            {
                UIAmmunitionIconMono icon = _arm.UIPoolObjectGetter.GetComponentFromUIPoolObject<UIAmmunitionIconMono>(
                    ConstantKeys.UI_COLLECTION_ID, ConstantKeys.AMMUNITION_ICON_ID, _ammunitionRoot);
                
                _ammunitionIcons.Add(icon);
            }
        }

        private UIAmmunitionIconMono GetAmmunitionIcon()
        {
            foreach (UIAmmunitionIconMono ammunitionIcon in _ammunitionIcons)
            {
                if (ammunitionIcon.IsUsed)
                    return ammunitionIcon;
            }

            return null;
        }
    }
}