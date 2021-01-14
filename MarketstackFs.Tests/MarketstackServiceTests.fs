module MarketstackFs.Tests

open MarketstackFs.Interfaces.IMarketstackService
open MarketstackFs.Services.MarketstackOptions
open MarketstackFs.Services.MarketstackService
open NUnit.Framework
open System

let testKey = ""

let msOptions: MarketstackOptions =
    { ApiToken = testKey
      MaxRequestsPerSecond = 1
      Https = false }

let subject =
    MarketstackService(msOptions) :> IMarketstackService

[<SetUp>]
let Setup () = ()

[<Test>]
let GetExchangesReturnsExchanges () =
    let exchanges =
        subject.GetExchanges() |> Async.RunSynchronously

    Assert.IsNotEmpty(exchanges)


[<Test>]
let GetExchangeStocksReturnsStocks () =
    let nasdaqMic = "XNAS"

    let stocks =
        subject.GetExchangeStocks nasdaqMic
        |> Async.RunSynchronously

    Assert.True(stocks.Length = 1000)


[<Test>]
let GetStockEodBarsReturnsBars () =
    let appleSymbol = "AAPL"
    let fromDate = new DateTime(2020, 1, 1)
    let toDate = DateTime.Now

    let bars =
        subject.GetStockEodBars appleSymbol fromDate toDate
        |> Async.RunSynchronously


    let distinctDates =
        bars |> List.distinctBy (fun x -> x.Date)

    Assert.IsNotEmpty(bars)
    Assert.AreEqual(distinctDates.Length, bars.Length)
    Assert.True(bars.Length > 100)

[<Test>]
let GetStockEodBarsParallelReturnsBars () =
    let symbols =
        [ "AAPL"
          "MSFT"
          "GOOG"
          "VOD"
          "NVDA"
          "NFLX"
          "PEP"
          "NOW"
          "VEEV"
          "MOH" ]

    let fromDate = new DateTime(2020, 1, 1)
    let toDate = DateTime.Now

    let stockBars =
        symbols
        |> List.map
            (fun symbol ->
                subject.GetStockEodBars symbol fromDate toDate
                |> Async.RunSynchronously)

    Assert.True(symbols.Length = stockBars.Length)

[<Test>]
let GetIntradayBarsReturnsBars () =
    let appleSymbol = "AAPL"
    let fromDate = DateTime.Now.AddDays(-5.0)
    let toDate = DateTime.Now

    let bars =
        subject.GetStockIntraDayBars appleSymbol fromDate toDate
        |> Async.RunSynchronously

    Assert.IsNotEmpty(bars)
    Assert.True(bars.Length > 10)
