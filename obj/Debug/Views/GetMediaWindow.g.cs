﻿#pragma checksum "..\..\..\Views\GetMediaWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C2C19C0C3C93072805D3356AC289036020423E9D470CFF351A733747A6B9F0DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using OverlayImageForWindows.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace OverlayImageForWindows.Views {
    
    
    /// <summary>
    /// GetMediaWindow
    /// </summary>
    public partial class GetMediaWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Images;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Videos;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ImageSW;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid ImageGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer VideoSW;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\GetMediaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid VideoGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OverlayImageForWindows;component/views/getmediawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\GetMediaWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Images = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Views\GetMediaWindow.xaml"
            this.Images.Click += new System.Windows.RoutedEventHandler(this.Images_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Videos = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Views\GetMediaWindow.xaml"
            this.Videos.Click += new System.Windows.RoutedEventHandler(this.Videos_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ImageSW = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.ImageGrid = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 5:
            this.VideoSW = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 6:
            this.VideoGrid = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

