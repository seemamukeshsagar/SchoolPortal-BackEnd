# Stop all running dotnet processes
Write-Host "Stopping dotnet processes..."
Stop-Process -Name "dotnet" -Force -ErrorAction SilentlyContinue

# Kill any remaining processes that might be locking the files
$processes = Get-Process | Where-Object { $_.Path -like "*SchoolPortal*" -or $_.ProcessName -like "*iisexpress*" }
foreach ($process in $processes) {
    try {
        Write-Host "Stopping process: $($process.ProcessName) (ID: $($process.Id))"
        Stop-Process -Id $process.Id -Force -ErrorAction Stop
    } catch {
        Write-Host "Could not stop process $($process.ProcessName): $_"
    }
}

# Clean solution
Write-Host "Cleaning solution..."
dotnet clean "E:\Workspace\SchoolPortal-BackEnd\SchoolPortal-BackEnd.sln" -c Debug -v m

# Delete bin and obj folders
$foldersToDelete = @("bin", "obj")
$projectRoots = @(
    "E:\Workspace\SchoolPortal-BackEnd\SchoolPortal-BackEnd\SchoolPortal-API",
    "E:\Workspace\SchoolPortal-BackEnd\SchoolPortal-BackEnd\SchoolPortal-BackEnd.ServiceDefaults"
)

foreach ($root in $projectRoots) {
    foreach ($folder in $foldersToDelete) {
        $path = Join-Path $root $folder
        if (Test-Path $path) {
            Write-Host "Removing $path..."
            Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}

Write-Host "Cleanup complete. You can now rebuild your solution."
