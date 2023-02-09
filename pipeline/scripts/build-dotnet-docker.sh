#!/bin/sh

SCRIPT_NAME="${0##*/}"
SCRIPT_FOLDER_PATH="$( cd "$( dirname "${0}" )" && pwd )";

# shellcheck source=./utils.sh
. "${SCRIPT_FOLDER_PATH}/utils.sh"

while getopts "b:i:t:s:p:u:" opt
do
   case "$opt" in
      b ) branchName="$OPTARG" ;;
      i ) imageName="$OPTARG" ;;
      t ) tag="$OPTARG" ;;
      s ) sdkImage="$OPTARG" ;;
      p ) projectPath="$OPTARG" ;;
      u ) url="$OPTARG" ;;
      * ) invalidParams "${SCRIPT_NAME}" ;;
   esac
done

if [ -z "$projectPath" ] \
    || [ -z "$branchName" ] \
    || [ -z "$imageName" ] \
    || [ -z "$tag" ] \
    || [ -z "$url" ] \
    || [ -z "$sdkImage" ]
then
    invalidParams "${SCRIPT_NAME}" ;
else
    set -e
    logInfo "${SCRIPT_NAME}" "Starting build of ${imageName}..."

    # Specific steps for building docker image with .NET application.
    # Here we can put more complex logic to abstract from Jenkins evironment
    # and in case of migration to different CI/CD we won't rebuild all scripts.

    docker build \
        --progress=plain --no-cache \
        --build-arg build_date="$(date -u +%Y-%m-%dT%H:%M:%SZ)" \
        --build-arg url="${url}" \
        --build-arg vcs_url="${url}" \
        --build-arg vcs_ref="$(git rev-parse HEAD)" \
        --build-arg branch_name="${branchName}" \
        --build-arg build_tag="${tag}" \
        --build-arg sdk_image="${sdkImage}" \
        -t "${imageName}:${tag}" \
        -f "${projectPath}/Dockerfile" \
        "${projectPath}"

    logInfo "${SCRIPT_NAME}" "Done."
fi
