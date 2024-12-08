 public class SpriteStuff
 {
     private static Sprite? _ontoggle;
     public static Sprite GetOnSprite()
     {
         if (_ontoggle == null)
             _ontoggle = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<ImagePublicOnVoVeRaBoCaVeVoUnique>().sprite;
         return _ontoggle;
     }

     private static Sprite? _offtoggle;
     public static Sprite GetOffSprite()
     {
         if (_offtoggle == null)
             _offtoggle = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Notifications/Panel_Notification_Tabs/Button_ClearNotifications/Text_FieldContent/Icon").GetComponent<ImagePublicOnVoVeRaBoCaVeVoUnique>().sprite;
         return _offtoggle;
     }
 }
