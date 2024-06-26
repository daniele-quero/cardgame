using System;

public class GameChoiceButton : BaseToggle
{
    public static Action<bool> OpenGameChoice;

    protected override void Awake()
    {
        base.Awake();
        OpenGameChoice += Untoggle;
    }

    public void LaunchOpenGameChoice(bool openGameChoice)
    {
        OpenGameChoice?.Invoke(openGameChoice);
        ButtonPressed?.Invoke();
    }
}
