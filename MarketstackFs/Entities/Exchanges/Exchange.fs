namespace MarketstackFs.Entities.Exchanges

open MarketstackFs.Entities.Exchanges.Currency
open Newtonsoft.Json
module Exchange =
    type Exchange =
        { Acronym: string
          City: string
          Country: string
          [<JsonProperty("country_code")>]
          CountryCode: string
          Mic: string
          Name: string
          Website: string
          Currency: Currency }
