using NUnit.Framework;
using FeeSystem;
using System;
using System.Collections.Generic;

[TestFixture]
public class FeeCalculatorTests
{
    // Test 1:
    // Check that if no payments have been made,
    // the full fee remains outstanding.
    [Test]
    public void OutstandingBalance_NoPayments_ReturnsFullFee()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>();

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(600m));
    }

    // Test 2:
    // Check that a single partial payment reduces the balance correctly.
    [Test]
    public void OutstandingBalance_OnePartialPayment_ReturnsRemainingBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(400m));
    }

    // Test 3:
    // Check that several instalments are added together correctly.
    [Test]
    public void OutstandingBalance_SeveralInstalments_ReturnsCorrectBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m, 200m, 100m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(100m));
    }

    // Test 4:
    // Check that a fully paid fee leaves no outstanding balance.
    [Test]
    public void OutstandingBalance_FullyPaid_ReturnsZero()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 600m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    // Test 5:
    // Check the system handles overpayment correctly.
    [Test]
    public void OutstandingBalance_Overpayment_ReturnsNegativeBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 700m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(-100m));
    }

    // Test 6:
    // Check that a negative fee causes an ArgumentException.
    [Test]
    public void OutstandingBalance_NegativeFee_ThrowsArgumentException()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>();

        // Act & Assert
        Assert.That(
            () => calc.OutstandingBalance(-1m, payments),
            Throws.ArgumentException);
    }

    // Test 7:
    // Check that a student is cleared for exams
    // when exactly half the fee has been paid.
    [Test]
    public void IsClearedForExams_ExactlyHalfPaid_ReturnsTrue()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 300m };

        // Act
        var result = calc.IsClearedForExams(600m, payments);

        // Assert
        Assert.That(result, Is.True);
    }

    // Test 8:
    // Check that a student is NOT cleared for exams
    // when payment is one toea below half the fee.
    [Test]
    public void IsClearedForExams_OneToeaBelowHalf_ReturnsFalse()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 299.99m };

        // Act
        var result = calc.IsClearedForExams(600m, payments);

        // Assert
        Assert.That(result, Is.False);
    }
}

