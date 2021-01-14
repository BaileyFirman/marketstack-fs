namespace MarketstackFs.Entities

module Pagination =
    type Pagination =
        { Limit: int
          Offset: int
          Count: int
          Total: int }
