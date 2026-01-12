using System;

public class UIEvents : SingletonBase<UIEvents>
{
    public event Action Hover;
    public event Action Click;

    public void UIHover() => Hover?.Invoke();
    public void UIClick() => Click?.Invoke();
}
