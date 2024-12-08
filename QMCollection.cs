public class QMCollection
{
    public static string watermark = "Crypt";
    public static List<QMTab> tabs = new List<QMTab>();
    public static List<QMPage> pages = new List<QMPage>();
    public static List<QMSingleButton> singlebuttons = new List<QMSingleButton>();
    public static List<QMToggleButton> toggleButtons = new List<QMToggleButton>();
    public static List<QMNestedButton> nestedbuttons = new List<QMNestedButton>();
    public static List<QMScrollMenu> scrollMenus = new List<QMScrollMenu>();

    public static void Clean()
    {
        foreach (QMTab tab in tabs)
            tab.Dispose();
        foreach (QMPage page in pages)
            page.Dispose();
        foreach (QMSingleButton button in singlebuttons)
            button.Dispose();
        foreach (QMToggleButton toggleButton in toggleButtons)
            toggleButton.Dispose();
        foreach (QMNestedButton nestedButton in nestedbuttons)
            nestedButton.Dispose();
        foreach (QMScrollMenu scrollMenu in scrollMenus)
            scrollMenu.Dispose();
        tabs.Clear();
        pages.Clear();
        singlebuttons.Clear();
        toggleButtons.Clear();
        nestedbuttons.Clear();
        scrollMenus.Clear();
    }
}
