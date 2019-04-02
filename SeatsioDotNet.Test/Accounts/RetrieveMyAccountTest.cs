﻿using SeatsioDotNet.Accounts;
using Xunit;

namespace SeatsioDotNet.Test.Accounts
{
    public class RetrieveMyAccountTest : SeatsioClientTest
    {
        [Fact]
        public void Test()
        {
            var account = Client.Accounts.RetrieveMyAccount();
            
            Assert.NotNull(account.SecretKey);
            Assert.NotNull(account.DesignerKey);
            Assert.NotNull(account.PublicKey);
            Assert.NotNull(account.Email);
            Assert.False(account.IsSubaccount);
            Assert.True(account.Settings.DraftChartDrawingsEnabled);
            Assert.True(account.Settings.HoldOnSelectForGAs);
            Assert.Equal(ChartValidationLevel.ERROR, account.Settings.ChartValidation.ValidateDuplicateLabels);
            Assert.Equal(ChartValidationLevel.ERROR, account.Settings.ChartValidation.ValidateObjectsWithoutCategories);
            Assert.Equal(ChartValidationLevel.ERROR, account.Settings.ChartValidation.ValidateUnlabeledObjects);
        }
    }
}