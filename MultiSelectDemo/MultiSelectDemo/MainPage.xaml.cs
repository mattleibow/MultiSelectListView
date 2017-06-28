using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

using MultiSelectDemo.Controls;
using System;

namespace MultiSelectDemo
{
	public partial class MainPage : ContentPage
	{
		private bool enableMultiSelect;

		public MainPage()
		{
			InitializeComponent();

			var initialItems = new[] {
				"One",
				"Two",
				"Three",
				"Four",
				"Five"
			};

			enableMultiSelect = true;
			Items = new SelectableObservableCollection<string>(initialItems);
			AddItemCommand = new Command(OnAddItem);
			RemoveSelectedCommand = new Command(OnRemoveSelected);
			ToggleSelectionCommand = new Command(OnToggleSelection);

			BindingContext = this;
		}

		public bool EnableMultiSelect
		{
			get { return enableMultiSelect; }
			set
			{
				enableMultiSelect = value;
				OnPropertyChanged();
			}
		}

		public SelectableObservableCollection<string> Items { get; }

		public ICommand AddItemCommand { get; }

		public ICommand RemoveSelectedCommand { get; }

		public ICommand ToggleSelectionCommand { get; }

		private void OnAddItem()
		{
			Items.Add(Guid.NewGuid().ToString());
		}

		private void OnRemoveSelected()
		{
			var selectedItems = Items.SelectedItems.ToArray();
			foreach (var item in selectedItems)
			{
				Items.Remove(item);
			}
		}

		private void OnToggleSelection()
		{
			foreach (var item in Items)
			{
				item.IsSelected = !item.IsSelected;
			}
		}
	}
}
