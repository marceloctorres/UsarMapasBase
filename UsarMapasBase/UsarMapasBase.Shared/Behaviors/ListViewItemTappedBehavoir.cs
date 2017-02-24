using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace UsarMapasBase.Behaviors
{
  /// <summary>
  /// 
  /// </summary>
  public class ListViewItemTappedBehavoir : BehaviorBase<ListView>
  {
    /// <summary>
    /// 
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command",
      typeof(ICommand),
      typeof(ListViewItemTappedBehavoir));

    /// <summary>
    /// 
    /// </summary>
    public ICommand Command
    {
      get { return (ICommand)GetValue(CommandProperty); }
      set
      {
        SetValue(CommandProperty, value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnAttachedTo(ListView bindable)
    {
      base.OnAttachedTo(bindable);
      bindable.ItemTapped += this.Bindable_ItemTapped;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bindable"></param>
    protected override void OnDetachingFrom(ListView bindable)
    {
      base.OnDetachingFrom(bindable);
      bindable.ItemTapped -= this.Bindable_ItemTapped;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (this.Command != null)
      {
        var item = e.Item.ToString();
        if (this.Command.CanExecute(item))
        {
          this.Command.Execute(item);
        }
      }
    }
  }
}
