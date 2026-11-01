# Gocgun.Results

> **Beta.** This package is in beta: the public API may change between releases.

Application result types for success and failure.

- `Result` has no payload. Success is HTTP 204 at the edge; this package does not reference HTTP.
- `Result<T>` carries `Item`. Failure leaves `Item` at its default and sets `Error`.
- `ResultPaginated<T>` carries `Item` and optional `Total`. No next cursor or has-more flag: the client sends the last row's key as the cursor and stops when a page is shorter than its limit.
- `Error` carries `Code`, `Message`, `ErrorType`, optional field `Details`, and optional `Metadata`.
- `ResultFactory.Failure<TResponse>` builds a failed result without reflection.

No ASP.NET Core types. No Entity Framework types. License: AGPL-3.0-only (`LICENSE`). Copyright holder exception: `LICENSE-EXCEPTION`.

Tests are unit tests. The package has no process, network, or database boundary, so an integration project would not exercise a different seam.
