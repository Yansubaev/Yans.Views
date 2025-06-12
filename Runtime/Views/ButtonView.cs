using UnityEngine;
using UnityEngine.UI;

namespace Yans.UI.Views
{
    public class ButtonView : View
    {
        [SerializeField] private Button _clickInterceptor;

        public bool Interactable
        {
            get => _clickInterceptor.interactable;
            set => _clickInterceptor.interactable = value;
        }

        public delegate void ClickOnViewDelegate(View view);

        private event ClickOnViewDelegate OnClickOnViewInternal;

        public event ClickOnViewDelegate OnClick
        {
            add
            {
                OnClickOnViewInternal += value;
                UpdateClickInterceptor();
            }
            remove
            {
                OnClickOnViewInternal -= value;
                UpdateClickInterceptor();
            }
        }

        public void PerformPointerClick()
        {
            OnClickOnViewInternal?.Invoke(this);
        }


        private void UpdateClickInterceptor()
        {
            if (_clickInterceptor != null)
                _clickInterceptor.enabled = OnClickOnViewInternal != null &&
                              OnClickOnViewInternal.GetInvocationList().Length > 0;
        }

        protected override void Awake()
        {
            base.Awake();

            if (_clickInterceptor != null)
                _clickInterceptor.enabled = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_clickInterceptor != null)
            {
                _clickInterceptor.onClick.AddListener(PerformPointerClick);
                _clickInterceptor.enabled = OnClickOnViewInternal?.GetInvocationList().Length > 0;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_clickInterceptor != null)
            {
                _clickInterceptor.onClick.RemoveListener(PerformPointerClick);
            }
        }
    }
}