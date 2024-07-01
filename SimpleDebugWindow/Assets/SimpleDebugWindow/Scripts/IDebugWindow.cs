namespace SimpleDebugWindow
{
    public interface IDebugWindow
    {
        int WindowId { get; }
        void DrawGUI();
    }
}
