namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, ShowDesktopDisableBehavior.WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        internal static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        internal static extern int GetClassName(IntPtr hwnd, StringBuilder name, int count);
    }

    /// <summary>
    /// �u�f�X�N�g�b�v��\���v�{�^�����������Ƃ��A�E�B���h�E���ŏ������邩�ǂ����𐧌䂷�邽�߂̃r�w�C�r�A��\���܂��B
    /// </summary>
    public class ShowDesktopDisableBehavior
    {
        #region IsEnabled �Y�t�v���p�e�B
        /// <summary>
        /// IsEnabled �Y�t�v���p�e�B�̒�`
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ShowDesktopDisableBehavior), new PropertyMetadata(OnIsEnabledPropertyChanged));

        /// <summary>
        /// IsEnabled �Y�t�v���p�e�B���擾���܂��B
        /// </summary>
        /// <param name="target">�ΏۂƂ��� DependencyObject ���w�肵�܂��B</param>
        /// <returns>�擾�����l��Ԃ��܂��B</returns>
        public static bool GetIsEnabled(DependencyObject target)
        {
            return (bool)target.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// IsEnabled �Y�t�v���p�e�B��ݒ肵�܂��B
        /// </summary>
        /// <param name="target">�ΏۂƂ��� DependencyObject ���w�肵�܂��B</param>
        /// <param name="value">�ݒ肷��l���w�肵�܂��B</param>
        public static void SetIsEnabled(DependencyObject target, bool value)
        {
            target.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// IsEnabled �Y�t�v���p�e�B�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window;
            if (w == null)
                return;

            var isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                w.SourceInitialized += OnSourceInitialized;
            }
            else
            {
                RemoveHook(w);
                w.SourceInitialized -= OnSourceInitialized;
            }
        }
        #endregion IsEnabled �Y�t�v���p�e�B

        /// <summary>
        /// Window �N���X�̃E�B���h�E�n���h�������肷��ő��̃C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        private static void OnSourceInitialized(object sender, EventArgs e)
        {
            var w = sender as Window;
            AddHook(w);
        }

        private const uint WINEVENT_OUTOFCONTEXT = 0u;
        private const uint EVENT_SYSTEM_FOREGROUND = 3u;

        private const string WORKERW = "WorkerW";
        private const string PROGMAN = "Progman";

        /// <summary>
        /// �t�b�N���J�n���܂��B
        /// </summary>
        /// <param name="window">�ΏۂƂ���E�B���h�E���w�肵�܂��B</param>
        public static void AddHook(Window window)
        {
            if (IsHooked)
            {
                return;
            }

            IsHooked = true;

            _window = window;
            _delegate = new WinEventDelegate(WinEventHook);
            _hookIntPtr = NativeMethods.SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        /// <summary>
        /// �t�b�N���������܂��B
        /// </summary>
        /// <param name="window">�ΏۂƂ���E�B���h�E���w�肵�܂��B</param>
        public static void RemoveHook(Window window)
        {
            if (!IsHooked)
            {
                return;
            }

            IsHooked = false;

            NativeMethods.UnhookWinEvent(_hookIntPtr.Value);

            _delegate = null;
            _hookIntPtr = null;
            _window = null;
        }

        private static string GetWindowClass(IntPtr hwnd)
        {
            StringBuilder _sb = new StringBuilder(32);
            NativeMethods.GetClassName(hwnd, _sb, _sb.Capacity);
            return _sb.ToString();
        }

        internal delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        private static void WinEventHook(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == EVENT_SYSTEM_FOREGROUND)
            {
                string _class = GetWindowClass(hwnd);

                if (string.Equals(_class, WORKERW, StringComparison.Ordinal) /*|| string.Equals(_class, PROGMAN, StringComparison.Ordinal)*/ )
                {
                    _window.Topmost = true;
                }
                else
                {
                    _window.Topmost = false;
                }
            }
        }

        /// <summary>
        /// �t�b�N�ς݂��ǂ������擾���܂��B
        /// </summary>
        public static bool IsHooked { get; private set; }

        private static IntPtr? _hookIntPtr { get; set; }

        private static WinEventDelegate _delegate { get; set; }

        private static Window _window { get; set; }
    }
}