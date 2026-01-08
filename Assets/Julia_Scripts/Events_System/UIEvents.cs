using System;

public class UIEvents
{
    public static event Action Hover;
    public static event Action Click;

    public static void UIHover() => Hover?.Invoke();
    public static void UIClick() => Click?.Invoke();
}
