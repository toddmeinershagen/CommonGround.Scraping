﻿using CommonGround.Scraping.Service.AcceptanceTests;
using NUnit.Framework;

[SetUpFixture]
public class AssemblyFixture
{
    public static void AfterAll()
    {
        Server.Instance.Dispose();
    }
}