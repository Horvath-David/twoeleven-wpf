# 2<sup>11</sup>  WPF

2048 app made with WPF, with animations and everything fancy.

### Building

Command used to build the single exe:

```
dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true
```