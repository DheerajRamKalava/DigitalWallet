using System;
using System.Collections.Generic;

namespace DigitalWallet;

/// <summary>
/// Represents a wallet that holds a balance and a history of deposits and withdrawals.
/// </summary>
public sealed class DigitalWallet
{
    private decimal _balance;
    private readonly List<decimal> _transactions = [];

    /// <summary>
    /// Creates a wallet with the given starting balance.
    /// </summary>
    /// <param name="initialBalance">The opening balance. Cannot be negative.</param>
    public DigitalWallet(decimal initialBalance)
    {
        if (initialBalance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");
        }

        _balance = initialBalance;
    }

    /// <summary>
    /// Gets the current balance.
    /// </summary>
    public decimal Balance => _balance;

    /// <summary>
    /// Gets the transaction history. Deposits are recorded as positive values and withdrawals as negative values.
    /// </summary>
    public IReadOnlyList<decimal> Transactions => _transactions.AsReadOnly();

    /// <summary>
    /// Adds funds to the wallet.
    /// </summary>
    /// <param name="amount">The amount to deposit. Must be greater than zero.</param>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");
        }

        _balance += amount;
        _transactions.Add(amount);
    }

    /// <summary>
    /// Removes funds from the wallet.
    /// </summary>
    /// <param name="amount">The amount to withdraw. Must be greater than zero and no more than the current balance.</param>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be greater than zero.");
        }

        if (amount > _balance)
        {
            throw new InvalidOperationException("Insufficient funds for this withdrawal.");
        }

        _balance -= amount;
        _transactions.Add(-amount);
    }
}