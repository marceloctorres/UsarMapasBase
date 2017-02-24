using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace UsarMapasBase.Behaviors
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class BehaviorBase<T> : Behavior<T> where T : BindableObject
  {
    public T AssociatedObject { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bindable"></param>
    protected override void OnAttachedTo(T bindable)
    {
      base.OnAttachedTo(bindable);
      this.AssociatedObject = bindable;
      if (bindable.BindingContext != null)
      {
        this.BindingContext = bindable.BindingContext;
      }
      bindable.BindingContextChanged += this.OnBindingContextChanged;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bindable"></param>
    protected override void OnDetachingFrom(T bindable)
    {
      base.OnDetachingFrom(bindable);
      bindable.BindingContextChanged -= OnBindingContextChanged;
      this.AssociatedObject = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      this.OnBindingContextChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnBindingContextChanged()
    {
      base.OnBindingContextChanged();
      this.BindingContext = AssociatedObject.BindingContext;
    }
  }
}
