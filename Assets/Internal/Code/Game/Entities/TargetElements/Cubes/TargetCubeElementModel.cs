using Settings;
using Signals;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public class TargetCubeElementModel : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly TargetCubeElementMono _targetCubeElementMono;
        private readonly MeshRenderer _meshRenderer;
        private readonly TMP_Text _scoreText;
        private readonly Rigidbody _rigidbody;

        private readonly float _repulsiveForceAtCollision;
        private int _quantityScoreByDestroy;
        private bool _isDie;

        public TargetCubeElementModel(
            SignalBus signalBus,
            GameSettings gameSettings,
            TargetCubeElementMono targetCubeElementMono,
            MeshRenderer meshRenderer,
            TMP_Text scoreText,
            Rigidbody rigidbody
            )
        {
            _signalBus = signalBus;
            _repulsiveForceAtCollision = gameSettings.RepulsiveForceAtCollision;
            _targetCubeElementMono = targetCubeElementMono;
            _meshRenderer = meshRenderer;
            _scoreText = scoreText;
            _rigidbody = rigidbody;

            targetCubeElementMono.OnDamage += OnDamage;
            targetCubeElementMono.OnChangeMaterial += ChangeMaterial;
            targetCubeElementMono.OnChangeQuantityScoreByDestroy += ChangeQuantityScoreBuDestroy;
            targetCubeElementMono.OnTargetDisable += () =>
            {
                _isDie = false;
                ChangeActivePhysics(false);
            };
        }

        public void Initialize() =>
            _signalBus.GetStream<StartGameSignal>().Subscribe(_ => ChangeActivePhysics(true)).AddTo(_targetCubeElementMono);
        
        private void ChangeQuantityScoreBuDestroy(int score)
        {
            _quantityScoreByDestroy = score;
            
            _scoreText.text = score.ToString();
        }

        private void ChangeMaterial(Material material) =>
            _meshRenderer.material = material;

        private void OnDamage(float damage)
        {
            if (!_isDie)
            {
                _rigidbody.AddForce(_repulsiveForceAtCollision * _targetCubeElementMono.transform.forward);
                _isDie = true;
            }
            
            _signalBus.Fire(new KillTargetElementSignal(){QuantityScoreOnDestroy = _quantityScoreByDestroy});
        }

        private void ChangeActivePhysics(bool isActive)
        {
            _rigidbody.isKinematic = !isActive;
            _rigidbody.useGravity = isActive;
        }
    }
}