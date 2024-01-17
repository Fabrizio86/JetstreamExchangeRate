namespace JetstreamExchangeRate.Tests;

using System;
using System.Net;
using System.Threading.Tasks;
using JetstreamExchangeRate.Controllers;
using JetstreamExchangeRate.Core;
using JetstreamExchangeRate.Core.Interfaces;
using JetstreamExchangeRate.Interfaces;
using JetstreamExchangeRate.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

public class InvoiceControllerTests
{
    // Test case for a valid input scenario
    [Test]
    public async Task CalculateInvoice_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var exchangeRateServiceMock = new Mock<IExchangeRateService>();
        exchangeRateServiceMock.Setup(x => x.GetExchangeRate(It.IsAny<DateTime>())).ReturnsAsync((decimal)1.187247);

        var invoiceCalculatorMock = new Mock<IInvoiceCalculator>();
        invoiceCalculatorMock.Setup(x => x.CalculateInvoiceDetails(It.IsAny<InvoiceInput>(), It.IsAny<decimal>()))
            .Returns(new InvoiceOutput
            {
                PreTaxTotal = 146.57m,
                TaxAmount = 14.66m,
                GrandTotal = 161.23m,
                ExchangeRate = 1.187247m
            });

        var controller = new InvoiceController(exchangeRateServiceMock.Object, invoiceCalculatorMock.Object);

        // Act
        var input = new InvoiceInput
        {
            InvoiceDate = new DateTime(2020, 8, 5),
            PreTaxAmount = 123.45m,
            PaymentCurrency = "USD"
        };

        var result = await controller.CalculateInvoice(input) as ObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        var output = result.Value as InvoiceOutput;
        Assert.IsNotNull(output);
        Assert.AreEqual(146.57m, output.PreTaxTotal);
        Assert.AreEqual(14.66m, output.TaxAmount);
        Assert.AreEqual(161.23m, output.GrandTotal);
        Assert.AreEqual(1.187247m, output.ExchangeRate);
    }

    // Test case for handling errors from the ExchangeRateService
    [Test]
    public async Task CalculateInvoice_ErrorFromExchangeRateService_ReturnsInternalServerError()
    {
        // Arrange
        var exchangeRateServiceMock = new Mock<IExchangeRateService>();
        var invoiceCalculatorMock = new Mock<InvoiceCalculator>();
        exchangeRateServiceMock.Setup(x => x.GetExchangeRate(It.IsAny<DateTime>())).ThrowsAsync(new ArgumentNullException());

        var controller = new InvoiceController(exchangeRateServiceMock.Object, invoiceCalculatorMock.Object);

        // Act
        var input = new InvoiceInput
        {
            InvoiceDate = new DateTime(2020, 8, 5),
            PreTaxAmount = 123.45m,
            PaymentCurrency = "USD"
        };

        var result = await controller.CalculateInvoice(input) as ObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        Assert.That(result.Value, Is.EqualTo("Internal Server Error: Value cannot be null."));
    }
}
