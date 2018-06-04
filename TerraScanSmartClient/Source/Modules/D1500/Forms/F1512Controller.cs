//--------------------------------------------------------------------------------------------
// <copyright file="F1512Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property to Load WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 August 06        KRISHNA A       Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1512Controller class file
    /// </summary>
    public class F1512Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1512WorkItem WorkItem
        {
            get { return base.WorkItem as F1512WorkItem; }
        }
    }
}
