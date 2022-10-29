using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace PixelRulerWin2D.Controls
{
    public sealed partial class RulerControl : UserControl, INotifyPropertyChanged
    {
        public RulerControl()
        {
            this.InitializeComponent();
            RulerWidth = 50;
            BackgroundColor = Color.FromArgb(128, 238, 238, 238);
            LargeSteps = 50;
            SmallSteps = 10;
        }
        #region Properties
        public static readonly DependencyProperty RulerWidthProperty = DependencyProperty.Register(nameof(RulerWidth), typeof(int), typeof(RulerControl), new PropertyMetadata(null));

        public int RulerWidth
        {
            get => (int)GetValue(RulerWidthProperty);
            set => SetValue(RulerWidthProperty, value);
        }
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(nameof(BackgroundColor), typeof(Color), typeof(RulerControl), new PropertyMetadata(null));

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set { SetValue(BackgroundColorProperty, value); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty LargeStepsProperty = DependencyProperty.Register(nameof(LargeSteps), typeof(int), typeof(RulerControl), new PropertyMetadata(null));

        public int LargeSteps
        {
            get => (int)GetValue(LargeStepsProperty);
            set => SetValue(LargeStepsProperty, value);
        }

        public static readonly DependencyProperty SmallStepsProperty = DependencyProperty.Register(
            nameof(SmallSteps),
            typeof(int),
            typeof(RulerControl),
            new PropertyMetadata(null)
        );

        public int SmallSteps
        {
            get => (int)GetValue(SmallStepsProperty);
            set => SetValue(SmallStepsProperty, value);
        } 
        #endregion
        
        private void RulerCanvas_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var session = args.DrawingSession;
            var pixelW = Window.Current.Bounds.Width;
            var pixelH = Window.Current.Bounds.Height;
            var largePixel = pixelW > pixelH ? pixelW : pixelH;

            session.DrawLine(0, 0, (int)largePixel, 0, Colors.Black, 1);
            session.DrawLine(0, RulerWidth, (int)largePixel, RulerWidth, Colors.Black, 1);
            for (int x = 0; x < 1920; x += LargeSteps)
            {
                session.DrawText(x.ToString(), x, 30, Colors.Black, new CanvasTextFormat()
                {
                    FontSize = (float)FontSize,
                    FontFamily = FontFamily.Source,
                    HorizontalAlignment = CanvasHorizontalAlignment.Center,
                    VerticalAlignment = CanvasVerticalAlignment.Center
                });
            }
            for (int x = 0; x < 1920; x += LargeSteps)
            {
                for (int x1 = x + SmallSteps; x1 < x + LargeSteps; x1 += SmallSteps)
                {
                    session.DrawLine(x1, 0, x1, 10, Colors.Black, 1);
                }
                session.DrawLine(x, 0, x, 20, Colors.Black, 1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
