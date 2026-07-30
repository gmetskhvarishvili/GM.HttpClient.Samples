# Contributing to GM.HttpClient Samples

This repository is a **usage sample** for the [`GM.HttpClient`](https://www.nuget.org/packages/GM.HttpClient)
package. It is not published to NuGet — there is no versioning or release workflow here. The goal is
to keep the sample building, tested, and easy to follow.

## Prerequisites

- **.NET 10 SDK**

```bash
dotnet build -c Release
dotnet test  -c Release
```

## Branch & PR flow

1. Branch off `master`: `git switch -c fix/sample-caching`
2. Open a PR into `master`. CI (`build` + tests) must pass.
3. Keep changes focused and the README in sync with what the sample does.

## Guidelines

- The consumer API references **only** `GM.HttpClient` (Refit/Polly come transitively) — keep it that
  way so the sample shows the minimal dependency footprint.
- Client behaviour is configured in `appsettings.json` under `ApiServices:<name>`; prefer configuration
  over code when demonstrating a feature.
- Add or update an integration test in `tests/GM.HttpClient.Sample.Tests` when you change endpoint
  behaviour.
- Bump the `GM.HttpClient` package version when a new release adds something the sample should show.

## Where releases happen

Package versioning, tags, changelog and nuget.org publishing live in the library repository
([`GM.HttpClient`](https://github.com/gmetskhvarishvili/GM.HttpClient)), driven by Conventional Commits.
Nothing is published from this samples repo.

## Code style

Enforced by [`.editorconfig`](.editorconfig). Run `dotnet format` before pushing if unsure.
