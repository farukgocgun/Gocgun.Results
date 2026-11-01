# Gocgun.Results

> **Beta.** This package is in beta: the public API may change between releases.

Application result types for success and failure.

- `Result` has no payload. Success is HTTP 204 at the edge; this package does not reference HTTP.
- `Result<T>` carries `Item`. Failure leaves `Item` at its default and sets `Error`.
- `ResultPaginated<T>` carries `Item` and optional `Total`. No next cursor or has-more flag: the client sends the last row's key as the cursor and stops when a page is shorter than its limit.
- `Error` carries `Code`, `Message`, `ErrorType`, optional field `Details`, and optional `Metadata`.
- `ResultFactory.Failure<TResponse>` builds a failed result without reflection.

No ASP.NET Core types. No Entity Framework types. License: MIT (`LICENSE`).

Tests are unit tests. The package has no process, network, or database boundary, so an integration project would not exercise a different seam.

## Publishing to nuget.org

Run `scripts/publish.sh` (or `scripts/publish.ps1` on Windows). It dispatches the manual `release.yml` workflow of each dependency first, in dependency order (Gocgun.Results), then of this package, waiting for each run and stopping on the first failure. Requires the GitHub CLI signed in with `gh auth login`; the `nuget` environment and the nuget.org Trusted Publishing policy must exist in each repository.
