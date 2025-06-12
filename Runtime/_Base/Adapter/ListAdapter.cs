using System;
using Yans.UI.Views;

namespace Yans.UI.Adapters
{
    
    public abstract class ListAdapter<V> : IDisposable where V : View
    {
        #region private fields
        private readonly EnumerableView<V> _enumerableView;
        private int _count;
        #endregion

        #region protected properties
        protected EnumerableView<V> EnumerableView => _enumerableView;
        protected int Count => _count;
        #endregion

        #region public methods

        public ListAdapter(EnumerableView<V> enumerableView)
        {
            _enumerableView = enumerableView;

            enumerableView.OnViewCreated += OnViewHolderCreated;
            enumerableView.OnViewDestroyed += OnViewHolderDestroyed;
        }

        #endregion

        #region protected methods

        protected void NotifyDatasetChanged(int count)
        {
            _count = count;
            DistributeData();
        }

        protected virtual void OnViewHolderCreated(V view)
        {
        }

        protected virtual void OnBeforeBinding()
        {
        }

        protected abstract void OnBindViewHolder(V view, int position);

        protected virtual void OnAfterBinding()
        {
        }

        protected virtual void OnViewHolderDestroyed(V v)
        {
        }

        #endregion

        #region private methods

        private void DistributeData()
        {
            OnBeforeBinding();
            if (_count > 0)
            {
                for (int i = 0; i < _count; i++)
                {
                    OnBindViewHolder(_enumerableView[i], i);
                }
            }
            OnAfterBinding();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_enumerableView != null)
            {
                _enumerableView.OnViewCreated -= OnViewHolderCreated;
                _enumerableView.OnViewDestroyed -= OnViewHolderDestroyed;
            }
        }

        #endregion
    }
}