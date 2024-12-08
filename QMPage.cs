 public class QMPage : IDisposable
 {
     private string Name;
     private GameObject Menu;
     private UIPage menu_comp;

     public QMPage(string name, bool is_root)
     {
         Name = $"Menu_{name}_{QMCollection.watermark}_{new System.Random().Next(100000, 999999)}";
         Menu = UnityEngine.Object.Instantiate(GameObjectStuff.GetMenuBase(), GameObjectStuff.GetMenuBase().transform.parent);
         Menu.name = Name;
         Menu.SetActive(false);
         UnityEngine.Object.DestroyImmediate(Menu.GetComponent<MainMenuContent>());
         menu_comp = Menu.AddComponent<UIPage>();
         menu_comp.field_Public_String_0 = Name;
         menu_comp.field_Private_Boolean_1 = true;
         menu_comp.field_Protected_MenuStateController_0 = GameObjectStuff.GetMenuStateController();
         menu_comp.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
         menu_comp.field_Private_List_1_UIPage_0.Add(menu_comp);
         menu_comp.GetComponent<Canvas>().enabled = true;
         menu_comp.GetComponent<CanvasGroup>().enabled = true;
         menu_comp.GetComponent<UIPage>().enabled = true;
         menu_comp.GetComponent<GraphicRaycaster>().enabled = true;
         GameObjectStuff.GetMenuStateController().field_Private_Dictionary_2_String_UIPage_0.Add(Name, menu_comp);
         if (is_root)
         {
             List<UIPage> tmpList = GameObjectStuff.GetMenuStateController().field_Public_Il2CppReferenceArray_1_UIPage_0.ToList();
             tmpList.Add(menu_comp);
             GameObjectStuff.GetMenuStateController().field_Public_Il2CppReferenceArray_1_UIPage_0 = tmpList.ToArray();
         }
         Menu.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
         Menu.GetComponentInChildren<TextMeshProUGUIEx>(true).prop_String_0 = name;
         QMCollection.pages.Add(this);
     }

     public void SetActive(bool active) => Menu.SetActive(active);
     public GameObject GetPageObject() => Menu;
     public UIPage GetMenuComp() => menu_comp;
     public string GetName() => Name;
     public void Dispose() => UnityEngine.Object.Destroy(Menu);
 }
