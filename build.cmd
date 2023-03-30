@echo off

pushd src
dotnet publish -c Release
popd

rmdir /s /q dist
mkdir dist

copy /y fxmanifest.lua dist
copy /y Config.yaml dist
xcopy /y /e src\bin\Release\netstandard2.0\publish\ dist\src\

rmdir /s /q src\bin\
rmdir /s /q src\obj\