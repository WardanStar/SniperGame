using UnityEngine;
using UnityEngine.UI;

namespace Tools.WTools
{
    public class GridLayoutCorrector : MonoBehaviour
    {
        enum TypeGridLayout
        {
            Vertical,
            Horizontal
        }

        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private TypeGridLayout _typeGridLayout = TypeGridLayout.Vertical;
        [SerializeField] private float _startSize = 100f, 
                                       _stepSize = 5f;

        private Vector2 _sizeTransform;
        private float _commonSize;
        private int _commonQuantityLines = 1,
                    _commonQuantityElement;

        private bool _isInitialize;

        private void Initialize()
        {
            _sizeTransform = ((RectTransform)transform).sizeDelta;
            UpdateData();
        }

        public void ChangeSize(int quantityElements)
        {
            if (!_isInitialize)
                Initialize();
            
            if (quantityElements < _commonQuantityElement)
                UpdateData();
            
            var size = _commonSize;
            
            while (true)
            {
                if (CheckElementCapacities(quantityElements, _commonQuantityLines, size) || size - _stepSize <= 0)
                {
                    _commonSize = size;
                    _gridLayout.cellSize = _commonSize * Vector2.one;
                    _gridLayout.constraintCount = _commonQuantityLines;
                    _commonQuantityElement = quantityElements;
                    return;
                }
                
                var quantityLines = CheckQuantityLine(size);
                
                if (_commonQuantityLines >= quantityLines || CheckLineCapacities(size, quantityLines) is false)
                {
                    size -= _stepSize;
                    continue;
                }

                _commonQuantityLines = quantityLines;
            }
        }

        private int CheckQuantityLine(float size) =>
            (int)(((_typeGridLayout == TypeGridLayout.Horizontal) ? _sizeTransform.y : _sizeTransform.x) / size);
        
        private bool CheckElementCapacities(int quantityElements, int quantityLines, float size)
        {
            return quantityElements * size / quantityLines <=
                   ((_typeGridLayout == TypeGridLayout.Horizontal) ? _sizeTransform.x : _sizeTransform.y);
        }

        private bool CheckLineCapacities(float size, int quantityLines) =>
            quantityLines * size <
            ((_typeGridLayout == TypeGridLayout.Horizontal) ? _sizeTransform.y : _sizeTransform.x);


        private void UpdateData()
        {
            _commonQuantityElement = 0;
            _commonSize = _startSize;
            _commonQuantityLines = 1;
        }
    }
}