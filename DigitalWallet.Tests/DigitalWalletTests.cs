using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalWallet;

namespace DigitalWallet.Tests;

[TestClass]
public class DigitalWalletTests
{
    [TestMethod]
    public void Constructor_SetsInitialBalance_Successfully()
    {
        var wallet = new DigitalWallet(100m);

        Assert.AreEqual(100m, wallet.Balance);
        Assert.IsFalse(wallet.Transactions.Any());
    }

    [TestMethod]
    public void Constructor_NegativeInitialBalance_ThrowsException()
    {
        try
        {
            new DigitalWallet(-1m);
            Assert.Fail("Expected an ArgumentOutOfRangeException to be thrown.");
        }
        catch (ArgumentOutOfRangeException)
        {
        }
    }

    [TestMethod]
    public void Deposit_ValidAmount_IncreasesBalanceAndRecordsTransaction()
    {
        var wallet = new DigitalWallet(100m);

        wallet.Deposit(50m);

        Assert.AreEqual(150m, wallet.Balance);
        Assert.AreEqual(50m, wallet.Transactions.Last());
    }

    [TestMethod]
    public void Deposit_NegativeAmount_ThrowsException()
    {
        var wallet = new DigitalWallet(100m);

        try
        {
            wallet.Deposit(-10m);
            Assert.Fail("Expected an ArgumentOutOfRangeException to be thrown.");
        }
        catch (ArgumentOutOfRangeException)
        {
        }
    }

    [TestMethod]
    public void Withdraw_ValidAmount_DecreasesBalanceAndRecordsNegativeTransaction()
    {
        var wallet = new DigitalWallet(100m);

        wallet.Withdraw(40m);

        Assert.AreEqual(60m, wallet.Balance);
        Assert.AreEqual(-40m, wallet.Transactions.Last());
    }

    [TestMethod]
    public void Withdraw_ExactBalance_SucceedsAndBalanceReachesZero()
    {
        var wallet = new DigitalWallet(50m);

        wallet.Withdraw(50m);

        Assert.AreEqual(0m, wallet.Balance);
    }

    [TestMethod]
    public void Withdraw_MoreThanBalance_ThrowsException()
    {
        var wallet = new DigitalWallet(50m);

        try
        {
            wallet.Withdraw(100m);
            Assert.Fail("Expected an InvalidOperationException to be thrown.");
        }
        catch (InvalidOperationException)
        {
        }
    }
}
