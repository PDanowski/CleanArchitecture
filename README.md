# CleanArchitecture

The main solution is in `Notebook/Notebook`.

Current baseline:
- .NET SDK `10.0.104` (`global.json`)
- Target framework `net10.0`
- Centrally managed package versions in `Notebook/Notebook/Directory.Packages.props`

Docker:
- Build: `docker build -f Notebook/Notebook/Dockerfile -t notebook-web:local Notebook/Notebook`
- Run: `docker run --rm -p 8080:8080 notebook-web:local`
