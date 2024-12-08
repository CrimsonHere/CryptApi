public class QMNestedButton : IDisposable
{
    private string? Location;
    private QMPage? _Page;
    private QMSingleButton? _button;

    public QMNestedButton(QMNestedButton? nestedButton, float x, float y, string text, string toolTip, string menuTitle, bool halfButton = false)
    {
        Location = nestedButton?.GetPage()?.GetName();
        Instantiate(false, x, y, text, toolTip, menuTitle, halfButton);
    }

    public QMNestedButton(string loc, float x, float y, string text, string toolTip, string menuTitle, bool halfButton = false)
    {
        Location = loc;
        Instantiate(Location.StartsWith("Menu_"), x, y, text, toolTip, menuTitle, halfButton);
    }

    public QMNestedButton(QMTab tab, float x, float y, string text, string toolTip, string menuTitle, bool halfButton = false)
    {
        Location = tab?.GetPage()?.GetName();
        Instantiate(false, x, y, text, toolTip, menuTitle, halfButton);
    }

    private void Instantiate(bool isRoot, float X, float Y, string Text, string ToolTip, string menuTitle, bool halfButton)
    {
        _Page = new QMPage(menuTitle, false);
        GameObject _BackButton = _Page.GetPageObject().transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
        _BackButton.SetActive(true);
        _BackButton.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
        _BackButton.GetComponentInChildren<Button>().onClick.AddListener(new Action(() =>
        {
            if (isRoot && !string.IsNullOrEmpty(Location))
            {
                if (Location.StartsWith("Menu_"))
                {
                    GameObjectStuff.GetMenuStateController().Method_Public_Void_String_Boolean_Boolean_PDM_0("QuickMenu" + Location.Remove(0, 5));
                    return;
                }
                GameObjectStuff.GetMenuStateController().Method_Public_Void_String_Boolean_Boolean_PDM_0(Location);
                return;
            }
            _Page.GetMenuComp().Method_Protected_Virtual_New_Void_0();
        }));
        _button = new QMSingleButton(Location, X, Y, Open, Text, ToolTip, halfButton);
        QMCollection.nestedbuttons.Add(this);
    }

    public void Open()
    {
        if (_Page != null)
            GameObjectStuff.GetMenuStateController().Method_Public_Void_String_UIContext_Boolean_TransitionType_0(_Page.GetMenuComp().field_Public_String_0);
    }

    public QMPage? GetPage() => _Page;
    public QMSingleButton? GetButton() => _button;

    public void Dispose()
    {
        if (_button != null)
            _button.Dispose();
        if (_Page != null)
            _Page.Dispose();
    }
}
