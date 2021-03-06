﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
namespace Helper.Test
{
    /// <summary>
    ///This is a test class for TerraScan.Helper.Properties.Settings and is intended
    ///to contain all TerraScan.Helper.Properties.Settings Unit Tests
    ///</summary>
    [TestClass()]
    public class SettingsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Default
        ///</summary>
        [DeploymentItem("TerraScan.Helper.dll")]
        [TestMethod()]
        public void DefaultTest()
        {
            Helper.Test.TerraScan_Helper_Properties_SettingsAccessor val = null; // TODO: Assign to an appropriate value for the property


            Assert.AreEqual(val, Helper.Test.TerraScan_Helper_Properties_SettingsAccessor.Default, "TerraScan.Helper.Properties.Settings.Default was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TerraScan_Helper_TerraScan_WebService_TerraScanService
        ///</summary>
        [DeploymentItem("TerraScan.Helper.dll")]
        [TestMethod()]
        public void TerraScan_Helper_TerraScan_WebService_TerraScanServiceTest()
        {
            ApplicationSettingsBase target = Helper.Test.TerraScan_Helper_Properties_SettingsAccessor.CreatePrivate();

            string val = null; // TODO: Assign to an appropriate value for the property

            Helper.Test.TerraScan_Helper_Properties_SettingsAccessor accessor = new Helper.Test.TerraScan_Helper_Properties_SettingsAccessor(target);


            Assert.AreEqual(val, accessor.TerraScan_Helper_TerraScan_WebService_TerraScanService, "TerraScan.Helper.Properties.Settings.TerraScan_Helper_TerraScan_WebService_TerraS" +
                    "canService was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }


}
