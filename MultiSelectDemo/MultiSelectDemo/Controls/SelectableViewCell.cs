using Xamarin.Forms;

namespace MultiSelectDemo.Controls
{
	[ContentProperty(nameof(DataView))]
	public class SelectableViewCell : ViewCell
	{
		private Grid rootGrid;
		private View dataView;
		private View checkView;

		public SelectableViewCell()
		{
			rootGrid = new Grid
			{
				Padding = 12,
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Auto }
				}
			};

			View = rootGrid;

			var check = new BoxView
			{
				Color = Color.CornflowerBlue,
				WidthRequest = 12,
				HeightRequest = 12,
			};
			CheckView = check;

			var text = new Label();
			text.SetBinding(Label.TextProperty, ".");
			DataView = text;
		}

		public View CheckView
		{
			get { return checkView; }
			set
			{
				// jump out if the value is the same or something happened to our layout
				if (checkView == value || View != rootGrid)
					return;

				OnPropertyChanging();

				// remove the old binding
				if (checkView != null)
				{
					checkView.RemoveBinding(VisualElement.IsVisibleProperty);
					rootGrid.Children.Remove(checkView);
				}

				checkView = value;

				// add the new binding
				if (checkView != null)
				{
					checkView.SetBinding(VisualElement.IsVisibleProperty, nameof(SelectableItem.IsSelected));
					Grid.SetColumn(checkView, 1);
					Grid.SetColumnSpan(checkView, 1);
					Grid.SetRow(checkView, 0);
					Grid.SetRowSpan(checkView, 1);
					checkView.HorizontalOptions = LayoutOptions.End;
					checkView.VerticalOptions = LayoutOptions.Center;
					rootGrid.Children.Add(checkView);
				}

				OnPropertyChanged();
			}
		}

		public View DataView
		{
			get { return dataView; }
			set
			{
				// jump out if the value is the same or something happened to our layout
				if (dataView == value || View != rootGrid)
					return;

				OnPropertyChanging();

				// remove the old view
				if (dataView != null)
				{
					dataView.RemoveBinding(BindingContextProperty);
					rootGrid.Children.Remove(dataView);
				}

				dataView = value;

				// add the new view
				if (dataView != null)
				{
					dataView.SetBinding(BindingContextProperty, nameof(SelectableItem.Data));
					Grid.SetColumn(dataView, 0);
					Grid.SetColumnSpan(dataView, 1);
					Grid.SetRow(dataView, 0);
					Grid.SetRowSpan(dataView, 1);
					dataView.HorizontalOptions = LayoutOptions.StartAndExpand;
					dataView.VerticalOptions = LayoutOptions.FillAndExpand;
					rootGrid.Children.Add(dataView);
				}

				OnPropertyChanged();
			}
		}
	}
}
