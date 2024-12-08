 public class QMScrollMenu : IDisposable
 {
     public QMNestedButton? _baseMenu;
     private QMSingleButton? _backButton;
     private QMSingleButton? _nextButton;

     private Action<QMScrollMenu>? _openAction;

     private float _posX = 1;
     private float _posY = 0;
     private int _index = 0;
     private int _currentMenuIndex = 0;

     public List<ScrollObject> QMButtons = new List<ScrollObject>();

     public class ScrollObject
     {
         public QMToggleButton? _toggleButton;
         public QMSingleButton? _singleButton;
         public int _index = 0;
     }

     public QMScrollMenu(QMNestedButton? nestedButton, float x, float y, string text, string toolTip, string menuTitle)
     {
         _baseMenu = new QMNestedButton(nestedButton, x, y, text, toolTip, menuTitle);
         InitializeNavigationButtons();
     }

     public QMScrollMenu(string btnMenu, float x, float y, string text, string toolTip, string menuTitle)
     {
         _baseMenu = new QMNestedButton(btnMenu, x, y, text, toolTip, menuTitle);
         InitializeNavigationButtons();
     }

     private void InitializeNavigationButtons()
     {
         _backButton = new QMSingleButton(_baseMenu, 2f, 3f, () => ShowMenu(_currentMenuIndex - 1), "<---", "Go Back");
         _nextButton = new QMSingleButton(_baseMenu, 3f, 3f, () => ShowMenu(_currentMenuIndex + 1), "--->", "Go Next");
     }

     public void SetAction(Action<QMScrollMenu> openAction, bool shouldClear = true)
     {
         _openAction = openAction;
         _baseMenu?.GetButton()?.SetAction(() =>
         {
             if (shouldClear) Clear();
             _openAction(this);
             _baseMenu.Open();
             ShowMenu(0);
         });
     }

     public void ShowMenu(int menuIndex)
     {
         if (menuIndex < 0 || menuIndex > _index) return;
         foreach (ScrollObject scrollObject in QMButtons)
         {
             QMSingleButton? singleButton = scrollObject._singleButton;
             if (singleButton != null)
                 singleButton?.SetActive(scrollObject._index == menuIndex);
            QMToggleButton? toggleButton = scrollObject._toggleButton;
             if (toggleButton != null)
                 toggleButton?.SetActive(scrollObject._index == menuIndex);
         }
         _currentMenuIndex = menuIndex;
     }

     public void Add(string text, string description, Action action)
     {
         if (_posY == 3f)
         {
             _posY = 0f;
             _index++;
         }
         QMSingleButton button = new QMSingleButton(_baseMenu, _posX, _posY, action, text, description);
         button.SetActive(false);
         QMButtons.Add(new ScrollObject
         {
             _singleButton = button,
             _index = _index
         });
         _posX++;
         if (_posX == 5f)
         {
             _posX = 1f;
             _posY += 1f;
         }
     }

     public void Add(Action action1, Action action2, string text, string description, bool config = false)
     {
         if (_posY == 3f)
         {
             _posY = 0f;
             _index++;
         }
         QMToggleButton button = new QMToggleButton(_baseMenu, _posX, _posY, action1, action2, text, description, config);
         button.SetActive(false);
         QMButtons.Add(new ScrollObject
         {
             _toggleButton = button,
             _index = _index
         });
         _posX++;
         if (_posX == 5f)
         {
             _posX = 1f;
             _posY += 1f;
         }
     }

     public void Clear()
     {
         foreach (ScrollObject scrollObject in QMButtons)
         {
             QMSingleButton? singleButton = scrollObject._singleButton;
             if (singleButton != null)
                 singleButton?.Dispose();
             QMToggleButton? toggleButton = scrollObject._toggleButton;
             if (toggleButton != null)
                 toggleButton?.Dispose();
         }
         QMButtons.Clear();
         _posX = 1f;
         _posY = 0f;
         _index = 0;
         _currentMenuIndex = 0;
     }

     public void Dispose()
     {
         Clear();
         _baseMenu?.Dispose();
         _backButton?.Dispose();
         _nextButton?.Dispose();
     }
 }
