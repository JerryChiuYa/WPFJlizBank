﻿#pragma checksum "..\..\Register.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1C9C469BFF1B9918FCFB1D9CD27AEABB92A955044F1DF0A83712BB9637D0D68A"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using BankLibrary.Services;
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
using WPFJlizBank;


namespace WPFJlizBank {
    
    
    /// <summary>
    /// Register
    /// </summary>
    public partial class Register : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 80 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel BasicInfo;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IdNum;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel bankInfoData;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox BankList;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox password;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox password2;
        
        #line default
        #line hidden
        
        
        #line 221 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PwdMsg;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveData;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFJlizBank;component/register.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Register.xaml"
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
            this.BasicInfo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            
            #line 91 "..\..\Register.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Check_Error));
            
            #line default
            #line hidden
            return;
            case 3:
            this.IdNum = ((System.Windows.Controls.TextBox)(target));
            
            #line 106 "..\..\Register.xaml"
            this.IdNum.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Check_Error));
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 135 "..\..\Register.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Check_Error));
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 155 "..\..\Register.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Check_Error));
            
            #line default
            #line hidden
            return;
            case 6:
            this.bankInfoData = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.BankList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 183 "..\..\Register.xaml"
            this.BankList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.BankList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 197 "..\..\Register.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Check_Error));
            
            #line default
            #line hidden
            return;
            case 9:
            this.password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 212 "..\..\Register.xaml"
            this.password.PasswordChanged += new System.Windows.RoutedEventHandler(this.password_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.password2 = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 220 "..\..\Register.xaml"
            this.password2.PasswordChanged += new System.Windows.RoutedEventHandler(this.password2_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.PwdMsg = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.SaveData = ((System.Windows.Controls.Button)(target));
            
            #line 237 "..\..\Register.xaml"
            this.SaveData.Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 239 "..\..\Register.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

