//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The WebServiceCatalogModuleInfoStore class provides an implementation of IModuleInfoStore
// that will retrieve the profile catalog from a webservice. It depends on the IProfileCatalogService
// to work. Using this IModuleInfoStore is possible to retrieve a profile catalog from a central location
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using TerraScan.Infrastructure.Library.Services;

namespace TerraScan.Infrastructure.Library.Services
{
    public class WebServiceCatalogModuleInfoStore : IModuleInfoStore
    {
        private string _catalogUrl;
        private string[] _roles;
        private IProfileCatalogService _catalogService;

        [InjectionConstructor]
        public WebServiceCatalogModuleInfoStore([ServiceDependency] IProfileCatalogService catalogService)
        {
            _catalogService = catalogService;
            _catalogUrl = "http://localhost:54092/profilecatalogservices/profilecatalog.asmx";

        }

        public string CatalogUrl
        {
            get { return _catalogUrl; }
            set
            {
                Guard.ArgumentNotNullOrEmptyString(value, "Catalog Url");

                _catalogUrl = value;
            }
        }

        public string[] Roles
        {
            get { return _roles; }
            set
            {
                Guard.ArgumentNotNull(value, "Roles");

                _roles = value;
            }
        }

        #region IModuleInfoStore Members

        public string GetModuleListXml()
        {
            try
            {
                _catalogService.Url = _catalogUrl;
                return _catalogService.GetProfileCatalog(_roles);

            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
