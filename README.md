# MultiSelectListView

An example app to show one way in which to create a multi-select ListView in Xamarin.Forms.

More info here: https://dotnetdevaddict.wordpress.com/2017/06/29/multi-select-listview/

This app has some new types, such as the selectable item:

```csharp
public class SelectableItem : BindableObject
{
    public static readonly BindableProperty DataProperty;
    public static readonly BindableProperty IsSelectedProperty;

    public SelectableItem(object data);
    public SelectableItem(object data, bool isSelected);

    public object Data { get; set; }
    public bool IsSelected { get; set; }
}
public class SelectableItem<T> : SelectableItem
{
    public SelectableItem(T data);
    public SelectableItem(T data, bool isSelected);

    public new T Data { get; set; }
}
```

There is also a selectable collection, with some convenience members:

```csharp
public class SelectableObservableCollection<T> : ObservableCollection<SelectableItem<T>>
{
    public SelectableObservableCollection();
    public SelectableObservableCollection(IEnumerable<T> collection);
    public SelectableObservableCollection(IEnumerable<SelectableItem<T>> collection);

    public IEnumerable<T> SelectedItems { get; }
    public IEnumerable<T> UnselectedItems { get; }

    public void Add(T item, bool selected = false);
    public void Insert(int index, T item, bool selected = false);
    public bool IsSelected(T item);
    public bool Contains(T item);
    public int IndexOf(T item);
    public bool Remove(T item);
}
```

These types can be used like any other collection:

```csharp
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Items = new SelectableObservableCollection<string>();

        Items.Add("Hello World");
        Items.Add("Testing 1. 2. 3.");
        Items.Add("Xamarin.Forms", true); // this one is selected

        BindingContext = this;
    }

    public SelectableObservableCollection<string> Items { get; }
}
```

But, they make any list support multi-select:

```xml
<ListView ItemsSource="{Binding Items}"
        local:MultiSelectListView.IsMultiSelect="true">
    <ListView.ItemTemplate>
        <DataTemplate>
            <local:SelectableViewCell>

                <!-- set the selected indicator (optional) -->
                <local:SelectableViewCell.CheckView>
                    <BoxView Color="Red" WidthRequest="12" HeightRequest="12" />
                </local:SelectableViewCell.CheckView>

                <!-- set the content (optional) -->
                <local:SelectableViewCell.DataView>
                    <Label Text="{Binding}" />
                </local:SelectableViewCell.DataView>

            </local:SelectableViewCell>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```
