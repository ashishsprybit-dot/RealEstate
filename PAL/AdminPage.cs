using System;

namespace PAL
{
    public class ClsAdminPageProperty
    {
        #region Private Member
        private int _intAdminPageID;
        private string _varAdminPageName;
        #endregion

        #region Public Property
        public int intAdminPageID
        {
            get { return _intAdminPageID; }
            set { _intAdminPageID = value; }
        }
        public string varAdminPageName
        {
            get { return _varAdminPageName; }
            set { _varAdminPageName = value; }
        }
        #endregion

        #region Constructors
        public ClsAdminPageProperty() { }
        public ClsAdminPageProperty(int intAdminPageID, string varAdminPageName)
        {
            this.intAdminPageID = intAdminPageID;
            this.varAdminPageName = varAdminPageName;
        }
        #endregion
    }
}
