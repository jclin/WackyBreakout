public sealed class HelpMenu : Menu
{
    protected override void Initialize()
    {
    }

    public void HandleGoBackOnClickEvent()
    {
        GoToMenu(MenuName.Main);
    }
}