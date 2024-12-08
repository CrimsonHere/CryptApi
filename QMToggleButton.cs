public class QMToggleButton : IDisposable
{
    private readonly string? _location;
    private readonly Action? _onAction;
    private readonly Action? _offAction;
    private GameObject? _button;
    private ImagePublicOnVoVeRaBoCaVeVoUnique? _image;
    private VRCButtonHandle? handle;
    private bool _state;

    public QMToggleButton(QMTab tab, float x, float y, Action on, Action off, string text, string toolTip, bool defaultState = false)
         : this(tab?.GetPage()?.GetName(), x, y, on, off, text, toolTip, defaultState) { }

    public QMToggleButton(QMPage page, float x, float y, Action on, Action off, string text, string toolTip, bool defaultState = false)
        : this(page.GetName(), x, y, on, off, text, toolTip, defaultState) { }

    public QMToggleButton(QMNestedButton? nestedButton, float x, float y, Action on, Action off, string text, string toolTip, bool defaultState = false)
        : this(nestedButton?.GetPage()?.GetName(), x, y, on, off, text, toolTip, defaultState) { }

    private QMToggleButton(string? location, float x, float y, Action on, Action off, string text, string toolTip, bool defaultState)
    {
        _location = location;
        _state = defaultState;
        _onAction = on;
        _offAction = off;
        InitializeButton(x, y, text, toolTip);
    }

    private void InitializeButton(float x, float y, string text, string toolTip)
    {
        _button = UnityEngine.Object.Instantiate(GameObjectStuff.GetButtonBase(), GameObject.Find($"UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/{_location}").transform, true);
        _button.name = $"{QMCollection.watermark}-ToggleButton-{new System.Random().Next(100000, 999999)}";
        _image = _button.transform.Find("Icons/Icon").GetComponentInChildren<ImagePublicOnVoVeRaBoCaVeVoUnique>();
        ConfigureButton(text, toolTip, x, y);
        QMCollection.toggleButtons.Add(this);
    }

    private void ConfigureButton(string text, string toolTip, float x, float y)
    {
        if (_button == null) return;
        RectTransform rectTransform = _button.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200f, 176f);
        rectTransform.anchoredPosition = new Vector2(-68f, -250f);
        rectTransform.anchoredPosition += Vector2.right * (232 * x);
        rectTransform.anchoredPosition += Vector2.down * (210 * y);
        TextMeshProUGUIEx textMeshPro = _button.GetComponentInChildren<TextMeshProUGUIEx>();
        textMeshPro.fontSize = 30f;
        textMeshPro._localizableString = LocalizableStringExtensions.Localize(text);
        handle = _button.GetComponent<VRCButtonHandle>();
        handle.onClick = new Button.ButtonClickedEvent();
        handle.onClick.AddListener(new Action(HandleClick));
        _button.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(toolTip);
        SetActive(true);
        SetSprite(_state ? SpriteStuff.GetOnSprite() : SpriteStuff.GetOffSprite());
    }

    private void HandleClick()
    {
        _state = !_state;
        SetSprite(_state ? SpriteStuff.GetOnSprite() : SpriteStuff.GetOffSprite());
        if (_state)
            _onAction?.Invoke();
        else
            _offAction?.Invoke();
    }

    private void SetSprite(Sprite? sprite)
    {
        if (_image != null && sprite != null)
        {
            _image.sprite = sprite;
            _image.overrideSprite = sprite;
        }
    }

    public void SetActive(bool state) => _button?.SetActive(state);
    public void Dispose() => UnityEngine.Object.Destroy(_button);
}
