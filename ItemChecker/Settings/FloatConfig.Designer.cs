﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItemChecker.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class FloatConfig : global::System.Configuration.ApplicationSettingsBase {
        
        private static FloatConfig defaultInstance = ((FloatConfig)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new FloatConfig())));
        
        public static FloatConfig Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public decimal maxFloatPrecent {
            get {
                return ((decimal)(this["maxFloatPrecent"]));
            }
            set {
                this["maxFloatPrecent"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("40")]
        public int countGetItems {
            get {
                return ((int)(this["countGetItems"]));
            }
            set {
                this["countGetItems"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int priceCompare {
            get {
                return ((int)(this["priceCompare"]));
            }
            set {
                this["priceCompare"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.001")]
        public decimal maxFloatValue_FN {
            get {
                return ((decimal)(this["maxFloatValue_FN"]));
            }
            set {
                this["maxFloatValue_FN"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.080")]
        public decimal maxFloatValue_MW {
            get {
                return ((decimal)(this["maxFloatValue_MW"]));
            }
            set {
                this["maxFloatValue_MW"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.175")]
        public decimal maxFloatValue_FT {
            get {
                return ((decimal)(this["maxFloatValue_FT"]));
            }
            set {
                this["maxFloatValue_FT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.400")]
        public decimal maxFloatValue_WW {
            get {
                return ((decimal)(this["maxFloatValue_WW"]));
            }
            set {
                this["maxFloatValue_WW"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.500")]
        public decimal maxFloatValue_BS {
            get {
                return ((decimal)(this["maxFloatValue_BS"]));
            }
            set {
                this["maxFloatValue_BS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string floatList {
            get {
                return ((string)(this["floatList"]));
            }
            set {
                this["floatList"] = value;
            }
        }
    }
}
