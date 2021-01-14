namespace MarketstackFs.Entities.Stocks

open FSharp.Json
open System

module StockBar =
    type StockBar =
        { [<JsonField("adj_close")>]
          AdjustedClose: float option
          [<JsonField("adj_high")>]
          AdjustedHigh: float option
          [<JsonField("adj_low")>]
          AdjustedLow: float option
          [<JsonField("adj_volume")>]
          AdjustedVolume: float option
          Close: float option
          High: float option
          Low: float option
          Open: float option
          Volume: float option
          Exchange: string
          Symbol: string
          Date: DateTime }
