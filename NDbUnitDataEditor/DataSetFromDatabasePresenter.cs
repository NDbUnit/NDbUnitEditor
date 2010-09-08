using System;
using System.Data;


namespace NDbUnitDataEditor
{

    public class DataSetFromDatabasePresenter
    {
        IDataSetFromDatabaseView _dataSetFromDatabase;

        private NDbUnitFacade _nDbUnit;

        /// <summary>
        /// Initializes a new instance of the DataSetFromDatabasePresenter class.
        /// </summary>
        /// <param name="nDbUnitManager"></param>
        /// <param name="dataSetFromDatabase"></param>
        public DataSetFromDatabasePresenter(IDataSetFromDatabaseView dataSetFromDatabase, NDbUnitFacade nDbUnitManager)
        {
            _nDbUnit = nDbUnitManager;
            _dataSetFromDatabase = dataSetFromDatabase;
            _dataSetFromDatabase.GetDataSetFromDatabase += GetDataSetFromDatabase;
            _dataSetFromDatabase.PutDataSetToDatabase += PutDataSetToDatabase;
            _dataSetFromDatabase.TestDatabaseConnection += TestDatabaseConnection;
            _dataSetFromDatabase.SelectDatabaseType += SetDatabaseType;
            FillPresenterWithSupportedDatabaseTypesList();
        }

        public string DatabaseConnectionString { get; set; }

        public DatabaseClientType DatabaseType { get; set; }

        public int DatabaseTypeSelectedIndex { get; set; }

        public bool DataFileHasChanged { get; set; }

        public string DataFilePath { get; set; }

        public DataSet DataSet { get; private set; }

        public bool OperationResult { get; set; }

        public bool SchemaFileHasChanged { get; set; }

        public string SchemaFilePath { get; set; }

        public void Start()
        {
            _dataSetFromDatabase.DatabaseConnectionString = DatabaseConnectionString;
            _dataSetFromDatabase.DatabaseTypeSelectedIndex = DatabaseTypeSelectedIndex;
            _dataSetFromDatabase.Run();
        }

        private void FillPresenterWithSupportedDatabaseTypesList()
        {
            _dataSetFromDatabase.DatabaseConnectionTypes = _nDbUnit.GetSupportedClientTypesList();
        }

        private void GetDataSetFromDatabase()
        {
            try
            {
                var selectedFilename = _dataSetFromDatabase.SelectFile(DataFilePath, "XML Data Files (*.xml)|*.xml");

                if (!string.IsNullOrEmpty(selectedFilename))
                {
                    SetupNDbUnit();

                    DataSet = _nDbUnit.GetDataSetFromDatabase(SchemaFilePath);
                    _dataSetFromDatabase.GetDataSetFromDatabaseResult = true;
                    OperationResult = true;
                    DataFileHasChanged = true;
                    DataFilePath = selectedFilename;

                    DataSet.WriteXml(selectedFilename);
                }

            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.GetDataSetFromDatabaseResult = false;
                OperationResult = false;
            }

        }


        private void PutDataSetToDatabase()
        {
            try
            {
                SetupNDbUnit();

                _nDbUnit.PutDataSetToDatabase(SchemaFilePath, DataFilePath);
                _dataSetFromDatabase.PutDataSetToDatabaseResult = true;
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.PutDataSetToDatabaseResult = false;
            }

        }

        private void SetDatabaseType()
        {
            DatabaseType = _dataSetFromDatabase.SelectedDatabaseConnectionType;
        }

        private void SetupNDbUnit()
        {
            _nDbUnit.Setup(DatabaseConnectionString, DatabaseType);
        }

        public void SetDatabaseType(string databaseTypeName)
        {
            if (String.IsNullOrEmpty(databaseTypeName))
                return;
            DatabaseTypeSelectedIndex = (int)Enum.Parse(typeof(DatabaseClientType), databaseTypeName);
        }

        public string DatabaseTypeName
        {
            get { return DatabaseType.ToString(); }
        }

        private void TestDatabaseConnection()
        {
            try
            {
                DatabaseConnectionString = _dataSetFromDatabase.DatabaseConnectionString;
                DatabaseTypeSelectedIndex = _dataSetFromDatabase.DatabaseTypeSelectedIndex;

                SetupNDbUnit();

                _dataSetFromDatabase.ConnectionTestResult = _nDbUnit.TestConnection();
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.ConnectionTestResult = false;
            }

        }

    }
}
