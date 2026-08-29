# Digital Wallet - Assignment A02

## 1. Problem Statement
The wallet should protect its balance and transaction history while exposing only valid operations. Deposits and withdrawals must be validated, and invalid attempts must be rejected without corrupting the wallet's state.

## 2. Design Overview
This project demonstrates Encapsulation.
* The balance and transaction list are private fields, nothing outside the class can read or modify them directly.
* The balance can only change through `Deposit` and `Withdraw`, both of which validate their input before touching any state.
* The transaction history is exposed through `Transactions`, a read-only view over the internal list, so other code using the wallet can look at it without being able to change past records.
* Deposits are recorded as positive values and withdrawals as negative values. This is a design choice made to keep the history simple to read and sum.
* The constructor also rejects a negative initial balance, so a wallet can never start in an invalid state. A dedicated test checks this.

## 3. Class Diagram Sketch
```text
+----------------------------------------------------+
|                   DigitalWallet                    |
+----------------------------------------------------+
| - _balance: decimal                                |
| - _transactions: List<decimal>                     |
+----------------------------------------------------+
| + DigitalWallet(initialBalance: decimal)           |
| + Balance: decimal <<get>>                         |
| + Transactions: IReadOnlyList<decimal> <<get>>     |
| + Deposit(amount: decimal): void                   |
| + Withdraw(amount: decimal): void                  |
+----------------------------------------------------+
```

## 4. Build and Test Commands
From the solution root directory:
* Build: `dotnet build`
* Test: `dotnet test`

## 5. Test Summary
There are 7 unit tests:
1. `Constructor_SetsInitialBalance_Successfully`: wallet starts with the correct balance and no transactions.
2. `Constructor_NegativeInitialBalance_ThrowsException`: a negative starting balance is rejected.
3. `Deposit_ValidAmount_IncreasesBalanceAndRecordsTransaction`: deposits increase the balance and are logged.
4. `Deposit_NegativeAmount_ThrowsException`: a non-positive deposit is rejected.
5. `Withdraw_ValidAmount_DecreasesBalanceAndRecordsNegativeTransaction`: withdrawals decrease the balance and are logged as negative.
6. `Withdraw_ExactBalance_SucceedsAndBalanceReachesZero`: withdrawing the full balance succeeds and leaves zero.
7. `Withdraw_MoreThanBalance_ThrowsException`: withdrawing more than the balance is rejected.

## 6. Critical Analysis & Limitations
This implementation covers every minimum requirement from the assignment: private state, validated deposit and withdraw methods, no state corruption on invalid input, and a read-only view of the transaction history. It also rejects a negative initial balance in the constructor, extending the same validation idea to how the wallet starts, not just how it changes.

The design comes down to one core idea, only `Deposit` and `Withdraw` are allowed to touch the balance, and both validate their input before changing anything, so a rejected call never leaves the wallet in a partially updated state. One class is enough to cover this, there was no real need for additional layers. The seven tests cover both operations working correctly, both operations rejecting bad input, and the boundary of withdrawing the exact balance down to zero. Documentation is split between this README and the XML doc comments on the public members.

Limitations:
* The transaction history is just a list of numbers. A real system would need a dedicated transaction type with a date, time, and unique ID.
* The code is not thread-safe. Concurrent calls to `Deposit` or `Withdraw` from multiple threads could cause a race condition between checking the balance and updating it.