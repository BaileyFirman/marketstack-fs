namespace MarketstackFs.Entities.Exchanges

open MarketstackFs.Entities.PageResponse
open MarketstackFs.Entities.Exchanges.Exchange

module ExchangesResponse =
    type ExchangesResponse = PageResponse<Exchange>