#pragma checksum "..\..\Shipping.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4FBE2AB93FE0907FFE5EFE30E7C807E415C04A9A2E04ED5C2853B1AC3CB943CD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Course4;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Course4 {
    
    
    /// <summary>
    /// Shipping
    /// </summary>
    public partial class Shipping : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 54 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_shipping;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_back;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_add;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_upd;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_del;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dp_shippingdate;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_address;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Shipping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_supply;
        
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
            System.Uri resourceLocater = new System.Uri("/Course4;component/shipping.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Shipping.xaml"
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
            this.dg_shipping = ((System.Windows.Controls.DataGrid)(target));
            
            #line 54 "..\..\Shipping.xaml"
            this.dg_shipping.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg_shipping_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_back = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\Shipping.xaml"
            this.btn_back.Click += new System.Windows.RoutedEventHandler(this.btn_back_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_add = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\Shipping.xaml"
            this.btn_add.Click += new System.Windows.RoutedEventHandler(this.btn_add_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_upd = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\Shipping.xaml"
            this.btn_upd.Click += new System.Windows.RoutedEventHandler(this.btn_upd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_del = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\Shipping.xaml"
            this.btn_del.Click += new System.Windows.RoutedEventHandler(this.btn_del_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dp_shippingdate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.cb_address = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.cb_supply = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

