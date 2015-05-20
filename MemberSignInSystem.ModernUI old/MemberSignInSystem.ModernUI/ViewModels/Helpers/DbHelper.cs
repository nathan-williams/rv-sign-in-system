using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Data;
using System.Windows.Input;
using System.IO;
using MemberSignInSystem.ModernUI.ViewModels.Models;
using System.Windows;

namespace MemberSignInSystem.ModernUI.ViewModels.Helpers
{
    public class DbHelper
    {
        static DataSet dataset = new DataSet();
        static DataTable searchHelper;
        static Family selectedMember; // Used with search helper

        static OleDbConnection conn = new OleDbConnection();
        
        public static DataTable MemberList
        {
            get { return dataset.Tables[0]; }
        }
        public static DataTable SearchHelper
        {
            get { return searchHelper; }
            set { searchHelper = value; }
        }
        public static DataSet DataSet
        {
            get { return dataset; }
        }

        public static DataTable searchByName(String n)
        {
            string tableName = "member";
            string cmdText = "SELECT * from " + tableName + " WHERE (surname LIKE ? + '%') OR (name LIKE ? + '%')";
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("sn", n);
            cmd.Parameters.AddWithValue("n", n);
            
            DataTable m = getData(cmd, tableName);
            RemoveFillers(m);
            copyToSearchHelper(m);

            if (m.Rows.Count == 1)
            {
                selectedMember = new Family(m.Rows[0]);
            }

            return m;

            /*
            
            string tableName = "individuals";
            string cmdText = "SELECT * from " + tableName + " WHERE (firstname LIKE ? + '%') OR (lastname LIKE ? + '%')";
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("fname", n);
            cmd.Parameters.AddWithValue("lname", n);
            
            DataTable m2 = getData(cmd, tableName);

            if (m2.Rows.Count != 0)
            {
                List<String> ids = new List<String>();
                foreach (DataRow row in m2.Rows)
                {
                    String target = row["id"] as String;
                    ids.Add(target.Substring(0, target.IndexOf('.')));
                }
                tableName = "families";
                cmdText = "SELECT * from " + tableName + " WHERE ";
                cmd.Parameters.Clear();
                foreach (String id in ids)
                {
                    cmdText += "( id = ? ) OR ";
                    cmd.Parameters.AddWithValue("id", id);
                }
                cmdText = cmdText.Remove(cmdText.Length - 4);
                cmd.CommandText = cmdText;

                m = getData(cmd, tableName);
            }
            
            copyToSearchHelper(m);

            if (m.Rows.Count == 1)
            {
                selectedMember = new Family(m.Rows[0]);
            }

            return m;
            */
        }

        public static DataTable searchByIdForFamilies(String id)
        {
            string tableName = "member";
            string cmdText = "SELECT * from " + tableName + " WHERE id = ?";
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("id", id);

            DataTable m = getData(cmd, tableName);
            RemoveFillers(m);
            copyToSearchHelper(m);

            if (m.Rows.Count == 1)
            {
                selectedMember = new Family(m.Rows[0]);
            }

            return m;
        }
        /*
        public static DataTable searchByIdForIndividuals(String id)
        {
            string tableName = "individuals";
            string cmdText = "SELECT * from " + tableName + " WHERE id = ?";
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("id", id);

            DataTable m = getData(cmd, tableName);

            if (m.Rows.Count != 0)
            {
                String target = (new Individual(m.Rows[0])).Id;
                target = target.Substring(0, target.IndexOf('.'));
                m = searchByIdForFamilies(target);
            }
            else
            {
                copyToSearchHelper(new DataTable()); // Empty DataTable.
            }

            return m;
        }
        */
        /*
        // Can only be used in Family constructor.
        public static DataTable searchByIdForIndividualsWithWildcard(String id)
        {
            string tableName = "individuals";
            string cmdText = "SELECT * from " + tableName + " WHERE (id LIKE ? + '%')";
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("id", id);

            DataTable m = getData(cmd, tableName);

            String target = (new Individual(m.Rows[0])).Id;
            target = target.Substring(0, target.IndexOf('.'));

            return m;
        }
        */

        private static DataTable getData(OleDbCommand cmd, String tableName)
        {
            verifyConnectionOpen();

            if (conn.State == ConnectionState.Open)
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                ds.Load(reader, LoadOption.PreserveChanges, tableName);
                return ds.Tables[tableName];
            }
            else
            {
                LoginViewModel.DoDisplayError = true;
                LoginViewModel.ErrorMessage = "Database file does not exist.";
                LoginViewModel.ErrorDisplayRule = "TextChanged";

                (Application.Current.Resources["LoginViewModel"] as LoginViewModel).ForceValidateCommand.Execute(null);
                return new DataTable();
            }
        }

        public static void verifyConnectionOpen()
        {
            string dbPath = Application.Current.Resources["RootFolder"] + "\\membership.mdb";
            if (conn.State != ConnectionState.Open || conn.DataSource != dbPath)
            {
                string ext = Path.GetExtension(dbPath);
                if (ext == ".accdb" || ext == ".mdb")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + dbPath + ";";
                else
                    throw new Exception("Database file extension invalid. Be sure you're trying to use a .accdb or .mdb file.");
                
                if (File.Exists(dbPath))
                    conn.Open();
                // else bad database path
            }
        }

        public static void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public static void copyToSearchHelper(DataTable source)
        {
            searchHelper = source.Copy();
        }

        public static Family SelectedMember
        {
            get { return selectedMember; }
            set { selectedMember = value; }
        }

        public static int classify(String query)
        {
            // Query only contains numbers (after spaces removed)
            if (query.Replace(" ", "").Replace(".", "").All(Char.IsNumber))
            {
                /*
                if (query.Contains('.') && query.LastOrDefault() != '.')
                    return 2; // Individual search
                else
                    return 1; // Family search
                */
                return 1;
            }
            // Query contains non-numbers
            return 0; // Name search
        }

        public static DataTable RemoveFillers(DataTable m)
        {
            List<DataRow> toRemove = new List<DataRow>();
            foreach (DataRow row in m.Rows)
            {
                String n = (row["name"] as String).ToLower();
                String sn = (row["surname"] as String).ToLower();
                if (n.Contains("bond") || sn.Contains("bond") ||
                    n.Contains("associate") || sn.Contains("associate") ||
                    n.Contains("available") || sn.Contains("available") ||
                    n.Contains("membership") || sn.Contains("membership"))
                    toRemove.Add(row);
            }
            foreach (DataRow row in toRemove)
            {
                m.Rows.Remove(row);
            }
            return m;
        }

        public static DataTable RemoveDupes(DataTable m)
        {
            List<DataRow> toRemove = new List<DataRow>();
            List<Int32> idHistory = new List<Int32>();
            foreach (DataRow row in m.Rows)
            {
                Int32 target = Convert.ToInt32(row["id"]);
                if (idHistory.Contains(target))
                    toRemove.Add(row);
                else
                    idHistory.Add(target);
            }
            foreach (DataRow row in toRemove)
            {
                m.Rows.Remove(row);
            }
            return m;
        }
    }
}
