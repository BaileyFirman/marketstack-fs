# marketstack-fs API
A straightforward F# JSON REST library for working with (https://marketstack.com)[Marketstack]

## API

**Initialize MarketstackService:**
```F#
let msOptions: MarketstackOptions =
    { ApiToken = apiToken
      MaxRequestsPerSecond = 1
      Https = false }

let marketstackService =
    MarketstackService(msOptions) :> IMarketstackService
```

**GetExchanges:**
```F#
    let exchanges =
        marketstackService.GetExchanges ()
        |> Async.RunSynchronously
```

**GetExchangeStocks:**
```F#
    let nasdaqMic = "XNAS"

    let stocks =
        marketstackService.GetExchangeStocks nasdaqMic
        |> Async.RunSynchronously
```    
    
**GetStockEodBars:**
```F#
    let appleSymbol = "AAPL"
    let fromDate = DateTime(2020, 1, 1)
    let toDate = DateTime.Now

    let bars =
        marketstackService.GetStockEodBars appleSymbol fromDate toDate
        |> Async.RunSynchronously

```

**GetStockIntraDayBars:**
```F#
    let appleSymbol = "AAPL"
    let fromDate = DateTime.Now.AddDays(-5.0)
    let toDate = DateTime.Now

    let bars =
        marketstackService.GetStockIntraDayBars appleSymbol fromDate toDate
        |> Async.RunSynchronously

```

**Parallel Requests:**
```F#
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

    let fromDate = DateTime(2020, 1, 1)
    let toDate = DateTime.Now

    let stockBars =
        symbols
        |> List.map
            (fun symbol ->
                marketstackService.GetStockEodBars symbol fromDate toDate)
        |> Async.Parallel
```
