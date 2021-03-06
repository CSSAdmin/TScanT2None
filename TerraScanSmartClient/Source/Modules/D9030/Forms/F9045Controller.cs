﻿//----------------------------------------------------------------------------------
// <copyright file="F9045Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			  Author		       Description
// ----------     ---------		       ---------------------------------------------
// 13 Oct 2011    P. Manoj Kumar           Created
//*********************************************************************************/




namespace D9030
{
    #region NameSpaces
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    #endregion NameSpaces

    /// <summary>
    /// Class F9045Controller
    /// </summary>
    public class F9045Controller : Controller
    {
        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9045WorkItem WorkItem
        {
            get { return base.WorkItem as F9045WorkItem; }
        }
    }
}
