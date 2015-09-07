﻿namespace YKToolkit.Controls
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// <c>YKToolkit.Controls.LineGraph</c> コントロールで使用する折れ線グラフ表示用のパネルです。
    /// </summary>
    internal class LineGraphPanel : Panel
    {
        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraphPanel()
        {
            this.SizeChanged += (s, e) =>
            {
                this.InvalidateVisual();
            };
        }
        #endregion コンストラクタ

        #region XMin プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnXMinChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double XMin
        {
            get { return (double)GetValue(XMinProperty); }
            set { SetValue(XMinProperty, value); }
        }

        /// <summary>
        /// XMin プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnXMinChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
                item.XMin = this.XMin;
        }
        #endregion XMin プロパティ

        #region XMax プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnXMaxChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }

        /// <summary>
        /// XMax プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnXMaxChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
                item.XMax = this.XMax;
        }
        #endregion XMax プロパティ

        #region XStep プロパティ
        /// <summary>
        /// XStep 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XStepProperty = DependencyProperty.Register("XStep", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸目盛の間隔を取得または設定します。
        /// </summary>
        public double XStep
        {
            get { return (double)GetValue(XStepProperty); }
            set { SetValue(XStepProperty, value); }
        }
        #endregion XStep プロパティ

        #region XStringFormat プロパティ
        /// <summary>
        /// XStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XStringFormatProperty = DependencyProperty.Register("XStringFormat", typeof(string), typeof(LineGraphPanel), new FrameworkPropertyMetadata("#0", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string XStringFormat
        {
            get { return (string)GetValue(XStringFormatProperty); }
            set { SetValue(XStringFormatProperty, value); }
        }
        #endregion XStringFormat プロパティ

        #region XFontSize プロパティ
        /// <summary>
        /// XFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XFontSizeProperty = DependencyProperty.Register("XFontSize", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double XFontSize
        {
            get { return (double)GetValue(XFontSizeProperty); }
            set { SetValue(XFontSizeProperty, value); }
        }
        #endregion XFontSize プロパティ

        #region XGridPen プロパティ
        /// <summary>
        /// XGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XGridPenProperty = DependencyProperty.Register("XGridPen", typeof(Pen), typeof(LineGraphPanel), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen XGridPen
        {
            get { return (Pen)GetValue(XGridPenProperty); }
            set { SetValue(XGridPenProperty, value); }
        }
        #endregion XGridPen プロパティ

        #region YMin プロパティ
        /// <summary>
        /// YMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnYMinChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 縦軸の最小値を取得または設定します。
        /// </summary>
        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }

        /// <summary>
        /// YMin プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnYMinChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
            {
                if (!item.IsSecond)
                    item.YMin = this.YMin;
            }
        }
        #endregion YMin プロパティ

        #region YMax プロパティ
        /// <summary>
        /// YMax 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnYMaxChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }

        /// <summary>
        /// YMax プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnYMaxChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
            {
                if (!item.IsSecond)
                    item.YMax = this.YMax;
            }
        }
        #endregion YMax プロパティ

        #region YStep プロパティ
        /// <summary>
        /// YStep 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YStepProperty = DependencyProperty.Register("YStep", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸目盛の間隔を取得または設定します。
        /// </summary>
        public double YStep
        {
            get { return (double)GetValue(YStepProperty); }
            set { SetValue(YStepProperty, value); }
        }
        #endregion YStep プロパティ

        #region YStringFormat プロパティ
        /// <summary>
        /// YStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YStringFormatProperty = DependencyProperty.Register("YStringFormat", typeof(string), typeof(LineGraphPanel), new FrameworkPropertyMetadata("#0", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string YStringFormat
        {
            get { return (string)GetValue(YStringFormatProperty); }
            set { SetValue(YStringFormatProperty, value); }
        }
        #endregion YStringFormat プロパティ

        #region YFontSize プロパティ
        /// <summary>
        /// YFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YFontSizeProperty = DependencyProperty.Register("YFontSize", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double YFontSize
        {
            get { return (double)GetValue(YFontSizeProperty); }
            set { SetValue(YFontSizeProperty, value); }
        }
        #endregion YFontSize プロパティ

        #region YGridPen プロパティ
        /// <summary>
        /// YGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YGridPenProperty = DependencyProperty.Register("YGridPen", typeof(Pen), typeof(LineGraphPanel), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen YGridPen
        {
            get { return (Pen)GetValue(YGridPenProperty); }
            set { SetValue(YGridPenProperty, value); }
        }
        #endregion YGridPen プロパティ

        #region IsY2Enabled プロパティ
        /// <summary>
        /// IsY2Enabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsY2EnabledProperty = DependencyProperty.Register("IsY2Enabled", typeof(bool), typeof(LineGraphPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸の有効性を取得または設定します。
        /// </summary>
        public bool IsY2Enabled
        {
            get { return (bool)GetValue(IsY2EnabledProperty); }
            set { SetValue(IsY2EnabledProperty, value); }
        }
        #endregion IsY2Enabled プロパティ

        #region Y2Min プロパティ
        /// <summary>
        /// Y2Min 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2MinProperty = DependencyProperty.Register("Y2Min", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnY2MinChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 第 2 主軸の最小値を取得または設定します。
        /// </summary>
        public double Y2Min
        {
            get { return (double)GetValue(Y2MinProperty); }
            set { SetValue(Y2MinProperty, value); }
        }

        /// <summary>
        /// Y2Min プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnY2MinChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
            {
                if (item.IsSecond)
                    item.YMin = this.Y2Min;
            }
        }
        #endregion Y2Min プロパティ

        #region Y2Max プロパティ
        /// <summary>
        /// Y2Max 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2MaxProperty = DependencyProperty.Register("Y2Max", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnY2MaxChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double Y2Max
        {
            get { return (double)GetValue(Y2MaxProperty); }
            set { SetValue(Y2MaxProperty, value); }
        }

        /// <summary>
        /// Y2Max プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnY2MaxChanged(double oldValue, double newValue)
        {
            foreach (LineGraphItem item in this.InternalChildren)
            {
                if (item.IsSecond)
                    item.YMax = this.Y2Max;
            }
        }
        #endregion Y2Max プロパティ

        #region Y2Step プロパティ
        /// <summary>
        /// Y2Step 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2StepProperty = DependencyProperty.Register("Y2Step", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸目盛の間隔を取得または設定します。
        /// </summary>
        public double Y2Step
        {
            get { return (double)GetValue(Y2StepProperty); }
            set { SetValue(Y2StepProperty, value); }
        }
        #endregion Y2Step プロパティ

        #region Y2StringFormat プロパティ
        /// <summary>
        /// Y2StringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2StringFormatProperty = DependencyProperty.Register("Y2StringFormat", typeof(string), typeof(LineGraphPanel), new FrameworkPropertyMetadata("#0", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string Y2StringFormat
        {
            get { return (string)GetValue(Y2StringFormatProperty); }
            set { SetValue(Y2StringFormatProperty, value); }
        }
        #endregion Y2StringFormat プロパティ

        #region Y2FontSize プロパティ
        /// <summary>
        /// Y2FontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2FontSizeProperty = DependencyProperty.Register("Y2FontSize", typeof(double), typeof(LineGraphPanel), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double Y2FontSize
        {
            get { return (double)GetValue(Y2FontSizeProperty); }
            set { SetValue(Y2FontSizeProperty, value); }
        }
        #endregion Y2FontSize プロパティ

        #region Y2GridPen プロパティ
        /// <summary>
        /// YGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2GridPenProperty = DependencyProperty.Register("Y2GridPen", typeof(Pen), typeof(LineGraphPanel), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen Y2GridPen
        {
            get { return (Pen)GetValue(Y2GridPenProperty); }
            set { SetValue(Y2GridPenProperty, value); }
        }
        #endregion Y2GridPen プロパティ

        #region FontFamily プロパティ
        /// <summary>
        /// FontFamily 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(string), typeof(LineGraphPanel), new FrameworkPropertyMetadata("ＭＳ ゴシック", FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphPanel).OnFontFamilyChanged(e.OldValue as string, e.NewValue as string)));

        /// <summary>
        /// フォントファミリを取得または設定します。
        /// </summary>
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// FontFamily プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnFontFamilyChanged(string oldValue, string newValue)
        {
            this._typeface = new Typeface(this.FontFamily);
        }
        #endregion FontFamily プロパティ

        #region Foreground プロパティ
        /// <summary>
        /// Foreground 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(LineGraphPanel), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 前景色を取得または設定します。
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        #endregion Foreground プロパティ

        #region 描画関連のオーバーライド
        /// <summary>
        /// 子要素の配置をおこないます。
        /// </summary>
        /// <param name="finalSize">使用可能な領域のサイズ</param>
        /// <returns>最終的に使用する領域のサイズです。<br />入力引数のサイズと異なる場合はもう一度サイズ計測がおこなわれます。</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            //return base.ArrangeOverride(finalSize);

            foreach (LineGraphItem item in this.InternalChildren)
            {
                item.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            }

            return finalSize;
        }

        private Typeface _typeface;

        /// <summary>
        /// 描画処理をおこないます。
        /// </summary>
        /// <param name="dc">描画するコンテキスト</param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (this.XMin >= this.XMax)
            {
                return;
            }
            if (this.YMin >= this.YMax)
            {
                return;
            }
            if (IsY2Enabled)
            {
                if (this.Y2Min >= this.Y2Max)
                {
                    return;
                }
            }

            if (this._typeface == null)
                this._typeface = new Typeface(this.FontFamily);

            #region 横軸目盛線の描画と目盛テキストの描画
            if ((this.XMin < this.XMax) && (this.XStep > 0.0))
            {
                var value = this.XMin;
                var ptTextOrg = new Point(this.XMin, this.YMin);
                var stringFormat = string.IsNullOrWhiteSpace(this.XStringFormat) ? "" : this.XStringFormat;
                while (value <= this.XMax)
                {
                    // 目盛線の描画
                    if ((this.XMin < value) && (value < this.XMax))
                    {
                        var pt0 = GetControlPointFromGraphPoint(value, this.YMin);
                        var pt1 = GetControlPointFromGraphPoint(value, this.YMax);
                        dc.DrawLine(this.XGridPen, pt0, pt1);
                    }

                    // 目盛テキストの描画
                    var text = new FormattedText(
                        value.ToString(stringFormat),
                        CultureInfo.CurrentUICulture,
                        this.FlowDirection,
                        this._typeface,
                        this.XFontSize,
                        this.Foreground);
                    var ptText = GetControlPointFromGraphPoint(ptTextOrg);
                    ptText.Offset(-text.Width / 2.0, 4.0);
                    dc.DrawText(text, ptText);

                    // 次の値に更新する
                    if (value >= this.XMax)
                        break;
                    value += this.XStep;
                    if (value > this.XMax)
                        value = this.XMax;
                    ptTextOrg = new Point(value, this.YMin);
                }
            }
            #endregion 横軸目盛線の描画と目盛テキストの描画

            #region 縦軸目盛線の描画と目盛テキストの描画
            if ((this.YMin < this.YMax) && (this.YStep > 0.0))
            {
                var value = this.YMin;
                var ptTextOrg = new Point(this.XMin, this.YMin);
                var stringFormat = string.IsNullOrWhiteSpace(this.YStringFormat) ? "" : this.YStringFormat;
                while (value <= this.YMax)
                {
                    // 目盛線の描画
                    if ((this.YMin < value) && (value < this.YMax))
                    {
                        var pt0 = GetControlPointFromGraphPoint(this.XMin, value);
                        var pt1 = GetControlPointFromGraphPoint(this.XMax, value);
                        dc.DrawLine(this.YGridPen, pt0, pt1);
                    }

                    // 目盛テキストの描画
                    var text = new FormattedText(
                        value.ToString(stringFormat),
                        CultureInfo.CurrentUICulture,
                        this.FlowDirection,
                        this._typeface,
                        this.YFontSize,
                        this.Foreground);
                    var ptText = GetControlPointFromGraphPoint(ptTextOrg);
                    ptText.Offset(-text.Width - 4.0, -text.Height / 2.0);
                    dc.DrawText(text, ptText);

                    // 次の値に更新する
                    if (value >= this.YMax)
                        break;
                    value += this.YStep;
                    if (value > this.YMax)
                        value = this.YMax;
                    ptTextOrg = new Point(this.XMin, value);
                }
            }
            #endregion 縦軸目盛線の描画と目盛テキストの描画

            #region 第 2 主軸目盛線の描画と目盛テキストの描画
            if (this.IsY2Enabled)
            {
                if ((this.Y2Min < this.Y2Max) && (this.Y2Step > 0.0))
                {
                    var value = this.Y2Min;
                    var ptTextOrg = new Point(this.XMax, this.Y2Min);
                    var stringFormat = string.IsNullOrWhiteSpace(this.Y2StringFormat) ? "" : this.Y2StringFormat;
                    while (value <= this.Y2Max)
                    {
                        // 目盛線の描画
                        if ((this.Y2Min < value) && (value < this.Y2Max))
                        {
                            var pt0 = GetControlPointFromGraphPoint(this.XMin, value, true);
                            var pt1 = GetControlPointFromGraphPoint(this.XMax, value, true);
                            dc.DrawLine(this.Y2GridPen, pt0, pt1);
                        }

                        // 目盛テキストの描画
                        var text = new FormattedText(
                            value.ToString(stringFormat),
                            CultureInfo.CurrentUICulture,
                            this.FlowDirection,
                            this._typeface,
                            this.Y2FontSize,
                            this.Foreground);
                        var ptText = GetControlPointFromGraphPoint(ptTextOrg, true);
                        ptText.Offset(4.0, -text.Height / 2.0);
                        dc.DrawText(text, ptText);

                        // 次の値に更新する
                        if (value >= this.Y2Max)
                            break;
                        value += this.Y2Step;
                        if (value > this.Y2Max)
                            value = this.Y2Max;
                        ptTextOrg = new Point(this.XMax, value);
                    }
                }
            }
            #endregion 第 2 主軸目盛線の描画と目盛テキストの描画
        }
        #endregion 描画関連のオーバーライド

        #region 座標変換ヘルパ
        /// <summary>
        /// グラフ座標をコントロール座標に変換します。
        /// </summary>
        /// <param name="pt">グラフ座標を指定します。</param>
        /// <param name="isSecond">第 2 主軸を使用する場合は true を指定します。</param>
        /// <returns>コントロール座標</returns>
        private Point GetControlPointFromGraphPoint(Point pt, bool isSecond = false)
        {
            return GetControlPointFromGraphPoint(pt.X, pt.Y, isSecond);
        }

        /// <summary>
        /// グラフ座標をコントロール座標に変換します。
        /// </summary>
        /// <param name="x">グラフの横軸座標</param>
        /// <param name="y">グラフの縦軸座標</param>
        /// <param name="isSecond">第 2 主軸を使用する場合に true を指定します。</param>
        /// <returns>コントロール座標</returns>
        private Point GetControlPointFromGraphPoint(double x, double y, bool isSecond = false)
        {
            var ymin = isSecond ? this.Y2Min : this.YMin;
            var ymax = isSecond ? this.Y2Max : this.YMax;
            var xx = this.ActualWidth * (x - this.XMin) / (this.XMax - this.XMin);
            var yy = this.ActualHeight - this.ActualHeight * (y - ymin) / (ymax - ymin);
            return new Point(xx, yy);
        }
        #endregion 座標変換ヘルパ
    }
}