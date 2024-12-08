public class QMTab : IDisposable
{
    private GameObject? Tab;
    private MenuTab? tab_comp;
    private QMPage? _Page;

    public QMTab(string name, string info)
    {
        Instantiate(name);
        SetToolTip(info);
    }

    public QMTab(string name, string info, Sprite sprite)
    {
        Instantiate(name);
        SetToolTip(info);
        SetSprite(sprite);
    }

    private void Instantiate(string name)
    {
        _Page = new QMPage(name, true);
        Tab = UnityEngine.Object.Instantiate(GameObjectStuff.GetTabButton(), GameObjectStuff.GetTabButton().transform.parent);
        Tab.name = $"Page_{name}_{QMCollection.watermark}_{new System.Random().Next(100000, 999999)}";
        tab_comp = Tab.GetComponent<MenuTab>();
        tab_comp.field_Private_MenuStateController_0 = GameObjectStuff.GetMenuStateController();
        tab_comp.prop_String_0 = _Page.GetName();
        Tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab_comp.GetComponent<Button>();
        Tab.GetComponent<Button>().onClick.AddListener(new Action(() =>
        {
            _Page.SetActive(true);
            tab_comp.GetComponent<StyleElement>().field_Private_Selectable_0 = tab_comp.GetComponent<Button>();
        }));
        Tab.SetActive(true);
        QMCollection.tabs.Add(this);
    }

    private void SetToolTip(string newText)
    {
        if (Tab != null)
            Tab.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(newText);
    }

    private void SetSprite(Sprite sprite)
    {
        if (Tab != null)
            Tab.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
    }

    public QMPage? GetPage() => _Page;

    public void Dispose()
    {
        if (_Page != null)
            _Page.Dispose();
        UnityEngine.Object.Destroy(Tab);
    }
}
