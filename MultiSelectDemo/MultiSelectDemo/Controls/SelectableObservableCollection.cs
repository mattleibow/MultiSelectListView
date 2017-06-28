using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiSelectDemo.Controls
{
	public class SelectableObservableCollection<T> : ObservableCollection<SelectableItem<T>>
	{
		public SelectableObservableCollection()
			: base()
		{
		}

		public SelectableObservableCollection(IEnumerable<T> collection)
			: base(collection.Select(c => new SelectableItem<T>(c)))
		{
		}

		public SelectableObservableCollection(IEnumerable<SelectableItem<T>> collection)
			: base(collection)
		{
		}

		public IEnumerable<T> SelectedItems => Items.Where(i => i.IsSelected).Select(i => i.Data);

		public IEnumerable<T> UnselectedItems => Items.Where(i => !i.IsSelected).Select(i => i.Data);

		public void Add(T item)
		{
			Add(new SelectableItem<T>(item));
		}

		public void Insert(int index, T item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			Insert(index, new SelectableItem<T>(item));
		}

		public bool Contains(T item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			return IndexOf(item) != -1;
		}

		public int IndexOf(T item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			for (var i = 0; i < Count; i++)
			{
				if (item.Equals(Items[i].Data))
				{
					return i;
				}
			}
			return -1;
		}

		public bool Remove(T item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			var i = IndexOf(item);
			if (i != -1)
			{
				RemoveAt(i);
				return true;
			}
			return false;
		}
	}
}
