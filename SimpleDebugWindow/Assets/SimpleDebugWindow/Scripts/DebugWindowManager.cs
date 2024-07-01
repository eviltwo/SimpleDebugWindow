using System.Collections.Generic;

namespace SimpleDebugWindow
{
    public static class DebugWindowManager
    {
        private static int _nextWindowId = 0;
        private static List<IDebugWindow> _windows = new List<IDebugWindow>();

        public static void ResetWindowId(int initialId)
        {
            _nextWindowId = initialId;
        }

        public static DebugTextWindow CreateTextWindow(string title)
        {
            var window = new DebugTextWindow(_nextWindowId++, title);
            _windows.Add(window);
            return window;
        }

        public static bool TryGetWindow(int windowId, out IDebugWindow result)
        {
            for (var i = 0; i < _windows.Count; i++)
            {
                if (_windows[i].WindowId == windowId)
                {
                    result = _windows[i];
                    return true;
                }
            }

            result = null;
            return false;
        }

        public static void DeleteWindow(IDebugWindow window)
        {
            _windows.Remove(window);
        }

        public static void DeleteWindow(int windowId)
        {
            _windows.RemoveAll(w => w.WindowId == windowId);
        }

        /// <summary>
        /// Called by <see cref="DebugWindowDrawer.OnGUI"/>
        /// </summary>
        internal static void DrawGUI()
        {
            for (var i = 0; i < _windows.Count; i++)
            {
                _windows[i].DrawGUI();
            }
        }
    }
}
