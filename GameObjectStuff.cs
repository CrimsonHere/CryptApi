public static class GameObjectStuff
{
    private static GameObject? tab_button_Base;
    public static GameObject GetTabButton()
    {
        if (tab_button_Base == null)
            tab_button_Base = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools");
        return tab_button_Base;
    }

    private static GameObject? menu_Base;
    public static GameObject GetMenuBase()
    {
        if (menu_Base == null)
            menu_Base = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard");
        return menu_Base;
    }

    private static GameObject? button_Base;
    public static GameObject GetButtonBase()
    {
        if (button_Base == null)
            button_Base = GetQuickMenu().GetComponentsInChildren<Button>(true).Where(b => b.name == "Button_Screenshot").First().gameObject;
        return button_Base;
    }

    private static QuickMenu? quick_Menu;
    public static QuickMenu GetQuickMenu()
    {
        if (quick_Menu == null)
            quick_Menu = Resources.FindObjectsOfTypeAll<QuickMenu>()[0];
        return quick_Menu;
    }

    private static MenuStateController? menu_State_Controller;
    public static MenuStateController GetMenuStateController()
    {
        if (menu_State_Controller == null)
            menu_State_Controller = GetQuickMenu().GetComponent<MenuStateController>();
        return menu_State_Controller;
    }

    public static void DestroyChildren(this Transform transform, Func<Transform, bool>? exclude = null)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (exclude == null || exclude(transform.GetChild(i)))
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
