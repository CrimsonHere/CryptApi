public class QMSingleButton : IDisposable
{
    private string? _location;
    private GameObject? _button;
    private ImagePublicOnVoVeRaBoCaVeVoUnique? _image;
    private VRCButtonHandle? handle;
    private TextMeshProUGUIEx? _textMesh;
    private RectTransform? _rectTransform;

    public QMSingleButton(QMNestedButton? nestedButton, float x, float y, Action action, string text, string toolTip, bool halfBtn = false)
        : this(nestedButton?.GetPage()?.GetName(), x, y, action, text, toolTip, halfBtn) { }

    public QMSingleButton(QMTab tab, float x, float y, Action action, string text, string toolTip, bool halfBtn = false)
        : this(tab?.GetPage()?.GetName(), x, y, action, text, toolTip, halfBtn) { }

    public QMSingleButton(QMPage page, float x, float y, Action action, string text, string toolTip, bool halfBtn = false)
        : this(page.GetName(), x, y, action, text, toolTip, halfBtn) { }

    public QMSingleButton(string? location, float x, float y, Action action, string text, string toolTip, bool halfBtn = false)
    {
        _location = location;
        if (halfBtn)
        {
            y -= 0.21f;
        }
        InitializeButton(x, y, action, text, toolTip);
        if (halfBtn && _button != null)
        {
            _button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
            _button.GetComponentInChildren<TextMeshProUGUIEx>().rectTransform.anchoredPosition = new Vector2(0, 22);
        }
    }

    private void InitializeButton(float x, float y, Action action, string text, string toolTip)
    {
        _button = UnityEngine.Object.Instantiate(GameObjectStuff.GetButtonBase(), GameObject.Find($"UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/{_location}").transform, true); 
        _button.name = $"{QMCollection.watermark}-SingleButton-{new System.Random().Next(100000, 999999)}";
        _image = _button.transform.Find("Icons/Icon").GetComponentInChildren<ImagePublicOnVoVeRaBoCaVeVoUnique>();
        _rectTransform = _button.GetComponent<RectTransform>();
        _textMesh = _button.GetComponentInChildren<TextMeshProUGUIEx>();
        _image.gameObject.SetActive(false);
        ConfigureButton(x, y, action, text, toolTip);
        QMCollection.singlebuttons.Add(this);
    }

    private void ConfigureButton(float x, float y, Action action, string text, string toolTip)
    {
        if (_button == null || _textMesh == null || _rectTransform == null) return;
        _textMesh.fontSize = 30f;
        _textMesh.rectTransform.anchoredPosition += new Vector2(0f, 50f);
        _rectTransform.sizeDelta = new Vector2(200f, 176f);
        _rectTransform.anchoredPosition = new Vector2(-68f, -250f);
        _rectTransform.anchoredPosition += Vector2.right * (232 * x);
        _rectTransform.anchoredPosition += Vector2.down * (210 * y);
        _button.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(toolTip);
        handle = _button.GetComponent<VRCButtonHandle>();
        SetText(text);
        SetAction(action);
        SetActive(true);
    }

    public void Click() => handle?.onClick.Invoke();

    public void SetAction(Action action)
    {
        if (_button != null && handle != null)
        {
            handle.onClick = new Button.ButtonClickedEvent();
            handle.onClick.AddListener(action);
        }
    }

    public void SetText(string text)
    {
        if (_textMesh != null)
            _textMesh._localizableString = LocalizableStringExtensions.Localize(text);
    }

    public void SetActive(bool state) => _button?.SetActive(state);
    public void Dispose() => UnityEngine.Object.Destroy(_button);
}
