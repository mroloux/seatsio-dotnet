﻿using SeatsioDotNet.Subaccounts;

namespace SeatsioDotNet.Test
{
    public class TestCompany
    {
        public User admin { get; set; }
        public Subaccount subaccount { get; set; }
    }

    public class User
    {
        public string SecretKey { get; set; }
    }
}