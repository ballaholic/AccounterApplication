﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccounterApplication.Common.LocalizationResources.Shared.Helpers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GreetingHelperResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GreetingHelperResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AccounterApplication.Common.LocalizationResources.Shared.Helpers.GreetingHelperRe" +
                            "sources", typeof(GreetingHelperResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добър ден.
        /// </summary>
        public static string GoodAfternoon {
            get {
                return ResourceManager.GetString("GoodAfternoon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добър вечер.
        /// </summary>
        public static string GoodEvening {
            get {
                return ResourceManager.GetString("GoodEvening", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добро утро.
        /// </summary>
        public static string GoodMorning {
            get {
                return ResourceManager.GetString("GoodMorning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Здравей.
        /// </summary>
        public static string Hello {
            get {
                return ResourceManager.GetString("Hello", resourceCulture);
            }
        }
    }
}
