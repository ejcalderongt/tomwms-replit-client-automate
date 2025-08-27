# Clear the console
Clear-Host

# Set the solution directory
$solutionDir = "C:\Users\yejc2\source\repos\TOMWMS4"

# Clean and rebuild the solution
Write-Host "Cleaning and rebuilding the solution..."
& "D:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe" "$solutionDir\TOMWMS.sln" /t:Clean,Build /p:Configuration=Debug

# Restore NuGet packages
Write-Host "Restoring NuGet packages..."
& "C:\Users\yejc2\source\repos\TOMWMS4\nuget.exe" restore "$solutionDir\TOMWMS.sln"

# Remove unused packages from the packages directory
Write-Host "Removing unused NuGet packages..."
Get-ChildItem "$solutionDir\packages" -Directory | ForEach-Object {
    $packageDir = $_.FullName
    $packageName = $_.Name

    # Find projects referencing this package
    $referencedByProjects = Get-ChildItem "$solutionDir\**\packages.config" -Recurse | Select-String -Pattern $packageName

    # If no project is referencing the package, delete the package directory
    if ($referencedByProjects -eq $null) {
        Write-Host "Deleting unused package $packageName"
        Remove-Item $packageDir -Recurse -Force
    }
}

Write-Host "Operation completed."
