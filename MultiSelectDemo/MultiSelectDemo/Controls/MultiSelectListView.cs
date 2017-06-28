using Xamarin.Forms;

namespace MultiSelectDemo.Controls
{
	public static class MultiSelectListView
	{
		public static readonly BindableProperty IsMultiSelectProperty =
			BindableProperty.CreateAttached("IsMultiSelect", typeof(bool), typeof(ListView), false, propertyChanged: OnIsMultiSelectChanged);

		public static bool GetIsMultiSelect(BindableObject view) => (bool)view.GetValue(IsMultiSelectProperty);

		public static void SetIsMultiSelect(BindableObject view, bool value) => view.SetValue(IsMultiSelectProperty, value);

		private static void OnIsMultiSelectChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var listView = bindable as ListView;
			if (listView != null)
			{
				// always remove event
				listView.ItemSelected -= OnItemSelected;

				// add the event if true
				if (true.Equals(newValue))
				{
					listView.ItemSelected += OnItemSelected;
				}
			}
		}

		private static void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as SelectableItem;
			if (item != null)
			{
				// toggle the selection property
				item.IsSelected = !item.IsSelected;
			}

			// deselect the item
			((ListView)sender).SelectedItem = null;
		}
	}
}
