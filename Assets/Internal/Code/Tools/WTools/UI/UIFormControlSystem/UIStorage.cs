using System;
using System.Collections.Generic;

namespace Tools.WTools
{
    public class UIStorage
    {
        private Dictionary<Type, object> _forms = new();

        public void AddForm<T>(Type type, T uiForm) where T : UIForm
        {
            if (_forms.TryGetValue(type, out _))
                throw new ArgumentNullException();
            
            _forms.Add(type, uiForm);
        }

        public T GetForm<T>() where T : UIForm
        {
            if (_forms.TryGetValue(typeof(T), out object obj))
                return (T)obj;

            throw new NullReferenceException();
        }
    }
}