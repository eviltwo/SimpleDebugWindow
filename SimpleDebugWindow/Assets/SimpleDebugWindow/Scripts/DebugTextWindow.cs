using System.Collections.Generic;
using UnityEngine;

namespace SimpleDebugWindow
{
    public class DebugTextWindow : IDebugWindow
    {
        public int WindowId { get; }
        public string Title { get; set; }

        private Rect _rect = new Rect();
        private List<string> _texts = new List<string>();
        private Vector2 _contentSize;
        private float _contentWidthDetectedTime;
        private float _contentHeightDetectedTime;
        private const float ContentSizeKeepDuration = 1.0f;

        internal DebugTextWindow(int windowId, string title)
        {
            WindowId = windowId;
            Title = title;
        }

        public void ClearTexts()
        {
            _texts.Clear();
        }

        public void PushText(string text)
        {
            _texts.Add(text);
        }

        public void PushTexts(IEnumerable<string> texts)
        {
            _texts.AddRange(texts);
        }

        public void DrawGUI()
        {
            // Calculate content size
            var contentSize = GetContentSize(_texts);
            if (contentSize.x >= _contentSize.x || Time.time > _contentWidthDetectedTime + ContentSizeKeepDuration)
            {
                _contentSize.x = contentSize.x;
                _contentWidthDetectedTime = Time.time;
            }
            if (contentSize.y >= _contentSize.y || Time.time > _contentHeightDetectedTime + ContentSizeKeepDuration)
            {
                _contentSize.y = contentSize.y;
                _contentHeightDetectedTime = Time.time;
            }

            // Calculate window size
            var titleWidth = GUI.skin.label.CalcSize(new GUIContent(Title)).x;
            var windowSize = new Vector2(
                Mathf.Max(titleWidth, _contentSize.x + GUI.skin.window.padding.horizontal),
                _contentSize.y + GUI.skin.window.padding.vertical);
            _rect.size = windowSize;

            // Draw window
            _rect = GUILayout.Window(WindowId, _rect, id =>
            {
                for (var i = 0; i < _texts.Count; i++)
                {
                    GUILayout.Label(_texts[i]);
                }
                GUI.DragWindow();
            }, Title);
        }

        private static Vector2 GetContentSize(IReadOnlyList<string> texts)
        {
            var contentSize = Vector2.zero;
            for (int i = 0; i < texts.Count; i++)
            {
                var size = GUI.skin.label.CalcSize(new GUIContent(texts[i]));
                contentSize.x = Mathf.Max(size.x, contentSize.x);
                contentSize.y += size.y;
            }
            return contentSize;
        }
    }
}
