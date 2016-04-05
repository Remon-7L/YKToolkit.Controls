﻿namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Threading;

    /// <summary>
    /// ファイルツリーを表します。
    /// </summary>
    [TemplatePart(Name = PART_MainTree, Type = typeof(TreeView))]
    public class FileTreeView : Control
    {
        #region TemplatePart
        private const string PART_MainTree = "PART_MainTree";
        private const string PART_ItemButton = "PART_ItemButton";

        private TreeView _mainTree;
        private TreeView MainTree
        {
            get { return _mainTree; }
            set
            {
                if (_mainTree != null)
                {
                    _mainTree.ItemsSource = null;
                    _mainTree.SelectedItemChanged -= MainTree_SelectedItemChanged;
                    _mainTree.MouseDoubleClick -= MainTree_MouseDoubleClick;
                }
                _mainTree = value;
                if (_mainTree != null)
                {
                    _mainTree.SelectedItemChanged += MainTree_SelectedItemChanged;
                    _mainTree.MouseDoubleClick += MainTree_MouseDoubleClick;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MainTree = this.Template.FindName(PART_MainTree, this) as TreeView;

            Initilization();
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static FileTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FileTreeView), new FrameworkPropertyMetadata(typeof(FileTreeView)));
        }
        #endregion コンストラクタ

        #region SelectedPath 依存関係プロパティ
        ///// <summary>
        ///// SelectedPath 依存関係プロパティキーの定義
        ///// </summary>
        //private static readonly DependencyPropertyKey SelectedPathPropertyKey = DependencyProperty.RegisterReadOnly("SelectedPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null));

        /// <summary>
        /// SelectedPath 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register("SelectedPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null));
        //public static readonly DependencyProperty SelectedPathProperty = SelectedPathPropertyKey.DependencyProperty;

        /// <summary>
        /// 選択されているパスを取得または設定します。
        /// </summary>
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }
        #endregion SelectedPath 依存関係プロパティ

        #region SearchPattern 依存関係プロパティ
        /// <summary>
        /// SearchPattern 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty SearchPatternProperty = DependencyProperty.Register("SearchPattern", typeof(string), typeof(FileTreeView), new PropertyMetadata(null, OnSearchPatternPropertyChanged));

        /// <summary>
        /// ファイル検索のための検索パターンを取得または設定します。
        /// </summary>
        public string SearchPattern
        {
            get { return (string)GetValue(SearchPatternProperty); }
            set { SetValue(SearchPatternProperty, value); }
        }

        /// <summary>
        /// SearchPattern プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSearchPatternPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion SearchPattern 依存関係プロパティ

        #region IsFileEnabled 依存関係プロパティ
        /// <summary>
        /// IsFileEnabled 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty IsFileEnabledProperty = DependencyProperty.Register("IsFileEnabled", typeof(bool), typeof(FileTreeView), new PropertyMetadata(false, OnIsFileEnabledPropertyChanged));

        /// <summary>
        /// ファイルを表示するかどうかを取得または設定します。
        /// </summary>
        public bool IsFileEnabled
        {
            get { return (bool)GetValue(IsFileEnabledProperty); }
            set { SetValue(IsFileEnabledProperty, value); }
        }

        /// <summary>
        /// IsFileEnabled プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsFileEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion IsFileEnabled 依存関係プロパティ

        #region RootPath 依存関係プロパティ
        /// <summary>
        /// RootPath 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty RootPathProperty = DependencyProperty.Register("RootPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null, OnRootPathPropertyChanged));

        /// <summary>
        /// ルートノードのパスを取得または設定します。
        /// </summary>
        public string RootPath
        {
            get { return (string)GetValue(RootPathProperty); }
            set { SetValue(RootPathProperty, value); }
        }

        /// <summary>
        /// RootPath プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnRootPathPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion RootPath 依存関係プロパティ

        #region IsSynchronizeFileSystem 依存関係プロパティ
        /// <summary>
        /// IsSynchronizeFileSystem 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsSynchronizeFileSystemProperty = DependencyProperty.Register("IsSynchronizeFileSystem", typeof(bool), typeof(FileTreeView), new PropertyMetadata(true, OnIsSynchronizeFileSystemPropertyChanged));

        /// <summary>
        /// ファイルシステムを監視するかどうかを取得または設定します。
        /// </summary>
        public bool IsSynchronizeFileSystem
        {
            get { return (bool)GetValue(IsSynchronizeFileSystemProperty); }
            set { SetValue(IsSynchronizeFileSystemProperty, value); }
        }

        /// <summary>
        /// IsSynchronizeFileSystem プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsSynchronizeFileSystemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion IsSynchronizeFileSystem 依存関係プロパティ

        /// <summary>
        /// アイテムをダブルクリックしたときに発生します。
        /// </summary>
        public event EventHandler<FileTreeViewItemDoubleClickEventArgs> ItemDoubleClick;

        #region イベントハンドラ
        /// <summary>
        /// MainTree SelectedItemChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void MainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as FileTreeViewItem;
            this.SelectedPath = item != null ? item.FullPath : null;
        }

        /// <summary>
        /// MainTree MouseDoubleClick イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void MainTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement).DataContext as FileTreeViewItem;
            var h = this.ItemDoubleClick;
            if (h != null) h(this, new FileTreeViewItemDoubleClickEventArgs(item != null ? item.FullPath : ""));
        }
        #endregion イベントハンドラ

        #region private フィールド
        /// <summary>
        /// マイコンピュータを表すノード
        /// </summary>
        private FileTreeViewItem _myComputer;

        /// <summary>
        /// ファイルシステム監視
        /// </summary>
        List<FileSystemWatcher> _watchers;
        #endregion private フィールド

        /// <summary>
        /// 初期化をおこないます。
        /// </summary>
        private void Initilization()
        {
            // テンプレート適用前は処理しない
            if (this.MainTree == null)
                return;

            var w = Window.GetWindow(this);
            if (w == null)
                return;

            ObservableCollection<FileTreeViewItem> rootCollection = null;
            if (Directory.Exists(this.RootPath))
            {
                var root = new FileTreeViewItem(this.RootPath, this.SearchPattern, this.IsFileEnabled) { IsExpanded = true };
                rootCollection = new ObservableCollection<FileTreeViewItem>()
                {
                    root,
                };
            }
            else
            {
                var handle = (new WindowInteropHelper(w)).Handle;

                #region マイコンピュータ
                _myComputer = new FileTreeViewItem("", this.SearchPattern, this.IsFileEnabled);
                _myComputer.Name = "マイコンピュータ";
                _myComputer.IsExpanded = true;
                _myComputer.IsExpanded = false;
                _myComputer.BitmapByteArray = Shell32.ShellInfo.GetSpecialIconByByteArray(handle, Shell32.ShellInfo.FolderID.MyComputer);
                _myComputer.Children = new ObservableCollection<FileTreeViewItem>();

                var infoArray = DriveInfo.GetDrives();
                foreach (var info in infoArray)
                {
                    if (info.IsReady)
                    {
                        (_myComputer.Children as ObservableCollection<FileTreeViewItem>).Add(new FileTreeViewItem(info.RootDirectory.FullName, this.SearchPattern, this.IsFileEnabled));
                    }
                }
                #endregion マイコンピュータ

                #region マイドキュメント
                var myDocumentPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var myDocument = new FileTreeViewItem(myDocumentPath, this.SearchPattern, this.IsFileEnabled);
                #endregion マイドキュメント

                #region デスクトップ
                var desktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var desktop = new FileTreeViewItem(desktopPath, this.SearchPattern, this.IsFileEnabled);
                desktop.Name = "デスクトップ";
                desktop.IsExpanded = true;

                (desktop.Children as Collection<FileTreeViewItem>).Insert(0, _myComputer);
                (desktop.Children as Collection<FileTreeViewItem>).Insert(1, myDocument);
                #endregion デスクトップ

                rootCollection = new ObservableCollection<FileTreeViewItem>()
                {
                    desktop,
                };
            }

            this.MainTree.ItemsSource = rootCollection ?? new ObservableCollection<FileTreeViewItem>();

            #region ファイルシステムの監視
            if (this._watchers != null)
            {
                foreach (var watcher in this._watchers)
                {
                    watcher.EnableRaisingEvents = false;
                    watcher.Changed -= OnFileSystemUpdated;
                    watcher.Created -= OnFileSystemUpdated;
                    watcher.Deleted -= OnFileSystemUpdated;
                    watcher.Renamed -= OnFileSystemUpdated;
                    watcher.Dispose();
                }
                this._watchers.Clear();
            }
            else
            {
                this._watchers = new List<FileSystemWatcher>();
            }
            if (this.IsSynchronizeFileSystem)
            {
                _dispatcher = w.Dispatcher;
                foreach (var item in rootCollection)
                {
                    if (!string.IsNullOrWhiteSpace(item.FullPath) && Directory.Exists(item.FullPath))
                    {
                        var watcher = new FileSystemWatcher()
                        {
                            Path = item.FullPath,
                            Filter = "",
                            NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
                            IncludeSubdirectories = true,
                            SynchronizingObject = w as System.ComponentModel.ISynchronizeInvoke,
                        };
                        watcher.Changed += OnFileSystemUpdated;
                        watcher.Created += OnFileSystemUpdated;
                        watcher.Deleted += OnFileSystemUpdated;
                        watcher.Renamed += OnFileSystemUpdated;
                        watcher.EnableRaisingEvents = true;
                        this._watchers.Add(watcher);
                    }
                }
            }
            else
            {
                this._watchers = null;
                _dispatcher = null;
            }
            #endregion ファイルシステムの監視
        }

        /// <summary>
        /// ファイルシステム監視スレッドからUIスレッドを触るため
        /// </summary>
        private static Dispatcher _dispatcher;

        /// <summary>
        /// ファイルシステム監視イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnFileSystemUpdated(object sender, FileSystemEventArgs e)
        {
            if (_dispatcher == null)
                return;

            _dispatcher.BeginInvoke(new Action(() =>
            {
                var dir = Path.GetDirectoryName(e.FullPath);
                var items = this.MainTree.ItemsSource as ObservableCollection<FileTreeViewItem>;
                var item = items.FirstOrDefault(x => x.FullPath == dir);
                if (item != null)
                {
                    var newItem = new FileTreeViewItem(dir, this.SearchPattern, this.IsFileEnabled) { IsExpanded = item.IsExpanded };
                    var index = items.IndexOf(item);
                    items.RemoveAt(index);
                    items.Insert(index, newItem);
                }
                System.Diagnostics.Trace.WriteLine("ファイルシステムが更新されましたぁ！");
            }), DispatcherPriority.Normal);
        }
    }
}
