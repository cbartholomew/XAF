﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xwSearchLib.Configuration {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class xwSearch : global::System.Configuration.ApplicationSettingsBase {
        
        private static xwSearch defaultInstance = ((xwSearch)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new xwSearch())));
        
        public static xwSearch Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("conv.data.verb")]
        public string VERB {
            get {
                return ((string)(this["VERB"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("conv.data.noun")]
        public string NOUN {
            get {
                return ((string)(this["NOUN"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("conv.data.adj")]
        public string ADVERB {
            get {
                return ((string)(this["ADVERB"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("xwing.data.verb")]
        public string XW_VERB_FILE_NAME {
            get {
                return ((string)(this["XW_VERB_FILE_NAME"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("xwing.data.noun")]
        public string XW_NOUN_FILE_NAME {
            get {
                return ((string)(this["XW_NOUN_FILE_NAME"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("xwing.data.adv")]
        public string XW_ADVERB_FILE_NAME {
            get {
                return ((string)(this["XW_ADVERB_FILE_NAME"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Christopher\\Source\\Repos\\XWingAbilityFinder\\xwSearchLib\\Template\\X-Wing " +
            "Pilots, Upgrades and FAQ.xlsx")]
        public string SERVER_TEMPLATE {
            get {
                return ((string)(this["SERVER_TEMPLATE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Christopher\\Source\\Repos\\XWingAbilityFinder\\xwSearchLib\\Dictionary\\")]
        public string LOCAL_OUTPUT {
            get {
                return ((string)(this["LOCAL_OUTPUT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\\\Hosting\\\\4173543\\\\html\\\\aoitsolutions.com\\\\xwing\\\\bin\\\\Dictionary\\\\")]
        public string DICTIONARY_OUTPUT {
            get {
                return ((string)(this["DICTIONARY_OUTPUT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pilots.in")]
        public string PILOT_FILE {
            get {
                return ((string)(this["PILOT_FILE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("upgrades.in")]
        public string UPGRADE_FILE {
            get {
                return ((string)(this["UPGRADE_FILE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool IS_LOCAL {
            get {
                return ((bool)(this["IS_LOCAL"]));
            }
        }
    }
}
