﻿// ---------------------------------------------------------------------- 
// <copyright file="Actions.cs" company="Izenda">
//  Copyright (c) 2015 Izenda, Inc.                          
//  ALL RIGHTS RESERVED                
//                                                                         
//  The entire contents of this file is protected by U.S. and      
//  International Copyright Laws. Unauthorized reproduction,        
//  reverse-engineering, and distribution of all or any portion of  
//  the code contained in this file is strictly prohibited and may  
//  result in severe civil and criminal penalties and will be      
//  prosecuted to the maximum extent possible under the law.        
//                                                                  
//  RESTRICTIONS                                                    
//                                                                  
//  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES          
//  ARE CONFIDENTIAL AND PROPRIETARY TRADE                          
//  SECRETS OF IZENDA INC. THE REGISTERED DEVELOPER IS  
//  LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    
//  CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                
//                                                                  
//  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      
//  FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        
//  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE      
//  AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  
//  AND PERMISSION FROM IZENDA INC.                      
//                                                                  
//  CONSULT THE END USER LICENSE AGREEMENT(EULA FOR INFORMATION ON  
//  ADDITIONAL RESTRICTIONS.
// </copyright> 
// ----------------------------------------------------------------------


namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models.Permissions.Reports
{
    /// <summary>
    /// Actions
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// Create
        /// </summary>
        public bool Schedule { get; set; }

        /// <summary>
        /// Edit
        /// </summary>
        public bool Email { get; set; }

        /// <summary>
        /// View report history
        /// </summary>
        public bool ViewReportHistory { get; set; }

        /// <summary>
        /// Delete
        /// </summary>
        public bool Del { get; set; }

        /// <summary>
        /// Register for alerts
        /// </summary>
        public bool RegisterForAlerts { get; set; }

        /// <summary>
        /// Print
        /// </summary>
        public bool Print { get; set; }

        /// <summary>
        /// Unarchive report versions
        /// </summary>
        public bool UnarchiveReportVersions { get; set; }

        /// <summary>
        /// Overwrite existing report
        /// </summary>
        public bool OverwriteExistingReport { get; set; }

        /// <summary>
        /// Subscribe
        /// </summary>
        public bool Subscribe { get; set; }

        /// <summary>
        /// Export
        /// </summary>
        public bool Exporting { get; set; }

        /// <summary>
        /// Configure access rights
        /// </summary>
        public bool ConfigureAccessRights { get; set; }

        /// <summary>
        /// Tenant access
        /// </summary>
        public int TenantAccess { get; set; }

    }
}
