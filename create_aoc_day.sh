#!/bin/bash

# Check if year and day are provided
if [ -z "$1" ] || [ -z "$2" ]; then
    echo "Usage: $0 <year> <day>"
    echo "Example: $0 2023 1"
    exit 1
fi

YEAR=$1
DAY=$(printf "%02d" $2) # Pad day with leading zero for consistent directory naming

PROJECT_ROOT="src/$YEAR/Day$DAY"
PROJECT_NAME="Day$DAY"

# Create year directory if it doesn't exist
if [ ! -d "src/$YEAR" ]; then
    echo "Creating directory: src/$YEAR"
    mkdir -p "src/$YEAR"
fi

# Create day directory
echo "Creating directory: $PROJECT_ROOT"
mkdir -p "$PROJECT_ROOT"

# Create new .NET console project
echo "Creating new .NET console project: $PROJECT_NAME in $PROJECT_ROOT"
dotnet new console -n "$PROJECT_NAME" -o "$PROJECT_ROOT" -f net10.0 # Use -f net10.0 as per reference

# Modify .csproj to include input.txt as content
CSPROJ_FILE="$PROJECT_ROOT/$PROJECT_NAME.csproj"
echo "Modifying $CSPROJ_FILE to include input.txt"
sed -i '' '/<ItemGroup>/a\
        <None Remove="input.txt"/>\
        <Content Include="input.txt">\
            <CopyToOutputDirectory>Always</CopyToOutputDirectory>\
        </Content>' "$CSPROJ_FILE"

# Create empty input.txt
echo "Creating empty input.txt in $PROJECT_ROOT"
touch "$PROJECT_ROOT/input.txt"

# Create basic Program.cs
echo "Creating basic Program.cs in $PROJECT_ROOT"
cat <<EOF > "$PROJECT_ROOT/Program.cs"
using System.Diagnostics;

var readLines = File.ReadLines("input.txt").ToList();

var stopwatch = Stopwatch.StartNew();
Console.WriteLine($"Part One: {TaskOne(readLines)}");
Console.WriteLine($"Part Two: {TaskTwo(readLines)}");
Console.WriteLine($"Elapsed MS: {stopwatch.ElapsedMilliseconds}");

return;

int TaskOne(List<string> input)
{
    // Implement Part One logic here
    return 0;
}

int TaskTwo(List<string> input)
{
    // Implement Part Two logic here
    return 0;
}
EOF

# Add the new project to the solution under the year folder
echo "Adding $PROJECT_NAME to Advent-of-Code.sln under year $YEAR folder"
dotnet sln Advent-of-Code.sln add "$PROJECT_ROOT/$PROJECT_NAME.csproj" --solution-folder "$YEAR"

echo "Advent of Code setup for Year $YEAR, Day $DAY completed."