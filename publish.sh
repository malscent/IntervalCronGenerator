#!/bin/bash
export CSPROJ_PATH="./IntervalCronGenerator.Core/"
export PROJECT_NAME="IntervalCronGenerator.Core.csproj"
export GIT_BRANCH=$(git branch | cut -c 3-)
export TEST_PROJECT="./IntervalCronGeneratorTests/IntervalCronGeneratorTests.csproj"
export NUGET_SERVER="https://api.nuget.org/v3/index.json"
export BUILD_DIR="./build"
export TOOL_DIR="./IntervalCronGeneratorCLI/"
export TOOL_PROJECT_NAME="IntervalCronGeneratorCLI.csproj"

set -euo pipefail

project=$(basename -s .csproj "$CSPROJ_PATH$PROJECT_NAME")
version=$(sed -n 's:.*<Version>\(.*\)</Version>.*:\1:p' "$CSPROJ_PATH$PROJECT_NAME")
version_suffix='--version-suffix "pre"'
toolproject=$(basename -s .csproj "$TOOL_DIR$TOOL_PROJECT_NAME")
tool_version=$(sed -n 's:.*<Version>\(.*\)</Version>.*:\1:p' "$TOOL_DIR$TOOL_PROJECT_NAME")
nupkg_file=./build/$project.$version-pre.nupkg
tool_file=./build/$toolproject.$tool_version-pre.nupkg

echo "Project: $project"
echo "Branch: $GIT_BRANCH"
echo "Testing $project version: $version"

dotnet test "$TEST_PROJECT"

# Nuget packages default to "pre" release unless on master
if [ "$GIT_BRANCH" == "master" ]; then
    echo "Building production release"
    nupkg_file=./build/$project.$version.nupkg
    tool_file=./build/$toolproject.$tool_version.nupkg
    version_suffix=''
fi

dotnet pack "$CSPROJ_PATH$PROJECT_NAME" -o "$BUILD_DIR" --include-symbols $version_suffix
dotnet pack "$TOOL_DIR$TOOL_PROJECT_NAME" -o "$BUILD_DIR" $version_suffix

# Only publish when building on master or develop
if [ "$GIT_BRANCH" == "master" ] || [ "$GIT_BRANCH" == "develop" ]; then

    echo "Publishing $nupkg_file to $NUGET_SERVER"

    # Publish to nuget using NUGET_SERVER and NUGET_API_KEY env variables
    dotnet nuget push "$nupkg_file" -s "$NUGET_SERVER" -k "$INTERVAL_CRON_GENERATOR_KEY" -t 60 -n --force-english-output --skip-duplicate
    dotnet nuget push "$tool_file" -s "$NUGET_SERVER" -k "$INTERVAL_CRON_GENERATOR_KEY" -t 60 -n --force-english-output --skip-duplicate
fi