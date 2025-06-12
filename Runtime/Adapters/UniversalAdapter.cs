using System;
using Yans.UI.Views;

namespace Yans.UI.Adapters
{
    public class UniversalAdapter<V> : ListAdapter<V> where V : View
    {
        private Action<V, int> _onBindView;

        public UniversalAdapter(EnumerableView<V> enumerableView) : base(enumerableView)
        {
        }

        public void ApplyDataset(int count, Action<V, int> onBindView)
        {
            _onBindView = onBindView;
            NotifyDatasetChanged(count);
        }

        protected override void OnBindViewHolder(V view, int position)
        {
            _onBindView?.Invoke(view, position);
        }
    }
}