namespace MarketstackFs.Entities.Stocks

open System
open Newtonsoft.Json

module StockBar =
    type StockBar =
        { [<JsonProperty("adj_close")>]
          AdjustedClose: float
          [<JsonProperty("adj_high")>]
          AdjustedHigh: float
          [<JsonProperty("adj_low")>]
          AdjustedLow: float
          [<JsonProperty("adj_volume")>]
          AdjustedVolume: float
          Close: float
          High: float
          Low: float
          Open: float
          Volume: float
          Exchange: string
          Symbol: string
          Date: DateTime }
