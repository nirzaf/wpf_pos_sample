using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Positive.PositiveDataSetTableAdapters;

namespace Positive.DataModels
{
    public class UserAccounts
    {
        private UserAccountsTableAdapter adapter;
        private PositiveDataSet dataset;

        public UserAccounts()
        {
            this.dataset = new PositiveDataSet();

            this.adapter = new UserAccountsTableAdapter();
            this.adapter.Fill(dataset.UserAccounts);
        }
        
        public List<UserAccount> GetUserAccounts()
        {
            this.adapter.Fill(dataset.UserAccounts);
            List<UserAccount> userAccounts = new List<UserAccount>();

            foreach (DataRow row in this.dataset.UserAccounts)
            {
                userAccounts.Add(GetUserAccountFromDataRow(row));
            }

            return userAccounts;
        }

        public UserAccount GetUserAccountByID(int? ID)
        {
            if (ID == null)
                return null;
            
            this.adapter.FillByID(dataset.UserAccounts, ID);

            // Check if account exists
            if (this.dataset.UserAccounts.Rows.Count > 0)
                return GetUserAccountFromDataRow(this.dataset.UserAccounts.Rows[0]);

            return null;
        }

        #region Data Mapping

        private UserAccount GetUserAccountFromDataRow(DataRow row)
        {
            PositiveDataSet.UserAccountsRow userAccountRow = row as PositiveDataSet.UserAccountsRow;

            UserAccount userAccount = new UserAccount
            {
                ID = userAccountRow.ID,
                PINNumber = userAccountRow.PINNumber,
                FirstName = userAccountRow.Firstname,
                LastName = userAccountRow.Lastname
            };
            return userAccount;
        }

        #endregion
    }
}
