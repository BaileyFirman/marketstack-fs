namespace MarketstackFs.Services

open FSharp.Data
open MarketstackFs.Entities.PageResponse
open Newtonsoft.Json

module HttpClient =
    exception PageLimitException of string

    type QueryStringItem = string * string

    let GetPageResonse<'T> url (queryString: QueryStringItem list) limit (offset: int option): Async<PageResponse<'T>> =
        async {
            let pageOffset =
                match offset with
                | Some (offset) -> offset
                | None -> 0

            let pagingQueryStringItems: QueryStringItem list =
                [ "limit", limit.ToString()
                  "offset", pageOffset.ToString() ]

            let query: QueryStringItem list = queryString @ pagingQueryStringItems

            let result: PageResponse<'T> =
                async {
                    let! html = Http.AsyncRequestString(url, query)
                    return html
                }
                |> Async.RunSynchronously
                |> JsonConvert.DeserializeObject<PageResponse<'T>>

            return result
        }


    let GetAsync<'T> url apiToken (limit: int option): list<'T> =
        let pageLimit =
            match limit with
            | Some (limit) -> limit
            | None -> PageResponse.MaxLimit

        match pageLimit > PageResponse.MaxLimit with
        | true -> raise (PageLimitException "Maximum allowed limit value is {PageResponse.MaxLimit}")
        | false ->
            let queryString: QueryStringItem list = [ "access_key", apiToken ]

            let firstReponse: PageResponse<'T> =
                GetPageResonse<'T> url queryString pageLimit None
                |> Async.RunSynchronously

            let pages: list<PageResponse<'T>> =
                firstReponse.AllRequestOffsets()
                |> List.map (fun offset -> GetPageResonse<'T> url queryString pageLimit (Some offset))
                |> Async.Parallel
                |> Async.RunSynchronously
                |> Array.toList

            let data =
                pages |> List.collect (fun page -> page.Data)

            data @ firstReponse.Data
