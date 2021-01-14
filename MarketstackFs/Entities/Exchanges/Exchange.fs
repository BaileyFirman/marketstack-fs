namespace MarketstackFs.Entities.Exchanges

open FSharp.Json
open MarketstackFs.Entities.Exchanges.Currency

module Exchange =
    type Exchange =
        { Acronym: string
          City: string
          Country: string
          [<JsonField("country_code")>]
          CountryCode: string
          Mic: string
          Name: string
          Website: string
          Currency: Currency }
