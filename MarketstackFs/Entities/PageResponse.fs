namespace MarketstackFs.Entities

open MarketstackFs.Entities.Pagination

module PageResponse =
    exception PaginationException of string

    type PageResponse() =
        member __.MaxLimit: int = 1000

    type PageResponse<'T>() =
        member __.Pagination: Pagination =
            { Limit = 0
              Offset = 0
              Count = 0
              Total = 0 }

        member __.Data: list<'T> = []

        member __.IsLastResponse: bool =
            __.Pagination.Count < __.Pagination.Limit

        member __.NextOffset: int =
            match __.IsLastResponse with
            | true -> raise (PaginationException "No remaining offset")
            | false -> __.Pagination.Offset + __.Pagination.Count

        member __.AllRequestOffsets(): list<int> =
            let offsets: list<int> = []
            let paginationTotal = __.Pagination.Total
            let maxLimit = (PageResponse()).MaxLimit

            let rec addOffset start: list<int> =
                match start < paginationTotal with
                | true ->
                    offsets |> List.append [ start ] |> ignore
                    addOffset (start + maxLimit)
                | false -> offsets

            addOffset (__.Pagination.Offset + maxLimit)
