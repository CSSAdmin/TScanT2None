//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The IWorkItemController is the interface that WorkItemControllers must implement. 
// It specifies a single method, Run.
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------


namespace TerraScan.Infrastructure.Interface
{
    /// <summary>
    /// Controller used by <see cref="ControlledWorkItem{TController}"/>.
    /// </summary>
    public interface IWorkItemController
    {
        /// <summary>
        /// Called when the controller is ready to run.
        /// </summary>
        void Run();
    }
}