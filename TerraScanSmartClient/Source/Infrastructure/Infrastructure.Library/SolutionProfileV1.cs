﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.42.
// 
namespace TerraScan.Infrastructure.Library.SolutionProfileV1
{
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/pag/cab-profile")]
    [System.Xml.Serialization.XmlRootAttribute("SolutionProfile", Namespace = "http://schemas.microsoft.com/pag/cab-profile", IsNullable = false)]
    public partial class SolutionProfileElement
    {

        private ModuleInfoElement[] modulesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ModuleInfo", IsNullable = false)]
        public ModuleInfoElement[] Modules
        {
            get
            {
                return this.modulesField;
            }
            set
            {
                this.modulesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/pag/cab-profile")]
    public partial class ModuleInfoElement
    {

        private RoleElement[] rolesField;

        private string assemblyFileField;

        private string updateLocationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Role", IsNullable = false)]
        public RoleElement[] Roles
        {
            get
            {
                return this.rolesField;
            }
            set
            {
                this.rolesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AssemblyFile
        {
            get
            {
                return this.assemblyFileField;
            }
            set
            {
                this.assemblyFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string UpdateLocation
        {
            get
            {
                return this.updateLocationField;
            }
            set
            {
                this.updateLocationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/pag/cab-profile")]
    public partial class RoleElement
    {

        private string allowField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Allow
        {
            get
            {
                return this.allowField;
            }
            set
            {
                this.allowField = value;
            }
        }
    }
}
