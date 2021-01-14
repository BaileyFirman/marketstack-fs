namespace MarketstackFs.Entities

open MarketstackFs.Entities.Pagination
open Newtonsoft.Json

module PageResponse =
    exception PaginationException of string

    type PageResponse() =
        static member MaxLimit: int = 1000

    type PageResponse<'T>(pagination, data) =
        [<JsonProperty("pagination")>]
        member __.Pagination: Pagination = pagination

        [<JsonProperty("data")>]
        member __.Data: list<'T> = data

        member __.IsLastResponse: bool =
            __.Pagination.Count < __.Pagination.Limit

        member __.NextOffset: int =
            match __.IsLastResponse with
            | true -> raise (PaginationException "No remaining offset")
            | false -> __.Pagination.Offset + __.Pagination.Count

        member __.AllRequestOffsets(): list<int> =
            let offsets: list<int> = []
            let paginationTotal = __.Pagination.Total

            let rec addOffset start: list<int> =
                match start < paginationTotal with
                | true ->
                    offsets |> List.append [ start ] |> ignore
                    addOffset (start + PageResponse.MaxLimit)
                | false -> offsets

            addOffset (__.Pagination.Offset + PageResponse.MaxLimit)
