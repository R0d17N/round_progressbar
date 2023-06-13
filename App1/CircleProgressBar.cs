using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace App1
{
    public class CircleProgressBar : SKCanvasView
    {
        // Определение свойства прогресса
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(CircleProgressBar), 0d,
                propertyChanged: OnProgressPropertyChanged);

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        private static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var progressBar = (CircleProgressBar)bindable;
            progressBar.InvalidateSurface();
        }

        // Определение свойства цвета прогресса
        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircleProgressBar), Color.Default);

        public Color ProgressColor
        {
            get { return (Color)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            base.OnPaintSurface(args);

            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKImageInfo info = args.Info;

            canvas.Clear();

            // Рисуем круговой фон
            float strokeWidth = 10;
            float radius = Math.Min(info.Width, info.Height) / 2;
            float centerX = info.Width / 2;
            float centerY = info.Height / 2;

            SKPaint backgroundPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Gray.ToSKColor(),
                StrokeWidth = strokeWidth
            };

            canvas.DrawCircle(centerX, centerY, radius, backgroundPaint);

            // Рисуем прогресс
            SKPaint progressPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = ProgressColor.ToSKColor(),
                StrokeWidth = strokeWidth,
                StrokeCap = SKStrokeCap.Round
            };

            float sweepAngle = (float)(Progress * 360);
            canvas.DrawArc(new SKRect(centerX - radius, centerY - radius, centerX + radius, centerY + radius), -90, sweepAngle, false, progressPaint);
        }
    }
}
