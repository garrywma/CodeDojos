using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CHIP_8_Virtual_Machine;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Chip8.UI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isClosing = false;
        private VM _vm;
        private string _imagePath;

        public MainWindow()
        {
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                _imagePath = dialog.FileName;
            }
            else
            {
                this.Close();
            }

            SetupVM();
        }


        private void SetupVM()
        {
            _vm = new VM(new WindowsKeypadMap());
            _vm.Load(_imagePath);
            _vm.Display.OnDisplayUpdated += (sender, info) => UpdateDisplay(info);
            _vm.Run(ClockMode.Threaded, 2);

            // play a beep to initialise Console.Beep properly
            _vm.Sound.Beep(500);
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            _vm.Keypad.KeyUp(e.Key.ToString());
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _vm.Keypad.KeyDown(e.Key.ToString());
        }

        private void UpdateDisplay(DisplayUpdateInfo info)
        {
            int magnification = 20;
            byte[] pixelBytes = info.Pixels.ToRGBA(magnification);

            Dispatcher.InvokeAsync(() =>
            {
                var bitmap = BitmapFactory.New(info.Width, info.Height).FromByteArray(pixelBytes);
                Screen.Source = bitmap;
            });
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            _isClosing = true;
            _vm?.Stop();
            base.OnClosing(e);
            Application.Current.Shutdown();
        }
    }
}
