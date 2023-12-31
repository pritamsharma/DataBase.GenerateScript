﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase.GenerateScript.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DataBase.GenerateScript.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Insert for Table
        ///
        ///This section creates an Insert script for existing data contained in the table.
        ///
        ///Table Name dropdown contains value of all the tables of the database. Select the table for which you want to create the insert script.
        ///
        ///Column Name Data Grid will contain the columns present in the selected table. It will have checkbox below the name of the column to indicate if it is a part of the filter string.
        ///
        ///Filter String Data Grid contains columns by which the insert script creation has to be fi [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateInsertScriptForTable {
            get {
                return ResourceManager.GetString("CreateInsertScriptForTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This section creates a New Stored Procedure create template having the format given below.
        ///
        ///Table Name dropdown gives the name of table contained in the Database. You can select more then one table name by selecting table name form the dropdown and tabbing out multiple times. On click of Retrieve button Stored Procedure create template for selected table will be created.
        ///
        ///If you are selecting the checkbox Create SP for all the Tables in the Database then new Stored Procedure Create template will be crea [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateSpTemplete {
            get {
                return ResourceManager.GetString("CreateSpTemplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This section creates Alter or Delete-Insert script for already existing Stored Procedures, Views or Function contained in the Database.
        ///
        ///The checkbox Insert-Delete Type Script indicates the type of Script to generate. If it is checked then a script containing both Delete and Insert is created. If it is unchecked then an Alter script is created.
        ///
        ///It further has the following three radio buttons 1) Enter Own Value, 2) Get List from Database and 3) Modified by Date.
        ///
        ///1)	Enter Own Value: If this radio but [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GenerateScript {
            get {
                return ResourceManager.GetString("GenerateScript", resourceCulture);
            }
        }
    }
}
