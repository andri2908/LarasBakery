﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace AlphaSoft
{
    class Data_Access
    {
        private string userName = "SYS_POS_ADMIN";
        private string password = "pass123";
        private string databaseName = "SYS_POS_LARASBAKERY";
        private string serverDatabaseName = "SYS_POS_LARASBAKERY_PABRIK";
        private string SSDatabaseName = "SYS_POS_LARASBAKERY_SS";
        public const int LOCAL_SERVER = 0;
        public const int HQ_SERVER = 1;
        public const int BRANCH_SERVER = 2;
        public const int SS_SERVER = 3;

        private MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
        private MySqlConnection HQ_conn = new MySql.Data.MySqlClient.MySqlConnection();
        private MySqlConnection Branch_conn = new MySql.Data.MySqlClient.MySqlConnection();
        private MySqlConnection SS_conn = new MySql.Data.MySqlClient.MySqlConnection();

        private MySqlTransaction myTrans;
        private MySqlCommand myTransCommand;
        public bool connectToLive = true;
        private static string configFileConnectionString = "";
        private static string HQconfigFileConnectionString = "";
        private static string BranchconfigFileConnectionString = "";
        private static string SSconfigFileConnectionString = "";

        private static string ipServer = "";
        //private string myConnectionString = "server=127.0.0.1;uid=SYS_POS_ADMIN;pwd=pass123;database=SYS_POS;";
        private string configFile = "pos.cfg";
        private MySqlConnection transConnection;

        public bool setIPServer()
        {
            string s = "";

            if (configFileConnectionString.Length <= 0)
            {
                if (File.Exists(configFile))
                {
                    using (StreamReader sr = File.OpenText(configFile))
                    {
                        if ((s = sr.ReadLine()) != null)
                        {
                            if (globalFeatureList.COMPILED_AS_PABRIK_APP == 1)
                                configFileConnectionString = "server=" + s + ";uid=" + userName + ";pwd=" + password + ";database=" + serverDatabaseName + ";";
                            else if (globalFeatureList.COMPILED_AS_SS_APP == 1)
                                configFileConnectionString = "server=" + s + ";uid=" + userName + ";pwd=" + password + ";database=" + SSDatabaseName + ";";
                            else
                                configFileConnectionString = "server=" + s + ";uid=" + userName + ";pwd=" + password + ";database=" + databaseName + ";";

                            ipServer = s;
                        }
                    }
                    return true;
                }
            }
            else
                return true;
            
            return false;
        }

        public string getIPServer()
        {
            return ipServer;
        }

        public string getHQ_IPServer()
        {
            string HQ_Ip = "";
            int dataExist = 0;

            dataExist = Convert.ToInt32(getDataSingleValue("SELECT COUNT(1) FROM SYS_CONFIG WHERE ID = 2"));

            if (dataExist > 0)
                HQ_Ip = getDataSingleValue("SELECT IFNULL(HQ_IP4, '') FROM SYS_CONFIG WHERE ID = 2").ToString();

            return HQ_Ip;
        }

        public string getSS_IPServer()
        {
            string HQ_Ip = "";
            int dataExist = 0;

            dataExist = Convert.ToInt32(getDataSingleValue("SELECT COUNT(1) FROM SYS_CONFIG WHERE ID = 2"));

            if (dataExist > 0)
                HQ_Ip = getDataSingleValue("SELECT IFNULL(SERVER_IP4, '') FROM SYS_CONFIG WHERE ID = 2").ToString();

            return HQ_Ip;
        }

        public bool HQ_mySQLConnect(bool connectToSyncServer = false)
        {
            string HQconnectionString = "";
            string HQ_IP = getHQ_IPServer(); 

            if (HQ_IP.Length > 0)
            { 
                if (!connectToSyncServer)
                    HQconnectionString = "server=" + HQ_IP + ";uid="+userName+";pwd="+password+";database="+databaseName+";";
                else
                    HQconnectionString = "server=" + HQ_IP + ";uid=" + userName + ";pwd=" + password + ";database=" + serverDatabaseName + ";";

                try
                {
                    HQ_conn.ConnectionString = HQconnectionString;//myConnectionString;
                    HQ_conn.Open();

                    HQconfigFileConnectionString = HQconnectionString;
                    return true;
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    //MessageBox.Show(ex.Message);
                    return false;
                }
            }

            return false;
        }

        public void HQ_mySqlClose()
        {
            if (null != HQ_conn)
            {
                HQ_conn.Close();
            }
        }

        public bool SS_mySQLConnect(bool connectToSyncServer = false)
        {
            string SSconnectionString = "";
            string SS_IP = getSS_IPServer();

            if (SS_IP.Length > 0)
            {
                if (!connectToSyncServer)
                    SSconnectionString = "server=" + SS_IP + ";uid=" + userName + ";pwd=" + password + ";database=" + databaseName + ";";
                else
                    SSconnectionString = "server=" + SS_IP + ";uid=" + userName + ";pwd=" + password + ";database=" + SSDatabaseName + ";";

                try
                {
                    SS_conn.ConnectionString = SSconnectionString;//myConnectionString;
                    SS_conn.Open();

                    SSconfigFileConnectionString = SSconnectionString;
                    return true;
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    //MessageBox.Show(ex.Message);
                    return false;
                }
            }

            return false;
        }

        public void SS_mySqlClose()
        {
            if (null != SS_conn)
            {
                SS_conn.Close();
            }
        }

        public string getBranch_IPServer(int branchID)
        {
            string Branch_Ip = "";

            Branch_Ip = getDataSingleValue("SELECT IFNULL(BRANCH_IP4, '') FROM MASTER_BRANCH WHERE BRANCH_ID = "+branchID).ToString();

            return Branch_Ip;
        }

        public bool Branch_mySQLConnect(int branchID, string ipBranch = "")
        {
            string BranchconnectionString = "";
            string Branch_IP = "";

            if (branchID > 0)
                Branch_IP = getBranch_IPServer(branchID);
            else
                Branch_IP = ipBranch;

            if (Branch_IP.Length > 0)
            {
                if (branchID > 0)
                    BranchconnectionString = "server=" + Branch_IP + ";uid=" + userName + ";pwd=" + password + ";database=" + databaseName + ";";
                else
                    BranchconnectionString = "server=" + Branch_IP + ";uid=" + userName + ";pwd=" + password + ";database=" + serverDatabaseName + ";";

                try
                {
                    Branch_conn.ConnectionString = BranchconnectionString;//myConnectionString;
                    Branch_conn.Open();

                    BranchconfigFileConnectionString = BranchconnectionString;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public void Branch_mySqlClose()
        {
            if (null != Branch_conn)
            {
                Branch_conn.Close();
            }
        }

        public void setConfigFileConnectionString(string ipAddress)
        {            
            configFileConnectionString = "server=" + ipAddress + ";uid="+userName+";pwd="+password+";database="+databaseName+";";
            ipServer = ipAddress;
        }

        public bool testConfigConnectionString(ref MySqlException returnEx)
        {
            try
            {
                conn.ConnectionString = configFileConnectionString;
                conn.Open();
                conn.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                returnEx = ex;
                return false;
            }
        }

        public MySqlConnection getDSConn()
        {
            return conn;
        }

        public bool firstMySqlConnect()
        {
            try
            {
                if (setIPServer())
                {
                    conn.ConnectionString = configFileConnectionString;//myConnectionString;
                    conn.Open();

                    return true;
                }
                else
                    return false;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return false;
            }
        }

        public bool mySqlConnect()
        {     
            try
            {
                    conn.ConnectionString = configFileConnectionString;//myConnectionString;
                    conn.Open();

                    return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return false;
            }
        }

        public void mySqlClose()
        {
            if (null != conn)
            { 
                conn.Close();
            }
        }

        public Boolean writeXML(string sqlCommand, string filename = "")
        {
            Boolean rslt = false;
            DataSet myData = new DataSet();
            MySqlCommand cmd;
            MySqlDataAdapter myAdapter;
            
            cmd = new MySqlCommand();
            myAdapter = new MySqlDataAdapter();
            if (conn.State.ToString() != "Open")
                mySqlConnect();
            //cmd = new MySqlCommand(sqlCommand, conn);

            try
            {
                cmd.CommandText = sqlCommand;
                cmd.Connection = conn;

                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData);
                if (filename.Equals(""))
                {
                    filename = "\\dataset.xml";
                } else
                {
                    filename = "\\" + filename;
                }
                string appPath = Directory.GetCurrentDirectory() + filename;
                myData.WriteXml(@appPath, XmlWriteMode.WriteSchema);
                rslt = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                rslt = false;
            }
            return rslt;
        }

        public MySqlDataReader getData(string sqlCommand, bool isHQConnection = false, bool isBranchConnection = false)
        {
            MySqlCommand cmd = null;
            MySqlDataReader rdr;

            if (isHQConnection)
            {
                if (HQ_conn.State.ToString() != "Open")
                    HQ_mySQLConnect();

                cmd = new MySqlCommand(sqlCommand, HQ_conn);
            }
            else if (isBranchConnection)
            {
                cmd = new MySqlCommand(sqlCommand, Branch_conn);
            }
            else
            {
                if (conn.State.ToString() != "Open")
                    mySqlConnect();

                cmd = new MySqlCommand(sqlCommand, conn);
            }

            rdr = cmd.ExecuteReader();

            return rdr;
        }

        public object getDataSingleValue(string sqlCommand, int serverToConnect = LOCAL_SERVER)
        {
            MySqlCommand cmd = null;
            switch (serverToConnect)
            {
                case LOCAL_SERVER:
                    if (conn.State.ToString() != "Open")
                        mySqlConnect();

                    cmd = new MySqlCommand(sqlCommand, conn);
                    break;

                case BRANCH_SERVER:
                    cmd = new MySqlCommand(sqlCommand, Branch_conn);
                    break;
            }
            object result = cmd.ExecuteScalar();

            return result;
        }

        public bool executeNonQueryCommand(string sqlCommand)
        {
            bool retVal = true;
            //myConnectionString = Properties.Settings.Default.connectionString;
            int temp;
           
            try
            {
                myTransCommand.CommandText = sqlCommand;
                if (myTransCommand.Connection.State.ToString() != "Open")
                    myTransCommand.Connection.Open();

                temp = myTransCommand.ExecuteNonQuery();

                retVal = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public bool executeNonQueryCommand(string sqlCommand, ref MySqlException returnEx)
        {
            bool retVal = true;
            //myConnectionString = Properties.Settings.Default.connectionString;
            int temp;

            try
            {
                myTransCommand.CommandText = sqlCommand;
                if (myTransCommand.Connection.State.ToString() != "Open")
                    myTransCommand.Connection.Open();

                temp = myTransCommand.ExecuteNonQuery();

                retVal = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                retVal = false;
                returnEx = ex;
            }

            return retVal;
        }

        public bool dataExist(string sqlCommand)
        {
            if (conn.State.ToString() != "Open")
                mySqlConnect();

            MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
            
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    return true;
                }

                return false;
            }
        }
    
        public void beginTransaction(int serverToConnect = LOCAL_SERVER)
        {
            //myConnectionString = Properties.Settings.Default.connectionString;

            //transConnection = new MySqlConnection(myConnectionString);
            //setConfigFileConnectionFromTable();
            if (serverToConnect == LOCAL_SERVER)
                transConnection = new MySqlConnection(configFileConnectionString);
            else if (serverToConnect == HQ_SERVER)
                transConnection = new MySqlConnection(HQconfigFileConnectionString);
            else if (serverToConnect == BRANCH_SERVER)
                transConnection = new MySqlConnection(BranchconfigFileConnectionString);
            else if (serverToConnect == SS_SERVER)
                transConnection = new MySqlConnection(SSconfigFileConnectionString);

            transConnection.Open();

            myTransCommand = transConnection.CreateCommand();

            // Start a local transaction
            myTrans = transConnection.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myTransCommand.Connection = transConnection;
            myTransCommand.Transaction = myTrans;
        }

        public void rollBack(ref MySqlException returnEx)
        {
            try 
            { 
                myTrans.Rollback();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                returnEx = ex;
            }
        }

        public void rollBack()
        {
                myTrans.Rollback();
        }

        public MySqlConnection getMyTransConnection()
        {
            return myTrans.Connection;
        }

        public void commit()
        {
            myTrans.Commit();
        }

        public int getUserAccessRight(int moduleID, int groupID)
        {
            int result = 0;

            result = Convert.ToInt32(getDataSingleValue("SELECT IFNULL(USER_ACCESS_OPTION, 0) FROM USER_ACCESS_MANAGEMENT WHERE MODULE_ID = " + moduleID + " AND GROUP_ID = " + groupID));

            return result;
        }

    }
}
