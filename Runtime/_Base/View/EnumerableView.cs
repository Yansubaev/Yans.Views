using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Yans.UI.Views
{
    public class EnumerableView<V> : View, IEnumerable<V> where V : View
    {
        #region private fields

        [SerializeField]
        private Transform _viewsContainer;

        [SerializeField]
        private V _viewPrefab;

        private IViewInstantiator<V> _viewInstantiator;
        private List<V> _activeViewsCache;
        private ObjectPool<V> _viewPool;
        #endregion

        #region public properties

        public Transform ViewsContainer
        {
            get => _viewsContainer;
            set => _viewsContainer = value;
        }

        public V this[int index]
        {
            get
            {
                if (_activeViewsCache == null) Awake();
                while (index >= _activeViewsCache.Count)
                {
                    CreateView();
                }
                V view = _activeViewsCache[index];
                view.transform.SetSiblingIndex(index);
                return view;
            }
        }

        public int Count
        {
            get
            {
                if (_activeViewsCache == null) Awake();
                return _activeViewsCache.Count;
            }
        }

        #endregion

        public event Action<V> OnViewCreated;
        public event Action<V> OnViewDestroyed;

        #region public methods

        public void ReleaseView(V view)
        {
            if (_viewPool == null) Awake();
            if (_activeViewsCache.Remove(view))
            {
                _viewPool.Release(view);
            }
        }

        public void ReleaseAllViews()
        {
            if (_activeViewsCache == null) Awake();
            for (int i = _activeViewsCache.Count - 1; i >= 0; i--)
            {
                ReleaseView(_activeViewsCache[i]);
            }
        }

        public void SetViewInstantiator(IViewInstantiator<V> viewInstantiator)
        {
            _viewInstantiator = viewInstantiator;
        }

        #endregion

        #region protected methods

        protected override void Awake()
        {
            base.Awake();

            _viewInstantiator = new DefaultViewInstantiator();
            _activeViewsCache = _viewsContainer.GetComponentsInChildren<V>(false).ToList();
            _viewPool = new ObjectPool<V>(
                CreateNewViewInternal,
                OnGetFromPool,
                OnReleaseToPool,
                OnViewDestroyedInternal);
        }

        protected virtual V CreateNewView()
        {
            return _viewInstantiator.InstantiateView(_viewPrefab, _viewsContainer);
        }

        protected virtual void OnDestroyView(V view)
        {
        }

        #endregion

        #region private methods

        private V CreateView()
        {
            if (_viewPool == null) Awake();

            V view = _viewPool.Get();
            _activeViewsCache.Add(view);
            view.transform.SetSiblingIndex(_activeViewsCache.Count - 1);
            return view;
        }

        private V CreateNewViewInternal()
        {
            V view = CreateNewView();
            OnViewCreated?.Invoke(view);
            return view;
        }

        private void OnViewDestroyedInternal(V view)
        {
            OnViewDestroyed?.Invoke(view);
            OnDestroyView(view);
        }

        private void OnGetFromPool(V view)
        {
            view.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(V view)
        {
            view.gameObject.SetActive(false);
        }

        #endregion

        public interface IViewInstantiator<T>
        {
            T InstantiateView(V prefab, Transform parent);
        }

        private class DefaultViewInstantiator : IViewInstantiator<V>
        {
            #region public methods

            public V InstantiateView(V prefab, Transform parent)
            {
                return Instantiate(prefab, parent);
            }

            #endregion
        }

        #region interfaces

        public IEnumerator<V> GetEnumerator()
        {
            return _activeViewsCache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}