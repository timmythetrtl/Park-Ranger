public static class EventManager
{
    public delegate void PanelStateChange(bool panelOpen);
    public static event PanelStateChange OnPanelStateChanged;

    public static void InvokePanelStateChanged(bool panelOpen)
    {
        OnPanelStateChanged?.Invoke(panelOpen);
    }
}
