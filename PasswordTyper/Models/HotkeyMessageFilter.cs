namespace PasswordTyper.Models
{
    public class HotkeyMessageFilter : IMessageFilter
    {
        private readonly int hotkeyId;
        private readonly Action callback;

        public HotkeyMessageFilter(int hotkeyId, Action callback)
        {
            this.hotkeyId = hotkeyId;
            this.callback = callback;
        }

        public bool PreFilterMessage(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY && (int)m.WParam == hotkeyId)
            {
                callback.Invoke();
                return true;
            }
            return false;
        }
    }
}
