namespace MarketstackFs.Services

module MarketstackOptions =
    type MarketstackOptions =
        { ApiToken: string
          MaxRequestsPerSecond: int
          Https: bool }
