namespace MarketstackFs.Services

open MarketstackFs.Entities.Exchanges.Exchange
open MarketstackFs.Entities.Stocks.Stock
open MarketstackFs.Entities.Stocks.StockBar
open MarketstackFs.Interfaces.IMarketstackService
open MarketstackFs.Services.HttpClient
open MarketstackFs.Services.MarketstackOptions

module MarketstackService =
    type MarketstackService(options) =
        let options: MarketstackOptions = options

        let apiUrl =
            match options.Https with
            | true -> "https://api.marketstack.com/v1"
            | false -> "http://api.marketstack.com/v1"


        interface IMarketstackService with
            member __.GetExchanges() =
                let url = apiUrl + "/exchanges"
                async { return GetAsync<Exchange> url options.ApiToken None }

            member __.GetExchangeStocks exchangeMic =
                let url = apiUrl + "/tickers?exchange=" + exchangeMic
                async { return GetAsync<Stock> url options.ApiToken None }

            member __.GetStockEodBars stockSymbol fromDate toDate =
                let dateFromStr =
                    "&date_from=" + fromDate.ToString("yyyy-MM-dd")

                let dateToStr =
                    "&date_to=" + toDate.ToString("yyyy-MM-dd")

                let url =
                    apiUrl
                    + "/eod?symbols="
                    + stockSymbol
                    + dateFromStr
                    + dateToStr

                async { return GetAsync<StockBar> url options.ApiToken None }

            member __.GetStockIntraDayBars stockSymbol fromDate toDate =
                let dateFromStr =
                    "&date_from="
                    + fromDate.ToString("yyyy-MM-dd HH:mm:ss")

                let dateToStr =
                    "&date_to="
                    + toDate.ToString("yyyy-MM-dd HH:mm:ss")

                let url =
                    apiUrl
                    + "/intraday?symbols="
                    + stockSymbol
                    + dateFromStr
                    + dateToStr

                async { return GetAsync<StockBar> url options.ApiToken None }
