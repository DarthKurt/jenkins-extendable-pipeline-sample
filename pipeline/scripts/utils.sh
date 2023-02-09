#!/bin/sh

logError()
{
    printf "\033[91m[%s]: %s\033[0m\n" "$1" "$2"
}

logInfo()
{
    printf "\033[36m[%s]: %s\033[0m\n" "$1" "$2"
}

logWarn()
{
    printf "\033[33m[%s]: %s\033[0m\n" "$1" "$2"
}

invalidParams()
{
    logError "$1" 'Some or all of script parameters are empty or invalid'
    exit 1
}
