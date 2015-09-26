﻿namespace YKToolkit.Sample.ViewModels
{
    using System.Linq;
    using YKToolkit.Bindings;
    using YKToolkit.Helpers;

    public class FourierViewModel : ViewModelBase
    {
        private string _source;
        /// <summary>
        /// 元データを取得または設定します。
        /// </summary>
        public string Source
        {
            get { return this._source; }
            set { SetProperty(ref this._source, value); }
        }

        private string _result;
        /// <summary>
        /// フーリエ変換結果を取得します。
        /// </summary>
        public string Result
        {
            get { return this._result; }
            private set { SetProperty(ref this._result, value); }
        }

        private string _amplitude;
        /// <summary>
        /// フーリエ変換結果の振幅を取得します。
        /// </summary>
        public string Amplitude
        {
            get { return this._amplitude; }
            private set { SetProperty(ref this._amplitude, value); }
        }

        private int _length;
        /// <summary>
        /// データ長を取得します。
        /// </summary>
        public int Length
        {
            get { return this._length; }
            private set { SetProperty(ref this._length, value); }
        }

        private DelegateCommand _fftCommand;
        public DelegateCommand FFTCommand
        {
            get
            {
                return this._fftCommand ?? (this._fftCommand = new DelegateCommand(FftProc, _ => !string.IsNullOrWhiteSpace(this.Source)));
            }
        }

        private void FftProc(object _)
        {
            var data = this.Source.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries)
                                  .Select(x => double.Parse(x));
            var data_minus_ave = data.Select(x => x - data.Average())
                                     .ToArray()
                                     .FFT();
            this.Length = data.ToArray().Length;
            this.Result = string.Join("\r\n", data_minus_ave);
            this.Amplitude = string.Join("\r\n", data_minus_ave.Select(x => x.Abs));

        }
    }
}
