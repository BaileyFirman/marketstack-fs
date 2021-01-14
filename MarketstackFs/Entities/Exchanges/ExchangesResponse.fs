namespace MarketstackFs.Entities.Exchanges

open MarketstackFs.Entities.Exchanges.Exchange
open MarketstackFs.Entities.PageResponse

module ExchangesResponse =
    type ExchangesResponse = PageResponse<Exchange>
