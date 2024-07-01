using UnityEngine;

namespace SimpleDebugWindow
{
    public class DebugWindowDrawer : MonoBehaviour
    {
        private void OnGUI()
        {
            DebugWindowManager.DrawGUI();
        }
    }
}
