namespace MarketstackFs.Interfaces

open MarketstackFs.Entities.Exchanges.Exchange
open MarketstackFs.Entities.Stocks.Stock
open MarketstackFs.Entities.Stocks.StockBar
open System

module IMarketstackService =
    type IMarketstackService =
        abstract member GetExchanges: unit -> Async<list<Exchange>>
        abstract member GetExchangeStocks: string -> Async<list<Stock>>
        abstract member GetStockEodBars: string -> DateTime -> DateTime -> Async<list<StockBar>>
        abstract member GetStockIntraDayBars: string -> DateTime -> DateTime -> Async<list<StockBar>>
